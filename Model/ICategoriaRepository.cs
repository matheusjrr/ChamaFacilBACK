using System.Collections.Generic;

namespace WebChama.Model
{
    public interface ICategoriaRepository
    {
        // CREATE - Adiciona uma nova categoria
        void Add(Categoria categoria);

        // READ - Retorna todas as categorias
        List<Categoria> Get();

        // READ - Busca uma categoria específica pelo ID
        Categoria? GetById(int id);

        // UPDATE - Atualiza uma categoria existente
        void Update(Categoria categoria);

        // DELETE - Remove uma categoria pelo ID
        void Delete(int id);
    }
}
