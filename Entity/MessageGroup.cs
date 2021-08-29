using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class MessageGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Message> Messages { get; set; } = new List<Message>();
        public List<User> Users { get; set; } = new List<User>();
        //TODO: сделать норм бд схему руками
    }
}
