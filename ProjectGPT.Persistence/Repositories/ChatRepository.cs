using ProjectGPT.Persistence.Interfaces;
using ProjectGPT.Persistence.Records;

namespace ProjectGPT.Persistence.Repositories
{
    public class ChatRepository: IChatRepository
    {
        private readonly GPTDbContext _context;
        public ChatRepository(GPTDbContext context)
        {
            _context = context;
        }
        public int AddChat(Chat c)
        {
            try
            {
                _context.Chats.Add(c);
                _context.SaveChanges();
                return c.Id;
            }
            catch (Exception)
            {

                Console.WriteLine("Chat add exception.");
                return -1;
            }
        }
        public void AddChats(List<Chat> chats)
        {
            try
            {
                _context.Chats.AddRange(chats);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                Console.WriteLine("Chats add exception.");
            }
        }
        public List<Chat> GetChat(int id)
        {
            try
            {
                return _context.Chats.Where(x=>x.ConversationId==id).ToList();

            }
            catch (Exception)
            {
                Console.WriteLine("Conversation get exception");
                return null;
            }
        }
    }
}
