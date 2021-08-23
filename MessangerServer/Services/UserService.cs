using Entity;
using MessangerServer.Common;
using MessangerServer.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MessangerServer
{
    public class UserService
    {
        private readonly Repository<User> repository;
        private readonly AuthOptions authOptions;

        public UserService(Repository<User> repository, IOptions<AuthOptions> options)
        {
            this.repository = repository;
            authOptions = options.Value;
        }

        public string Auth(Login login)
        {
            login.Password = GetCryptPassword(login.Password);
            var user = repository.GetEntities().SingleOrDefault(x => x.Username == login.Name && x.Password == login.Password);

            if (user == null)
                throw new NullReferenceException("Пользователь не найден");

            var token = GenerateToken(user);

            return token;
        }

        public string Registration(Login reg)
        {
            reg.Password = GetCryptPassword(reg.Password);
            var user = repository.GetEntities().SingleOrDefault(x => x.Username == reg.Name);

            if (user != null)
                throw new ArgumentException("Пользователь с таким именем уже существует");

            user = repository.Save(new User()
            {
                Username = reg.Name,
                Password = reg.Password
            });

            var token = GenerateToken(user);

            return token;
        }

        public User[] GetAll()
        {
            return repository.GetEntities();
        }

        private string GetCryptPassword(string originalPassword)
        {
            return new Hash(SHA256.Create()).ComputeHash(originalPassword);
        }

        private string GenerateToken(User user)
        {
            var symetricKey = authOptions.GetSemetricKey();
            var creditionals = new SigningCredentials(symetricKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Name, user.Username),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };

            var token = new JwtSecurityToken(authOptions.Issuer,
                authOptions.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authOptions.TokenLifetime),
                signingCredentials: creditionals);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
