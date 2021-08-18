using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MessangerServer.Common
{
    public class Hash
    {
        private readonly HashAlgorithm _algorithm;

        public Hash(HashAlgorithm algorithm)
        {
            _algorithm = algorithm;
        }

        public string ComputeHash(string source)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = _algorithm.ComputeHash(Encoding.UTF8.GetBytes(source));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
