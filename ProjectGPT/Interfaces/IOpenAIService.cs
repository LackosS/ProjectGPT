using ProjectGPT.Persistence.DTO;

namespace ProjectGPT.Interfaces
{
    public interface IOpenAIService
    {
        public void CreateConversation(string model);
        public void CreateConversationWithBehavior(string behavior, string model);
        Task<Dictionary<string, string>> GetAnswer(string question);
        public List<ChatDTO> LoadConversation(int id);

    }
}
