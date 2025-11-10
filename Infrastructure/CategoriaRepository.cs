using System.Collections.Generic;
using System.Linq;
using WebChama.Model;

namespace WebChama.Infrastructure
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        // CREATE - Adiciona uma nova categoria
        public void Add(Categoria categoria)
        {
            try
            {
                _context.Categoria.Add(categoria);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao adicionar categoria: " + e.Message, e);
            }
        }

        // READ - Retorna todas as categorias
        public List<Categoria> Get()
        {
            try
            {
                return _context.Categoria.ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao buscar categorias: " + e.Message, e);
            }
        }

        // READ - Retorna uma categoria específica pelo ID
        public Categoria? GetById(int id)
        {
            try
            {
                return _context.Categoria.FirstOrDefault(c => c.Id_categoria == id);
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao buscar categoria com ID {id}: " + e.Message, e);
            }
        }

        // UPDATE - Atualiza uma categoria existente
        public void Update(Categoria categoria)
        {
            try
            {
                var categoriaExistente = _context.Categoria.FirstOrDefault(c => c.Id_categoria == categoria.Id_categoria);
                if (categoriaExistente != null)
                {
                    categoriaExistente.Nome_categoria = categoria.Nome_categoria;
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception($"Categoria com ID {categoria.Id_categoria} não encontrada para atualização.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao atualizar categoria: " + e.Message, e);
            }
        }

        // DELETE - Remove uma categoria pelo ID
        public void Delete(int id)
        {
            try
            {
                var categoria = _context.Categoria.FirstOrDefault(c => c.Id_categoria == id);
                if (categoria != null)
                {
                    _context.Categoria.Remove(categoria);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception($"Categoria com ID {id} não encontrada para exclusão.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao excluir categoria: " + e.Message, e);
            }
        }
    }
}
