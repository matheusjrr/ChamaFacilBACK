using System.Collections.Generic;

namespace WebChama.Model
{
    public interface IChamadoRepository
    {
        // CREATE - Adiciona um novo chamado
        void Add(Chamado chamado);

        // READ - Retorna todos os chamados
        List<Chamado> Get();

        // READ - Busca um chamado específico pelo ID
        Chamado? GetById(int id);

        // UPDATE - Atualiza os dados de um chamado existente
        void Update(Chamado chamado);

        // DELETE - Remove um chamado pelo ID
        void Delete(int id);
    }
}
