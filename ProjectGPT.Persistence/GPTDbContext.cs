using Microsoft.EntityFrameworkCore;
using ProjectGPT.Persistence.Records;

namespace ProjectGPT.Persistence
{
    public class GPTDbContext : DbContext
    {
        public GPTDbContext(DbContextOptions<GPTDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //1-n Conversation - Chat
            modelBuilder.Entity<Conversation>().HasMany<Chat>(p => p.Chats).WithOne(p => p.Conversation)
                .HasForeignKey(p => p.ConversationId);
        }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Chat> Chats { get; set; }
    }
}
