using Entity;
using Microsoft.Extensions.Logging;
using Repository;
using System.Linq;

namespace MessangerServer.Services
{
    public class MessageService
    {
        private readonly Repository<Message> _repository;
        private readonly ILogger<MessageService> log;

        public MessageService(Repository<Message> repository, ILogger<MessageService> log)
        {
            _repository = repository;
            this.log = log;
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

        public Message[] GetMessages(int groupId)
        {
            var messages = _repository.GetWithInclude(x => x.MessageGroup, x => x.FromUser);
            return messages.Where(x => x.MessageGroup != null && x.MessageGroup.Id == groupId).OrderBy(x => x.Id).ToArray();
        }
    }
}
