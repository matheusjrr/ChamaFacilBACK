using System;
using System.ComponentModel.DataAnnotations;

namespace WebChama.Model
{
    public class Especialidade
    {
        [Key] // Indica que é a chave primária
        public int Id_especialidade { get; set; }

        [Required(ErrorMessage = "O nome da especialidade é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome da especialidade deve ter no máximo 100 caracteres.")]
        [Display(Name = "Nome da Especialidade")]
        public string Nome_especialidade { get; set; } = string.Empty;

        public Especialidade(int id_especialidade, string nome_especialidade)
        {
            Id_especialidade = id_especialidade;
            Nome_especialidade = nome_especialidade;
        }

        public Especialidade() { }
    }
}
