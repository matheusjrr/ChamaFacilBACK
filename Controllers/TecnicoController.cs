using Microsoft.AspNetCore.Mvc;
using WebChama.Model;
using WebChama.ViewModel;

namespace WebChama.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TecnicoController : ControllerBase
    {
        private readonly ITecnicoRepository _tecnicoRepository;

        public TecnicoController(ITecnicoRepository tecnicoRepository)
        {
            _tecnicoRepository = tecnicoRepository ?? throw new ArgumentNullException(nameof(tecnicoRepository));
        }

        // CREATE
        [HttpPost]
        public IActionResult Add([FromBody] TecnicoViewModel tecnicoView)
        {
            if (tecnicoView == null)
                return BadRequest("Dados do técnico inválidos.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tecnico = new Tecnico(
                tecnicoView.Id_tecnico,
                tecnicoView.Nome_tecnico,
                tecnicoView.Funcional,
                tecnicoView.Senha,
                tecnicoView.Id_setor
            );

            _tecnicoRepository.Add(tecnico);

            return CreatedAtAction(nameof(GetById), new { id = tecnico.Id_tecnico }, tecnico);
        }

        // READ ALL
        [HttpGet]
        public IActionResult Get()
        {
            var tecnicos = _tecnicoRepository.Get();
            if (tecnicos == null || !tecnicos.Any())
                return NotFound("Nenhum técnico encontrado.");

            return Ok(tecnicos);
        }

        // READ BY ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var tecnico = _tecnicoRepository.GetById(id);
            if (tecnico == null)
                return NotFound($"Técnico com ID {id} não encontrado.");

            return Ok(tecnico);
        }

        // UPDATE
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TecnicoViewModel tecnicoView)
        {
            if (tecnicoView == null)
                return BadRequest("Dados do técnico inválidos.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existente = _tecnicoRepository.GetById(id);
            if (existente == null)
                return NotFound($"Técnico com ID {id} não encontrado.");

            var tecnicoAtualizado = new Tecnico(
                id,
                tecnicoView.Nome_tecnico,
                tecnicoView.Funcional,
                tecnicoView.Senha,
                tecnicoView.Id_setor
            );

            _tecnicoRepository.Update(tecnicoAtualizado);

            return Ok(tecnicoAtualizado);
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var tecnico = _tecnicoRepository.GetById(id);
            if (tecnico == null)
                return NotFound($"Técnico com ID {id} não encontrado.");

            _tecnicoRepository.Delete(id);
            return NoContent();
        }
    }
}
