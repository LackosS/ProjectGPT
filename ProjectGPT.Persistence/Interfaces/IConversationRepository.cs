using ProjectGPT.Persistence.Records;

namespace ProjectGPT.Persistence.Interfaces
{
    public interface IConversationRepository
    {
        public int AddConversation(Conversation c);
        public int DeleteConversation(int id);
        public int UpdateConversation(Conversation conversation);
        public Conversation GetConversation(int id);
        public List<Conversation> GetConversations();
    }
}
