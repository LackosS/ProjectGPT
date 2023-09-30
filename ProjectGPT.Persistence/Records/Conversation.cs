using System.ComponentModel.DataAnnotations;

namespace ProjectGPT.Persistence.Records
{
    public class Conversation
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Model { get; set; }
        public virtual ICollection<Chat> Chats { get; set; }
    }
}