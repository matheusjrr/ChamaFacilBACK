using Microsoft.AspNetCore.Mvc;
using WebChama.Model;
using WebChama.ViewModel;

namespace WebChama.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
        }

        // CREATE
        [HttpPost]
        public IActionResult Add([FromBody] UsuarioViewModel usuarioView)
        {
            if (usuarioView == null)
                return BadRequest("Dados do usuário inválidos.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = new Usuario(
                usuarioView.Id_usuario,
                usuarioView.Nome_usuario,
                usuarioView.Funcional,
                usuarioView.Senha,
                usuarioView.Tipo_usuario,
                usuarioView.Id_setor
            );

            _usuarioRepository.Add(usuario);

            return CreatedAtAction(nameof(GetById), new { id = usuario.Id_usuario }, usuario);
        }

        // READ ALL
        [HttpGet]
        public IActionResult Get()
        {
            var usuarios = _usuarioRepository.Get();
            if (usuarios == null || !usuarios.Any())
                return NotFound("Nenhum usuário encontrado.");

            return Ok(usuarios);
        }

        // READ BY ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var usuario = _usuarioRepository.GetById(id);
            if (usuario == null)
                return NotFound($"Usuário com ID {id} não encontrado.");

            return Ok(usuario);
        }

        // UPDATE
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UsuarioViewModel usuarioView)
        {
            if (usuarioView == null)
                return BadRequest("Dados do usuário inválidos.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existente = _usuarioRepository.GetById(id);
            if (existente == null)
                return NotFound($"Usuário com ID {id} não encontrado.");

            var usuarioAtualizado = new Usuario(
                id,
                usuarioView.Nome_usuario,
                usuarioView.Funcional,
                usuarioView.Senha,
                usuarioView.Tipo_usuario,
                usuarioView.Id_setor
            );

            _usuarioRepository.Update(usuarioAtualizado);

            return Ok(usuarioAtualizado);
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var usuario = _usuarioRepository.GetById(id);
            if (usuario == null)
                return NotFound($"Usuário com ID {id} não encontrado.");

            _usuarioRepository.Delete(id);
            return NoContent();
        }
    }
}
