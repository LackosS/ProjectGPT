using AutoMapper;
using Microsoft.Extensions.Options;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using ProjectGPT.Configurations;
using ProjectGPT.Interfaces;
using ProjectGPT.Persistence.DTO;
using ProjectGPT.Persistence.Records;

namespace ProjectGPT.Services
{
    public class OpenAIService : IOpenAIService
    {
        private readonly OpenAIConfig _openAIConfig;
        private readonly IConversationService _conversationService;
        private readonly IChatService _chatService;
        private OpenAI_API.OpenAIAPI _api;
        private static OpenAI_API.Chat.Conversation? _currentChat;
        private readonly IMapper _mapper;
        public OpenAIService(IOptionsMonitor<OpenAIConfig> optionsMonitor, IConversationService conversationService, IMapper mapper, IChatService chatService)
        {
            _openAIConfig = optionsMonitor.CurrentValue;
            _conversationService = conversationService;
            _mapper = mapper;
            _api = new OpenAI_API.OpenAIAPI(_openAIConfig.Key);
            _chatService = chatService;
        }
        public void CreateConversation(string model)
        {
            try
            {
                ChatRequest req = new ChatRequest();
                setModel(model, req);
                saveConversation(model);
                _currentChat = _api.Chat.CreateConversation(req);
            }
            catch (Exception)
            {
                Console.WriteLine("Error during the chat creation");
            }
        }
        public void CreateConversationWithBehavior(string behavior,string model)
        {
            try
            {
                ChatRequest req = new ChatRequest();
                setModel(model, req);
                saveConversation(model);
                _currentChat = _api.Chat.CreateConversation(req);
                _currentChat.AppendSystemMessage(behavior);
            }
            catch (Exception)
            {
                Console.WriteLine("Error during the chat creation");
            }
        }

        public async Task<Dictionary<string,string>> GetAnswer(string question)
        {
            try
            {
                if (_currentChat == null) return new Dictionary<string, string>();
                _currentChat.AppendUserInput(question);
                await _currentChat.GetResponseFromChatbotAsync();
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add(_currentChat.Messages[_currentChat.Messages.Count - 2].Role, _currentChat.Messages[_currentChat.Messages.Count - 2].Content);
                dic.Add(_currentChat.Messages[_currentChat.Messages.Count - 1].Role, _currentChat.Messages[_currentChat.Messages.Count - 1].Content);
                return dic;
            }
            catch (Exception)
            {
                Console.WriteLine("Error during the answer");
                return null;
            }
        }
        public List<ChatDTO> LoadConversation(int id)
        {
            try
            {
                ProjectGPT.Persistence.Records.Conversation c = _conversationService.GetConversation(id);
                ChatRequest req = new ChatRequest();
                setModel(c.Model, req);
                //_currentChat = _api.Chat.CreateConversation(req);
                var chat = setPreviousContext(c.Id);
                return chat;
            }
            catch (Exception)
            {
                Console.WriteLine("Error during the loading");
                return null;
            }
        }
        private void saveConversation(string model)
        {
            string name = "";
            if (_currentChat != null)
            {
                if (_currentChat.Messages.Count != 0) name = _currentChat.Messages[0].Content;
                else name = "New chat";
                var conversation = new ProjectGPT.Persistence.Records.Conversation();
                conversation.Name = name;
                conversation.Model = model;
                var id = _conversationService.AddConversation(_mapper.Map<ConversationDTO>(conversation));
                foreach (ChatMessage msg in _currentChat.Messages)
                {
                    if (!msg.Role.Equals(ChatMessageRole.System))
                    {
                        _chatService.AddChat(new ChatDTO { ConversationId = id, Sender = msg.Role, Text = msg.Content, Time = DateTime.UtcNow });
                    }
                }
            }
        }
        private void setModel(string model, ChatRequest req)
        {
            if (model.Equals("3.5")) req.Model = Model.ChatGPTTurbo;
            else req.Model = Model.GPT4;
        }
        private List<ChatDTO> setPreviousContext(int id)
        {
            List<ChatDTO> chat = _chatService.GetChat(id).OrderBy(x=>x.Time).ToList();
            /*foreach (var item in chat)
            {
                if (item.Sender.Equals("system")) _currentChat.AppendUserInput(item.Text);
                if (item.Sender.Equals("user")) _currentChat.AppendUserInput(item.Text);
                if (item.Sender.Equals("assistant")) _currentChat.AppendMessage(new ChatMessage(ChatMessageRole.Assistant,item.Text));
            }*/
            return chat;
        }
    }
}
