using Entity;
using Repository;
using System;

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
            _repository.Update(newGroup);
            return group;
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
    }
}
