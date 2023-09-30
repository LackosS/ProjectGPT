using ProjectGPT.Persistence.DTO;

namespace ProjectGPT.Interfaces
{
    public interface IChatService
    {
        public int AddChat(ChatDTO c);
        public List<ChatDTO> GetChat(int id);
        public void AddChats(List<ChatDTO> chatsDTO);
    }
}
