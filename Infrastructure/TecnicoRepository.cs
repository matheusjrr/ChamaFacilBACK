using System;
using System.Collections.Generic;
using System.Linq;
using WebChama.Model;

namespace WebChama.Infrastructure
{
    public class TecnicoRepository : ITecnicoRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        // CREATE - Adiciona um novo técnico
        public void Add(Tecnico tecnico)
        {
            try
            {
                _context.Tecnico.Add(tecnico);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao adicionar técnico: " + e.Message, e);
            }
        }

        // READ - Retorna todos os técnicos
        public List<Tecnico> Get()
        {
            try
            {
                return _context.Tecnico.ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao obter técnicos: " + e.Message, e);
            }
        }

        // READ - Retorna um técnico específico pelo ID
        public Tecnico? GetById(int id)
        {
            try
            {
                return _context.Tecnico.FirstOrDefault(t => t.Id_tecnico == id);
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao obter técnico com ID {id}: " + e.Message, e);
            }
        }

        // UPDATE - Atualiza os dados de um técnico existente
        public void Update(Tecnico tecnico)
        {
            try
            {
                var tecnicoExistente = _context.Tecnico
                    .FirstOrDefault(t => t.Id_tecnico == tecnico.Id_tecnico);

                if (tecnicoExistente != null)
                {
                    tecnicoExistente.Nome_tecnico = tecnico.Nome_tecnico;
                    tecnicoExistente.Funcional = tecnico.Funcional;
                    tecnicoExistente.Senha = tecnico.Senha;
                    tecnicoExistente.Id_setor = tecnico.Id_setor;

                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao atualizar técnico: " + e.Message, e);
            }
        }

        // DELETE - Remove um técnico pelo ID
        public void Delete(int id)
        {
            try
            {
                var tecnico = _context.Tecnico.FirstOrDefault(t => t.Id_tecnico == id);
                if (tecnico != null)
                {
                    _context.Tecnico.Remove(tecnico);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao remover técnico com ID {id}: " + e.Message, e);
            }
        }
    }
}
