using Entity;
using Microsoft.Extensions.Logging;
using Repository;

namespace MessangerServer.Services
{
    public class MessageService
    {
        private readonly Repository<Message> _repository;
        private readonly Logger<MessageService> log;

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

            log.LogInformation("Сообщение сохранено");

            message.FromUser = sender;
            message.MessageGroup = group;

            log.LogInformation("Иформация об отправителе и группе сообщений сохранена");

            return _repository.Update(message);
        }
    }
}
