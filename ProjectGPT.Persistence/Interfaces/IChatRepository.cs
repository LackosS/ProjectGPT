using ProjectGPT.Persistence.Records;

namespace ProjectGPT.Persistence.Interfaces
{
    public interface IChatRepository
    {
        public int AddChat(Chat c);
        public List<Chat> GetChat(int id);
        public void AddChats(List<Chat> chats);
    }
}
