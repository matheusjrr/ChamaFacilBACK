using System;
using System.ComponentModel.DataAnnotations;

namespace WebChama.Model
{
    public class Categoria
    {
        [Key] // Define que este campo é a chave primária
        public int Id_categoria { get; set; }

        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome da categoria deve ter no máximo 100 caracteres.")]
        [Display(Name = "Nome da Categoria")]
        public string Nome_categoria { get; set; } = string.Empty;

        // Construtor com parâmetros
        public Categoria(int id_categoria, string nome_categoria)
        {
            Id_categoria = id_categoria;
            Nome_categoria = nome_categoria;
        }

        // Construtor vazio (necessário para ORM e serialização)
        public Categoria() { }
    }
}
