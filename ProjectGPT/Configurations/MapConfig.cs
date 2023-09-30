using AutoMapper;
using ProjectGPT.Persistence.DTO;
using ProjectGPT.Persistence.Records;

namespace ProjectGPT.Configurations
{
    public class MapConfig
    {
        public class ConversationProfile : Profile
        {
            public ConversationProfile()
            {
                CreateMap<Conversation, ConversationDTO>();
                CreateMap<ConversationDTO, Conversation>();
            }
            public class ChatProfile : Profile
            {
                public ChatProfile()
                {
                    CreateMap<Chat, ChatDTO>();
                    CreateMap<ChatDTO, Chat>();
                }
            }
        }
    }
}
 