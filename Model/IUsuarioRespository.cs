using System.Collections.Generic;

namespace WebChama.Model
{
    public interface IUsuarioRepository
    {
        // CREATE - Adiciona um novo usuário
        void Add(Usuario usuario);

        // READ - Retorna todos os usuários
        List<Usuario> Get();

        // READ - Busca um usuário específico pelo ID
        Usuario? GetById(int id);

        // UPDATE - Atualiza os dados de um usuário existente
        void Update(Usuario usuario);

        // DELETE - Remove um usuário pelo ID
        void Delete(int id);
    }
}
