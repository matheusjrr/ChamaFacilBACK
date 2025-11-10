using System.Collections.Generic;

namespace WebChama.Model
{
    public interface IDesignacao_tecnicoRepository
    {
        // CREATE - Adiciona uma nova designação de técnico
        void Add(Designacao_tecnico designacao_tecnico);

        // READ - Retorna todas as designações
        List<Designacao_tecnico> Get();

        // READ - Busca uma designação específica pelo ID
        Designacao_tecnico? GetById(int id);

        // UPDATE - Atualiza uma designação existente
        void Update(Designacao_tecnico designacao_tecnico);

        // DELETE - Remove uma designação pelo ID
        void Delete(int id);
    }
}
