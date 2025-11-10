using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebChama.Model
{
    public class Chamado
    {
        [Key] // Define como chave primária
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_chamado { get; set; }

        [Required(ErrorMessage = "A descrição do chamado é obrigatória.")]
        [StringLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres.")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O número do chamado é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O número do chamado deve ser maior que zero.")]
        [Display(Name = "Número do Chamado")]
        public int Numero_chamado { get; set; }

        [Required(ErrorMessage = "A data de abertura é obrigatória.")]
        [Display(Name = "Data de Abertura")]
        public DateTime Data_abertura { get; set; } = DateTime.Now; // valor padrão

        [Required(ErrorMessage = "O status do chamado é obrigatório.")]
        [StringLength(20, ErrorMessage = "O status deve ter no máximo 20 caracteres.")]
        [Display(Name = "Status")]
        public string Status { get; set; } = "Pendente";

        [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
        [Display(Name = "Usuário Responsável")]
        public int Id_usuario { get; set; }

        [Required(ErrorMessage = "O ID da categoria é obrigatório.")]
        [Display(Name = "Categoria")]
        public int Id_categoria { get; set; }

        public Chamado(int id_chamado, string descricao, int numero_chamado, DateTime data_abertura, string status, int id_usuario, int id_categoria)
        {
            Id_chamado = id_chamado;
            Descricao = descricao;
            Numero_chamado = numero_chamado;
            Data_abertura = data_abertura;
            Status = status;
            Id_usuario = id_usuario;
            Id_categoria = id_categoria;
        }

        public Chamado() { }
    }
}
