using System;
using System.Collections.Generic;
using System.Linq;
using WebChama.Model;

namespace WebChama.Infrastructure
{
    public class SetorRepository : ISetorRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        // CREATE - Adiciona um novo setor
        public void Add(Setor setor)
        {
            try
            {
                _context.Setor.Add(setor);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao adicionar setor: " + e.Message, e);
            }
        }

        // READ - Retorna todos os setores
        public List<Setor> Get()
        {
            try
            {
                return _context.Setor.ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao obter setores: " + e.Message, e);
            }
        }

        // READ - Retorna um setor específico pelo ID
        public Setor? GetById(int id)
        {
            try
            {
                return _context.Setor.FirstOrDefault(s => s.Id_setor == id);
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao obter setor com ID {id}: " + e.Message, e);
            }
        }

        // UPDATE - Atualiza o nome de um setor existente
        public void Update(Setor setor)
        {
            try
            {
                var setorExistente = _context.Setor
                    .FirstOrDefault(s => s.Id_setor == setor.Id_setor);

                if (setorExistente != null)
                {
                    setorExistente.Nome_setor = setor.Nome_setor;
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao atualizar setor: " + e.Message, e);
            }
        }

        // DELETE - Remove um setor pelo ID
        public void Delete(int id)
        {
            try
            {
                var setor = _context.Setor.FirstOrDefault(s => s.Id_setor == id);
                if (setor != null)
                {
                    _context.Setor.Remove(setor);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao remover setor com ID {id}: " + e.Message, e);
            }
        }
    }
}
