using Microsoft.AspNetCore.Mvc;
using WebChama.Model;
using WebChama.ViewModel;

namespace WebChama.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SetorController : ControllerBase
    {
        private readonly ISetorRepository _setorRepository;

        public SetorController(ISetorRepository setorRepository)
        {
            _setorRepository = setorRepository ?? throw new ArgumentNullException(nameof(setorRepository));
        }

        // CREATE
        [HttpPost]
        public IActionResult Add([FromBody] SetorViewModel setorView)
        {
            if (setorView == null)
                return BadRequest("Dados do setor inválidos.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var setor = new Setor(
                setorView.Id_setor,
                setorView.Nome_setor
            );

            _setorRepository.Add(setor);

            return CreatedAtAction(nameof(GetById), new { id = setor.Id_setor }, setor);
        }

        // READ ALL
        [HttpGet]
        public IActionResult Get()
        {
            var setores = _setorRepository.Get();
            if (setores == null || !setores.Any())
                return NotFound("Nenhum setor encontrado.");

            return Ok(setores);
        }

        // READ BY ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var setor = _setorRepository.GetById(id);
            if (setor == null)
                return NotFound($"Setor com ID {id} não encontrado.");

            return Ok(setor);
        }

        // UPDATE
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] SetorViewModel setorView)
        {
            if (setorView == null)
                return BadRequest("Dados do setor inválidos.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existente = _setorRepository.GetById(id);
            if (existente == null)
                return NotFound($"Setor com ID {id} não encontrado.");

            var setorAtualizado = new Setor(
                id,
                setorView.Nome_setor
            );

            _setorRepository.Update(setorAtualizado);

            return Ok(setorAtualizado);
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var setor = _setorRepository.GetById(id);
            if (setor == null)
                return NotFound($"Setor com ID {id} não encontrado.");

            _setorRepository.Delete(id);
            return NoContent();
        }
    }
}
