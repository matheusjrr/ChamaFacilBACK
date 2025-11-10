using System.Collections.Generic;

namespace WebChama.Model
{
    public interface IEspecialidadeRepository
    {
        // CREATE - Adiciona uma nova especialidade
        void Add(Especialidade especialidade);

        // READ - Retorna todas as especialidades
        List<Especialidade> Get();

        // READ - Busca uma especialidade específica pelo ID
        Especialidade? GetById(int id);

        // UPDATE - Atualiza uma especialidade existente
        void Update(Especialidade especialidade);

        // DELETE - Remove uma especialidade pelo ID
        void Delete(int id);
    }
}
