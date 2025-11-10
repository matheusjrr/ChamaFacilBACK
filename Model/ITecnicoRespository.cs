using System.Collections.Generic;

namespace WebChama.Model
{
    public interface ITecnicoRepository
    {
        // CREATE - Adiciona um novo técnico
        void Add(Tecnico tecnico);

        // READ - Retorna todos os técnicos
        List<Tecnico> Get();

        // READ - Busca um técnico específico pelo ID
        Tecnico? GetById(int id);

        // UPDATE - Atualiza os dados de um técnico existente
        void Update(Tecnico tecnico);

        // DELETE - Remove um técnico pelo ID
        void Delete(int id);
    }
}
