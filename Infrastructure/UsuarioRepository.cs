using System;
using System.Collections.Generic;
using System.Linq;
using WebChama.Model;

namespace WebChama.Infrastructure
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        // CREATE - Adiciona um novo usuário
        public void Add(Usuario usuario)
        {
            try
            {
                _context.Usuario.Add(usuario);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao adicionar usuário: " + e.Message, e);
            }
        }

        // READ - Retorna todos os usuários
        public List<Usuario> Get()
        {
            try
            {
                return _context.Usuario.ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao obter usuários: " + e.Message, e);
            }
        }

        // READ - Retorna um usuário específico pelo ID
        public Usuario? GetById(int id)
        {
            try
            {
                return _context.Usuario.FirstOrDefault(u => u.Id_usuario == id);
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao obter usuário com ID {id}: " + e.Message, e);
            }
        }

        // UPDATE - Atualiza os dados de um usuário existente
        public void Update(Usuario usuario)
        {
            try
            {
                var usuarioExistente = _context.Usuario
                    .FirstOrDefault(u => u.Id_usuario == usuario.Id_usuario);

                if (usuarioExistente != null)
                {
                    usuarioExistente.Nome_usuario = usuario.Nome_usuario;
                    usuarioExistente.Funcional = usuario.Funcional;
                    usuarioExistente.Senha = usuario.Senha;
                    usuarioExistente.Tipo_usuario = usuario.Tipo_usuario;
                    usuarioExistente.Id_setor = usuario.Id_setor;

                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao atualizar usuário: " + e.Message, e);
            }
        }

        // DELETE - Remove um usuário pelo ID
        public void Delete(int id)
        {
            try
            {
                var usuario = _context.Usuario.FirstOrDefault(u => u.Id_usuario == id);
                if (usuario != null)
                {
                    _context.Usuario.Remove(usuario);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao remover usuário com ID {id}: " + e.Message, e);
            }
        }
    }
}
