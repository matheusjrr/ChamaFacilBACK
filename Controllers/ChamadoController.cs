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

        // Recebe o mecanismo de acesso aos chamados e garante que ele esteja disponível
        public ChamadoController(IChamadoRepository chamadoRepository)
        {
            _chamadoRepository = chamadoRepository ?? throw new ArgumentNullException(nameof(chamadoRepository));
        }

        // ==================== ADICIONAR ==================== //
        // Registra uma nova solicitação de chamado
        // Valida os dados recebidos antes de enviar para armazenamento
        // Retorna confirmação ou mensagem de erro caso os dados estejam incorretos
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

            // Confirmação de que o chamado foi registrado
            return CreatedAtAction(nameof(GetById), new { id = chamado.Id_chamado }, chamado);
        }

        // ==================== LISTAR TODOS ==================== //
        // Recupera todas as solicitações registradas
        // Retorna a lista completa ou mensagem informando que não há registros
        [HttpGet]
        public IActionResult Get()
        {
            var chamados = _chamadoRepository.Get();

            if (chamados == null || !chamados.Any())
                return NotFound("Nenhum chamado encontrado.");

            return Ok(chamados);
        }

        // ==================== CONSULTAR POR IDENTIFICADOR ==================== //
        // Busca uma solicitação específica usando seu identificador
        // Retorna a solicitação encontrada ou mensagem indicando inexistência
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var chamado = _chamadoRepository.GetById(id);

            if (chamado == null)
                return NotFound($"Chamado com ID {id} não encontrado.");

            return Ok(chamado);
        }

        // ==================== CONSULTAR POR USUÁRIO ==================== //
        // Recupera todas as solicitações associadas a um usuário
        // Retorna a lista de registros ou mensagem caso não haja registros
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

        // ==================== ALTERAR ==================== //
        // Atualiza os dados de uma solicitação existente
        // Verifica existência da solicitação e validade dos dados antes de aplicar alterações
        // Retorna a solicitação atualizada ou mensagem em caso de erro
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

        // ==================== REMOVER ==================== //
        // Exclui uma solicitação usando seu identificador
        // Confirma a existência do registro antes de remover
        // Retorna confirmação de remoção ou mensagem informando que o registro não existe
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
