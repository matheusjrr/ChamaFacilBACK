using System;
using System.ComponentModel.DataAnnotations;

namespace WebChama.Model
{
    public class Tecnico
    {
        [Key] // Chave primária
        public int Id_tecnico { get; set; }

        [Required(ErrorMessage = "O nome do técnico é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do técnico deve ter no máximo 100 caracteres.")]
        [Display(Name = "Nome do Técnico")]
        public string Nome_tecnico { get; set; } = string.Empty;

        [Required(ErrorMessage = "O funcional é obrigatório.")]
        [StringLength(20, ErrorMessage = "O funcional deve ter no máximo 20 caracteres.")]
        public string Funcional { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(255, ErrorMessage = "A senha deve ter no máximo 255 caracteres.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; } = string.Empty;

        [Required(ErrorMessage = "O setor é obrigatório.")]
        [Display(Name = "Setor do Técnico")]
        public int Id_setor { get; set; } = 0;

        public Tecnico(int id_tecnico, string nome_tecnico, string funcional, string senha, int id_setor)
        {
            Id_tecnico = id_tecnico;
            Nome_tecnico = nome_tecnico;
            Funcional = funcional;
            Senha = senha;
            Id_setor = id_setor;
        }

        public Tecnico() { }
    }
}