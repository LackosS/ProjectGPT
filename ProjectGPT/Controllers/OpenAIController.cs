using Microsoft.AspNetCore.Mvc;
using ProjectGPT.Interfaces;
using ProjectGPT.Persistence.DTO;

namespace ProjectGPT.Controllers
{
    public class OpenAIController : ControllerBase
    {
        private readonly ILogger<OpenAIController> _logger;
        private readonly IOpenAIService _openAIService;
        private readonly IConversationService _conversationService;

        public OpenAIController(ILogger<OpenAIController> logger,
            IOpenAIService openAIService, IConversationService conversationService)
        {
            _logger = logger;
            _openAIService = openAIService;
            _conversationService = conversationService;
        }
        [HttpPost]
        [Route("CreateConversation")]
        public IActionResult CreateConversation([FromBody]CreateConversationDTO c)
        {
            _openAIService.CreateConversation(c.Model);
            return Ok();
        }
        [HttpPost]
        [Route("CreateConversationWithBehavior")]
        public IActionResult CreateConversationWithBehavior([FromBody] CreateConversationWithBehaviorDTO c)
        {
            _openAIService.CreateConversationWithBehavior(c.Behavior,c.Model);
            return Ok();
        }
        [HttpPost]
        [Route("GetAnswer")]
        public async Task<IActionResult> GetAnswer([FromBody]GetAnswerDTO a)
        {
            var result = await _openAIService.GetAnswer(a.Text);
            return Ok(result);
        }
        [HttpPatch]
        [Route("UpdateConversation")]
        public IActionResult UpdateConversation(ConversationDTO c)
        {
           return Ok(_conversationService.UpdateConversation(c));
        }
        [HttpDelete]
        [Route("DeleteConversation")]
        public IActionResult DeleteConversation(int id)
        {
            return Ok(_conversationService.DeleteConversation(id));
        }
        [HttpGet]
        [Route("LoadConversation")]
        public IActionResult LoadConversation(int id)
        {
            return Ok(_openAIService.LoadConversation(id));
        }
        [HttpGet]
        [Route("GetConversations")]
        public IActionResult GetConversations()
        {
            return Ok(_conversationService.GetConversations());
        }
    }
}
