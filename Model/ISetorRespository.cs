using System.Collections.Generic;

namespace WebChama.Model
{
    public interface ISetorRepository
    {
        // CREATE - Adiciona um novo setor
        void Add(Setor setor);

        // READ - Retorna todos os setores
        List<Setor> Get();

        // READ - Busca um setor específico pelo ID
        Setor? GetById(int id);

        // UPDATE - Atualiza os dados de um setor existente
        void Update(Setor setor);

        // DELETE - Remove um setor pelo ID
        void Delete(int id);
    }
}
