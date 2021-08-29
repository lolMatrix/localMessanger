using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entity
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        [JsonIgnore]
        public List<MessageGroup> MessageGroups { get; set; } = new List<MessageGroup>();
    }
}
