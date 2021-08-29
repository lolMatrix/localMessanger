using Entity;
using Repository;

namespace MessangerServer.Services
{
    public class MessageService
    {
        private readonly Repository<Message> _repository;

        public MessageService(Repository<Message> repository)
        {
            _repository = repository;
        }

        public Message SendMessage(User sender, MessageGroup group, string body)
        {
            var message = _repository.Save(new Message()
            {
                Body = body
            });

            message.FromUser = sender;
            message.MessageGroup = group;

            return _repository.Update(message);
        }
    }
}
