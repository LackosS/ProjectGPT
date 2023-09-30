using AutoMapper;
using ProjectGPT.Interfaces;
using ProjectGPT.Persistence.DTO;
using ProjectGPT.Persistence.Interfaces;
using ProjectGPT.Persistence.Records;

namespace ProjectGPT.Services
{
    public class ConversationService : IConversationService
    {
        private IConversationRepository _conversationRepository;
        private readonly IMapper _mapper;

        public ConversationService(IConversationRepository conversationRepository, IMapper mapper)
        {
            _conversationRepository = conversationRepository;
            _mapper = mapper;
        }

        public int AddConversation(ConversationDTO c)
        {
            var conversation = _mapper.Map<Conversation>(c);
            return _conversationRepository.AddConversation(conversation);
        }
        public int DeleteConversation(int id)
        {
            return _conversationRepository.DeleteConversation(id);
        }
        public int UpdateConversation(ConversationDTO c)
        {
            var conversation = _mapper.Map<Conversation>(c);
            return _conversationRepository.UpdateConversation(conversation);
        }
        public Conversation GetConversation(int id)
        {
            return _conversationRepository.GetConversation(id);
        }
        public List<ConversationDTO> GetConversations()
        {
            var conversations = _conversationRepository.GetConversations();
            return _mapper.Map<List<ConversationDTO>>(conversations);
        }
    }
}
