using System;
using System.Collections.Generic;
using System.Linq;
using WebChama.Model;

namespace WebChama.Infrastructure
{
    public class Designacao_tecnicoRepository : IDesignacao_tecnicoRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        // CREATE - Adiciona uma nova designação de técnico
        public void Add(Designacao_tecnico designacao_tecnico)
        {
            try
            {
                _context.Designacao_tecnico.Add(designacao_tecnico);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao adicionar designação de técnico: " + e.Message, e);
            }
        }

        // READ - Retorna todas as designações
        public List<Designacao_tecnico> Get()
        {
            try
            {
                return _context.Designacao_tecnico.ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao obter designações de técnicos: " + e.Message, e);
            }
        }

        // READ - Retorna uma designação específica pelo ID
        public Designacao_tecnico? GetById(int id)
        {
            try
            {
                return _context.Designacao_tecnico.FirstOrDefault(d => d.Id_designacao == id);
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao obter designação com ID {id}: " + e.Message, e);
            }
        }

        // UPDATE - Atualiza os dados de uma designação existente
        public void Update(Designacao_tecnico designacao_tecnico)
        {
            try
            {
                var designacaoExistente = _context.Designacao_tecnico
                    .FirstOrDefault(d => d.Id_designacao == designacao_tecnico.Id_designacao);

                if (designacaoExistente != null)
                {
                    designacaoExistente.Id_chamado = designacao_tecnico.Id_chamado;
                    designacaoExistente.Id_tecnico = designacao_tecnico.Id_tecnico;
                    designacaoExistente.Data_designacao = designacao_tecnico.Data_designacao;
                    designacaoExistente.Observacao = designacao_tecnico.Observacao;
                    designacaoExistente.Status_atendimento = designacao_tecnico.Status_atendimento;

                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao atualizar designação de técnico: " + e.Message, e);
            }
        }

        // DELETE - Remove uma designação pelo ID
        public void Delete(int id)
        {
            try
            {
                var designacao = _context.Designacao_tecnico.FirstOrDefault(d => d.Id_designacao == id);
                if (designacao != null)
                {
                    _context.Designacao_tecnico.Remove(designacao);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao remover designação de técnico com ID {id}: " + e.Message, e);
            }
        }
    }
}
