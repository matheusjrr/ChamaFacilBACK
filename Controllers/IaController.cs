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
            // Verifica se a mensagem enviada é nula ou vazia
            if (string.IsNullOrWhiteSpace(msg?.Message))
                return BadRequest(new { error = "Mensagem vazia" });

            try
            {
                // Envia a mensagem para a IA e aguarda a resposta
                // O sessionId mantém o contexto da conversa
                var resposta = await _openAiService.EnviarMensagemAsync(
                    msg.SessionId ?? "default",
                    msg.Message
                );

                // Retorna a resposta obtida da IA
                return Ok(new { response = resposta });
            }
            catch (Exception ex)
            {
                // Caso ocorra algum erro interno, retorna código 500
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}