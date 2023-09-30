namespace ProjectGPT.Persistence.DTO
{
    public class ChatDTO
    {
        public int Id { get; set; }
        public int ConversationId { get; set; }
        public string Sender { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
    }
}
