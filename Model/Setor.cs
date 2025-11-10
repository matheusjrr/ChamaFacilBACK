using System;
using System.ComponentModel.DataAnnotations;

namespace WebChama.Model
{
    public class Setor
    {
        [Key] // Identifica a chave primária
        public int Id_setor { get; set; }

        [Required(ErrorMessage = "O nome do setor é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do setor deve ter no máximo 100 caracteres.")]
        [Display(Name = "Nome do Setor")]
        public string Nome_setor { get; set; } = string.Empty;

        public Setor(int id_setor, string nome_setor)
        {
            Id_setor = id_setor;
            Nome_setor = nome_setor;
        }

        public Setor() { }
    }
}
