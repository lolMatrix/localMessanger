namespace Entity
{
    public class Message
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public User FromUser { get; set; }
    }
}
