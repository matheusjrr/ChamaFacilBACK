using System;
using System.ComponentModel.DataAnnotations;

namespace WebChama.Model
{
    public class Usuario
    {
        [Key] // Chave primária
        public int Id_usuario { get; set; }

        [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do usuário deve ter no máximo 100 caracteres.")]
        [Display(Name = "Nome do Usuário")]
        public string Nome_usuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "O funcional é obrigatório.")]
        [StringLength(20, ErrorMessage = "O funcional deve ter no máximo 20 caracteres.")]
        public string Funcional { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(255, ErrorMessage = "A senha deve ter no máximo 255 caracteres.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; } = string.Empty;

        [Required(ErrorMessage = "O tipo de usuário é obrigatório.")]
        [StringLength(50, ErrorMessage = "O tipo de usuário deve ter no máximo 50 caracteres.")]
        [Display(Name = "Tipo de Usuário")]
        public string Tipo_usuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "O setor é obrigatório.")]
        [Display(Name = "Setor do Usuário")]
        public int Id_setor { get; set; } = 0;

        public Usuario(int id_usuario, string nome_usuario, string funcional, string senha, string tipo_usuario, int id_setor)
        {
            Id_usuario = id_usuario;
            Nome_usuario = nome_usuario;
            Funcional = funcional;
            Senha = senha;
            Tipo_usuario = tipo_usuario;
            Id_setor = id_setor;
        }

        public Usuario() { }
    }
}