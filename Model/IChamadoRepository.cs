using System.Collections.Generic;

namespace WebChama.Model
{
    // Interface que define os métodos de CRUD para a entidade Chamado.
    // Serve como contrato para qualquer implementação de repositório,
    // garantindo que os métodos essenciais de acesso a dados estejam disponíveis.
    public interface IChamadoRepository
    {
        // CREATE - Adiciona um novo chamado no banco de dados.
        // Recebe um objeto Chamado com os dados a serem inseridos.
        void Add(Chamado chamado);

        // READ - Retorna todos os chamados cadastrados.
        // Útil para listar todos os chamados no sistema ou em dashboards.
        List<Chamado> Get();

        // READ - Busca um chamado específico pelo seu ID.
        // Retorna o chamado correspondente ou null se não for encontrado.
        Chamado? GetById(int id);

        // READ - Retorna todos os chamados de um usuário específico.
        // Permite filtrar chamados por ID do usuário, útil para histórico ou relatórios.
        List<Chamado> GetByUsuario(int idUsuario);

        // UPDATE - Atualiza os dados de um chamado existente.
        // Recebe um objeto Chamado com as alterações, que serão aplicadas no registro correspondente.
        void Update(Chamado chamado);

        // DELETE - Remove um chamado do banco de dados pelo seu ID.
        // Caso o chamado não exista, nenhuma alteração é feita.
        void Delete(int id);
    }
}
