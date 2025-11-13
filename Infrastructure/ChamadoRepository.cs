using System;
using System.Collections.Generic;
using System.Linq;
using WebChama.Model;

namespace WebChama.Infrastructure
{
    public class ChamadoRepository : IChamadoRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        // CREATE - Adiciona um novo chamado
        public void Add(Chamado chamado)
        {
            try
            {
                _context.Chamado.Add(chamado);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao adicionar chamado: " + e.Message, e);
            }
        }

        // READ - Retorna todos os chamados
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

        // READ - Retorna um chamado específico pelo ID
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

        // READ - Retorna todos os chamados de um usuário específico
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

        // UPDATE - Atualiza os dados de um chamado existente
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

                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao atualizar chamado: " + e.Message, e);
            }
        }

        // DELETE - Remove um chamado pelo ID
        public void Delete(int id)
        {
            try
            {
                var chamado = _context.Chamado.FirstOrDefault(c => c.Id_chamado == id);
                if (chamado != null)
                {
                    _context.Chamado.Remove(chamado);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao remover chamado com ID {id}: " + e.Message, e);
            }
        }
    }
}
