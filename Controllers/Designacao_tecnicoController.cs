using Microsoft.AspNetCore.Mvc;
using WebChama.Model;
using WebChama.ViewModel;

namespace WebChama.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class Designacao_tecnicoController : ControllerBase
    {
        private readonly IDesignacao_tecnicoRepository _designacaoRepository;

        public Designacao_tecnicoController(IDesignacao_tecnicoRepository designacaoRepository)
        {
            _designacaoRepository = designacaoRepository ?? throw new ArgumentNullException(nameof(designacaoRepository));
        }

        // CREATE
        [HttpPost]
        public IActionResult Add([FromBody] Designacao_tecnicoViewModel designacaoView)
        {
            if (designacaoView == null)
                return BadRequest("Dados da designação inválidos.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var designacao = new Designacao_tecnico(
                designacaoView.Id_designacao,
                designacaoView.Id_chamado,
                designacaoView.Id_tecnico,
                designacaoView.Data_designacao == default ? DateTime.Now : designacaoView.Data_designacao,
                designacaoView.Observacao,
                string.IsNullOrWhiteSpace(designacaoView.Status_atendimento) ? "Aguardando" : designacaoView.Status_atendimento
            );

            _designacaoRepository.Add(designacao);

            return CreatedAtAction(nameof(GetById), new { id = designacao.Id_designacao }, designacao);
        }

        // READ ALL
        [HttpGet]
        public IActionResult Get()
        {
            var designacoes = _designacaoRepository.Get();

            if (designacoes == null || !designacoes.Any())
                return NotFound("Nenhuma designação encontrada.");

            return Ok(designacoes);
        }

        // READ BY ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var designacao = _designacaoRepository.GetById(id);

            if (designacao == null)
                return NotFound($"Designação com ID {id} não encontrada.");

            return Ok(designacao);
        }

        // UPDATE
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Designacao_tecnicoViewModel designacaoView)
        {
            if (designacaoView == null)
                return BadRequest("Dados da designação inválidos.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existente = _designacaoRepository.GetById(id);
            if (existente == null)
                return NotFound($"Designação com ID {id} não encontrada.");

            var designacaoAtualizada = new Designacao_tecnico(
                id,
                designacaoView.Id_chamado,
                designacaoView.Id_tecnico,
                designacaoView.Data_designacao == default ? existente.Data_designacao : designacaoView.Data_designacao,
                string.IsNullOrWhiteSpace(designacaoView.Observacao) ? existente.Observacao : designacaoView.Observacao,
                string.IsNullOrWhiteSpace(designacaoView.Status_atendimento) ? existente.Status_atendimento : designacaoView.Status_atendimento
            );

            _designacaoRepository.Update(designacaoAtualizada);

            return Ok(designacaoAtualizada);
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var designacao = _designacaoRepository.GetById(id);
            if (designacao == null)
                return NotFound($"Designação com ID {id} não encontrada.");

            _designacaoRepository.Delete(id);
            return NoContent();
        }
    }
}
