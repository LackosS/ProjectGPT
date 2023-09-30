using ProjectGPT.Persistence.DTO;
using ProjectGPT.Persistence.Records;

namespace ProjectGPT.Interfaces
{
    public interface IConversationService
    {
        public int AddConversation(ConversationDTO c);
        public int DeleteConversation(int id);
        public int UpdateConversation(ConversationDTO c);
        public Conversation GetConversation(int id);
        public List<ConversationDTO> GetConversations();
    }
}
