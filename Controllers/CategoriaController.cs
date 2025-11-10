using Microsoft.AspNetCore.Mvc;
using WebChama.Model;
using WebChama.ViewModel;

namespace WebChama.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository ?? throw new ArgumentNullException(nameof(categoriaRepository));
        }

        // CREATE
        [HttpPost]
        public IActionResult Add([FromBody] CategoriaViewModel categoriaView)
        {
            var categoria = new Categoria(categoriaView.Id_categoria, categoriaView.Nome_categoria);
            _categoriaRepository.Add(categoria);
            return CreatedAtAction(nameof(GetById), new { id = categoria.Id_categoria }, categoria);
        }

        // READ ALL
        [HttpGet]
        public IActionResult Get()
        {
            var categorias = _categoriaRepository.Get();
            if (categorias == null || !categorias.Any())
                return NotFound("Nenhuma categoria encontrada.");

            return Ok(categorias);
        }

        // READ BY ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var categoria = _categoriaRepository.GetById(id);
            if (categoria == null)
                return NotFound($"Categoria com ID {id} não encontrada.");

            return Ok(categoria);
        }

        // UPDATE
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CategoriaViewModel categoriaView)
        {
            var categoriaExistente = _categoriaRepository.GetById(id);
            if (categoriaExistente == null)
                return NotFound($"Categoria com ID {id} não encontrada.");

            var categoriaAtualizada = new Categoria(id, categoriaView.Nome_categoria);
            _categoriaRepository.Update(categoriaAtualizada);

            return Ok(categoriaAtualizada);
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var categoria = _categoriaRepository.GetById(id);
            if (categoria == null)
                return NotFound($"Categoria com ID {id} não encontrada.");

            _categoriaRepository.Delete(id);
            return NoContent();
        }
    }
}
