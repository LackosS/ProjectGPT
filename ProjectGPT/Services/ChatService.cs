using AutoMapper;
using ProjectGPT.Interfaces;
using ProjectGPT.Persistence.DTO;
using ProjectGPT.Persistence.Interfaces;
using ProjectGPT.Persistence.Records;

namespace ProjectGPT.Services
{
    public class ChatService : IChatService
    {
        private IChatRepository _chatRepository;
        private readonly IMapper _mapper;

        public ChatService(IChatRepository chatRepository, IMapper mapper)
        {
            _chatRepository = chatRepository;
            _mapper = mapper;
        }
        public int AddChat(ChatDTO c)
        {
            var chat = _mapper.Map<Chat>(c);
            return _chatRepository.AddChat(chat);
        }
        public List<ChatDTO>GetChat(int id)
        {
            var list = _chatRepository.GetChat(id);
            List<ChatDTO> listDTO = new List<ChatDTO>();
            foreach (var item in list)
            {
                listDTO.Add(_mapper.Map<ChatDTO>(item));
            }
            return listDTO;
        }
        public void AddChats(List<ChatDTO> chatsDTO)
        {
            List<Chat> chats = new List<Chat>();
            foreach (var item in chatsDTO)
            {
                chats.Add(_mapper.Map<Chat>(item));
            }
            _chatRepository.AddChats(chats);
        }
    }
}
