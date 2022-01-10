using Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MessangerServer.Services
{
    public class GroupService
    {
        private readonly Repository<MessageGroup> _repository;

        public GroupService(Repository<MessageGroup> repository)
        {
            _repository = repository;
        }

        public MessageGroup CreateGroup(string name, User creator)
        {
            var newGroup = new MessageGroup()
            {
                Name = name
            };
            var group = _repository.Save(newGroup);
            newGroup.Users.Add(creator);
            return _repository.Update(newGroup);
        }

        public MessageGroup AddUserToGroup(User newUser, int groupId)
        {
            var group = _repository.FindById(groupId);
            group.Users.Add(newUser);

            return _repository.Update(group);
        }

        public MessageGroup GetById(int groupId)
        {
            return _repository.FindById(groupId);
        }

        public List<MessageGroup> getMessageGroupsByUserId(int userId)
        {
            var groups = _repository.GetWithInclude(x => x.Users);
            List<MessageGroup> messageGroups = new List<MessageGroup>();
            foreach (var group in groups)
            {
                var user = group.Users.FirstOrDefault(x => x.Id == userId);
                if (user != null)
                {
                    messageGroups.Add(group);
                }
            }
            return messageGroups;
        }
    }
}
