using Microsoft.AspNetCore.Mvc;
using WebChama.Model;
using WebChama.ViewModel;

namespace WebChama.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EspecialidadeController : ControllerBase
    {
        private readonly IEspecialidadeRepository _especialidadeRepository;

        public EspecialidadeController(IEspecialidadeRepository especialidadeRepository)
        {
            _especialidadeRepository = especialidadeRepository ?? throw new ArgumentNullException(nameof(especialidadeRepository));
        }

        // CREATE
        [HttpPost]
        public IActionResult Add([FromBody] EspecialidadeViewModel especialidadeView)
        {
            if (especialidadeView == null)
                return BadRequest("Dados da especialidade inválidos.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var especialidade = new Especialidade(
                especialidadeView.Id_especialidade,
                especialidadeView.Nome_especialidade
            );

            _especialidadeRepository.Add(especialidade);

            return CreatedAtAction(nameof(GetById), new { id = especialidade.Id_especialidade }, especialidade);
        }

        // READ ALL
        [HttpGet]
        public IActionResult Get()
        {
            var especialidades = _especialidadeRepository.Get();
            if (especialidades == null || !especialidades.Any())
                return NotFound("Nenhuma especialidade encontrada.");

            return Ok(especialidades);
        }

        // READ BY ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var especialidade = _especialidadeRepository.GetById(id);
            if (especialidade == null)
                return NotFound($"Especialidade com ID {id} não encontrada.");

            return Ok(especialidade);
        }

        // UPDATE
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] EspecialidadeViewModel especialidadeView)
        {
            if (especialidadeView == null)
                return BadRequest("Dados da especialidade inválidos.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existente = _especialidadeRepository.GetById(id);
            if (existente == null)
                return NotFound($"Especialidade com ID {id} não encontrada.");

            var especialidadeAtualizada = new Especialidade(
                id,
                especialidadeView.Nome_especialidade
            );

            _especialidadeRepository.Update(especialidadeAtualizada);

            return Ok(especialidadeAtualizada);
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var especialidade = _especialidadeRepository.GetById(id);
            if (especialidade == null)
                return NotFound($"Especialidade com ID {id} não encontrada.");

            _especialidadeRepository.Delete(id);
            return NoContent();
        }
    }
}
