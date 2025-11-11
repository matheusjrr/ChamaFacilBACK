using Microsoft.AspNetCore.Mvc;
using WebChama.Infrastructure;
using WebChama.Model;

namespace WebChama.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IaController : ControllerBase
    {
        private readonly OpenAiService _openAiService;

        public IaController(OpenAiService openAiService)
        {
            _openAiService = openAiService;
        }

        [HttpPost("chat")]
        public async Task<IActionResult> Chat([FromBody] MensagemChat msg)
        {
            if (string.IsNullOrWhiteSpace(msg?.Message))
                return BadRequest(new { error = "mensagem vazia" });

            try
            {
                var resposta = await _openAiService.EnviarMensagemAsync(msg.SessionId ?? "default", msg.Message);
                return Ok(new { response = resposta });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
