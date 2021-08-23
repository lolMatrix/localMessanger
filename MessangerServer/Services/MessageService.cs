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

        public Message sendMessage(User sender, MessageGroup group, string body)
        {
            var message = _repository.Save(new Message()
            {
                FromUser = sender,
                Body = body,
                messageGroup = group
            });

            return message;
        }
    }
}
