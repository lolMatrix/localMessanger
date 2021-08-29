using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entity
{
    public class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Body { get; set; }
        public User FromUser { get; set; }
        [JsonIgnore]
        public MessageGroup MessageGroup { get; set; }
    }
}
