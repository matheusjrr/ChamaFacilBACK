using System;
using System.Collections.Generic;
using System.Linq;
using WebChama.Model;

namespace WebChama.Infrastructure
{
    // Classe responsável por gerenciar a persistência de dados da entidade Chamado.
    // Implementa a interface IChamadoRepository, fornecendo métodos de CRUD (Create, Read, Update, Delete).
    // Atua como camada de acesso a dados, isolando a lógica do banco da aplicação.
    public class ChamadoRepository : IChamadoRepository
    {
        // Contexto de conexão com o banco de dados.
        // Permite acessar a tabela Chamado e executar operações de CRUD.
        private readonly ConnectionContext _context = new ConnectionContext();

        // CREATE - Adiciona um novo chamado ao banco de dados.
        // Lança exceção caso ocorra algum erro durante a inserção.
        public void Add(Chamado chamado)
        {
            try
            {
                _context.Chamado.Add(chamado);
                _context.SaveChanges(); // Salva as alterações no banco
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao adicionar chamado: " + e.Message, e);
            }
        }

        // READ - Retorna todos os chamados cadastrados.
        // Útil para listar todos os chamados no sistema.
        public List<Chamado> Get()
        {
            try
            {
                return _context.Chamado.ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao obter chamados: " + e.Message, e);
            }
        }

        // READ - Retorna um chamado específico pelo seu ID.
        // Retorna null se não encontrar nenhum chamado com o ID fornecido.
        public Chamado? GetById(int id)
        {
            try
            {
                return _context.Chamado.FirstOrDefault(c => c.Id_chamado == id);
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao obter chamado com ID {id}: " + e.Message, e);
            }
        }

        // READ - Retorna todos os chamados de um usuário específico.
        // Permite filtrar chamados por ID do usuário, útil para dashboards ou histórico de chamados.
        public List<Chamado> GetByUsuario(int idUsuario)
        {
            try
            {
                return _context.Chamado
                    .Where(c => c.Id_usuario == idUsuario)
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao obter chamados do usuário com ID {idUsuario}: " + e.Message, e);
            }
        }

        // UPDATE - Atualiza os dados de um chamado existente.
        // Busca o chamado pelo ID e atualiza seus campos.
        // Caso o chamado não exista, não faz nenhuma alteração.
        public void Update(Chamado chamado)
        {
            try
            {
                var chamadoExistente = _context.Chamado.FirstOrDefault(c => c.Id_chamado == chamado.Id_chamado);
                if (chamadoExistente != null)
                {
                    chamadoExistente.Descricao = chamado.Descricao;
                    chamadoExistente.Status = chamado.Status;
                    chamadoExistente.Data_abertura = chamado.Data_abertura;
                    chamadoExistente.Id_categoria = chamado.Id_categoria;
                    chamadoExistente.Id_usuario = chamado.Id_usuario;

                    _context.SaveChanges(); // Salva alterações no banco
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao atualizar chamado: " + e.Message, e);
            }
        }

        // DELETE - Remove um chamado pelo seu ID.
        // Busca o chamado e, se encontrado, remove do banco de dados.
        public void Delete(int id)
        {
            try
            {
                var chamado = _context.Chamado.FirstOrDefault(c => c.Id_chamado == id);
                if (chamado != null)
                {
                    _context.Chamado.Remove(chamado);
                    _context.SaveChanges(); // Salva alterações no banco
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao remover chamado com ID {id}: " + e.Message, e);
            }
        }
    }
}
