using System.Collections.Generic;

namespace Entity
{
    public class MessageGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Message> Messages { get; set; } = new List<Message>();
        public List<User> Users { get; set; } = new List<User>();
    }
}
