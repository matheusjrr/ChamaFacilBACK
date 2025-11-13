using Microsoft.AspNetCore.Mvc;
using WebChama.Model;
using WebChama.ViewModel;

namespace WebChama.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ChamadoController : ControllerBase
    {
        private readonly IChamadoRepository _chamadoRepository;

        public ChamadoController(IChamadoRepository chamadoRepository)
        {
            _chamadoRepository = chamadoRepository ?? throw new ArgumentNullException(nameof(chamadoRepository));
        }

        // === CREATE ===
        [HttpPost]
        public IActionResult Add([FromBody] ChamadoViewModel chamadoView)
        {
            if (chamadoView == null)
                return BadRequest("Dados do chamado inválidos.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var chamado = new Chamado(
                chamadoView.Id_chamado,
                chamadoView.Descricao,
                chamadoView.Numero_chamado,
                chamadoView.Data_abertura == default ? DateTime.Now : chamadoView.Data_abertura,
                string.IsNullOrWhiteSpace(chamadoView.Status) ? "Pendente" : chamadoView.Status,
                chamadoView.Id_usuario,
                chamadoView.Id_categoria
            );

            _chamadoRepository.Add(chamado);

            return CreatedAtAction(nameof(GetById), new { id = chamado.Id_chamado }, chamado);
        }

        // === READ ALL ===
        [HttpGet]
        public IActionResult Get()
        {
            var chamados = _chamadoRepository.Get();

            if (chamados == null || !chamados.Any())
                return NotFound("Nenhum chamado encontrado.");

            return Ok(chamados);
        }

        // === READ BY ID ===
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var chamado = _chamadoRepository.GetById(id);

            if (chamado == null)
                return NotFound($"Chamado com ID {id} não encontrado.");

            return Ok(chamado);
        }

        // === READ BY USER ===
        [HttpGet("usuario/{idUsuario}")]
        public IActionResult GetByUsuario(int idUsuario)
        {
            var chamadosUsuario = _chamadoRepository
                .Get()
                .Where(c => c.Id_usuario == idUsuario)
                .ToList();

            if (chamadosUsuario == null || !chamadosUsuario.Any())
                return NotFound($"Nenhum chamado encontrado para o usuário com ID {idUsuario}.");

            return Ok(chamadosUsuario);
        }

        // === UPDATE ===
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ChamadoViewModel chamadoView)
        {
            if (chamadoView == null)
                return BadRequest("Dados do chamado inválidos.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existente = _chamadoRepository.GetById(id);
            if (existente == null)
                return NotFound($"Chamado com ID {id} não encontrado.");

            var chamadoAtualizado = new Chamado(
                id,
                chamadoView.Descricao,
                chamadoView.Numero_chamado,
                chamadoView.Data_abertura == default ? existente.Data_abertura : chamadoView.Data_abertura,
                string.IsNullOrWhiteSpace(chamadoView.Status) ? existente.Status : chamadoView.Status,
                chamadoView.Id_usuario,
                chamadoView.Id_categoria
            );

            _chamadoRepository.Update(chamadoAtualizado);

            return Ok(chamadoAtualizado);
        }

        // === DELETE ===
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var chamado = _chamadoRepository.GetById(id);
            if (chamado == null)
                return NotFound($"Chamado com ID {id} não encontrado.");

            _chamadoRepository.Delete(id);
            return NoContent();
        }
    }
}
