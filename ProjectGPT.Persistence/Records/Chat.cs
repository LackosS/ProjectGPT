using System.ComponentModel.DataAnnotations;

namespace ProjectGPT.Persistence.Records
{
    public class Chat
    {
        [Key]
        public int Id { get; set; }
        public int? ConversationId { get; set; }
        public string Sender { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }

        public virtual Conversation Conversation{ get; set; }
    }
}
