using ProjectGPT.Persistence.Interfaces;
using ProjectGPT.Persistence.Records;

namespace ProjectGPT.Persistence.Repositories
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly GPTDbContext _context;
        public ConversationRepository(GPTDbContext context)
        {
            _context = context;
        }
        public int AddConversation(Conversation c)
        {
            try
            {
                _context.Conversations.Add(c);
                _context.SaveChanges();
                return c.Id;
            }
            catch (Exception)
            {

                Console.WriteLine("Conversation add exception.");
                return -1;
            }
        }

        public int DeleteConversation(int id)
        {
            try
            {
                var c = _context.Conversations.FirstOrDefault(x => x.Id == id);
                if (c == null) return -1;
                _context.Conversations.Remove(c);
                _context.SaveChanges();
                return c.Id;
            }
            catch (Exception)
            {

                Console.WriteLine("Conversation clear exception.");
                return -1;
            }
        }
        public int UpdateConversation(Conversation conversation)
        {
            try
            {
                var c = _context.Conversations.FirstOrDefault(x => x.Id == conversation.Id);
                if (c == null) return -1;
                c.Chats = conversation.Chats;
                _context.SaveChanges();
                return c.Id;

            }
            catch (Exception)
            {
                Console.WriteLine("Conversation update exception");
                return -1;
            }
        }
        public Conversation GetConversation(int id)
        {
            try
            {
                var c = _context.Conversations.FirstOrDefault(x => x.Id == id);
                if (c == null) return null;
                return c;

            }
            catch (Exception)
            {
                Console.WriteLine("Conversation get exception");
                return null;
            }
        }
        public List<Conversation> GetConversations()
        {
            try
            {
                return _context.Conversations.ToList();
            }
            catch (Exception)
            {

                Console.WriteLine("Conversations get exception");
                return new List<Conversation> { };
            }
        }
    }
}
