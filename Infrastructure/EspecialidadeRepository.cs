using System;
using System.Collections.Generic;
using System.Linq;
using WebChama.Model;

namespace WebChama.Infrastructure
{
    public class EspecialidadeRepository : IEspecialidadeRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        // CREATE - Adiciona uma nova especialidade
        public void Add(Especialidade especialidade)
        {
            try
            {
                _context.Especialidade.Add(especialidade);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao adicionar especialidade: " + e.Message, e);
            }
        }

        // READ - Retorna todas as especialidades
        public List<Especialidade> Get()
        {
            try
            {
                return _context.Especialidade.ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao obter especialidades: " + e.Message, e);
            }
        }

        // READ - Retorna uma especialidade específica pelo ID
        public Especialidade? GetById(int id)
        {
            try
            {
                return _context.Especialidade.FirstOrDefault(e => e.Id_especialidade == id);
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao obter especialidade com ID {id}: " + e.Message, e);
            }
        }

        // UPDATE - Atualiza uma especialidade existente
        public void Update(Especialidade especialidade)
        {
            try
            {
                var especialidadeExistente = _context.Especialidade
                    .FirstOrDefault(e => e.Id_especialidade == especialidade.Id_especialidade);

                if (especialidadeExistente != null)
                {
                    especialidadeExistente.Nome_especialidade = especialidade.Nome_especialidade;
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao atualizar especialidade: " + e.Message, e);
            }
        }

        // DELETE - Remove uma especialidade pelo ID
        public void Delete(int id)
        {
            try
            {
                var especialidade = _context.Especialidade.FirstOrDefault(e => e.Id_especialidade == id);
                if (especialidade != null)
                {
                    _context.Especialidade.Remove(especialidade);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao remover especialidade com ID {id}: " + e.Message, e);
            }
        }
    }
}
