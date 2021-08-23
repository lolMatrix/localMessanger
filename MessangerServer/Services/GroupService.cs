using Entity;
using Repository;

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
            newGroup.Users.Add(creator);
            var group = _repository.Save(newGroup);

            return group;
        }

        public MessageGroup AddUserToGroup(User newUser, int groupId)
        {
            var group = _repository.FindById(groupId);
            group.Users.Add(newUser);

            return _repository.Save(group);
        }
    }
}
