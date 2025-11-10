using System;
using System.ComponentModel.DataAnnotations;

namespace WebChama.Model
{
    public class Designacao_tecnico
    {
        [Key] // Chave primária
        public int Id_designacao { get; set; }

        [Required(ErrorMessage = "O ID do chamado é obrigatório.")]
        [Display(Name = "Chamado")]
        public int Id_chamado { get; set; }

        [Required(ErrorMessage = "O ID do técnico é obrigatório.")]
        [Display(Name = "Técnico Designado")]
        public int Id_tecnico { get; set; }

        [Required(ErrorMessage = "A data da designação é obrigatória.")]
        [Display(Name = "Data da Designação")]
        public DateTime Data_designacao { get; set; } = DateTime.Now;

        [StringLength(500, ErrorMessage = "A observação deve ter no máximo 500 caracteres.")]
        [Display(Name = "Observação")]
        public string Observacao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O status do atendimento é obrigatório.")]
        [StringLength(30, ErrorMessage = "O status deve ter no máximo 30 caracteres.")]
        [Display(Name = "Status do Atendimento")]
        public string Status_atendimento { get; set; } = "Aguardando";

        public Designacao_tecnico(
            int id_designacao,
            int id_chamado,
            int id_tecnico,
            DateTime data_designacao,
            string observacao,
            string status_atendimento)
        {
            Id_designacao = id_designacao;
            Id_chamado = id_chamado;
            Id_tecnico = id_tecnico;
            Data_designacao = data_designacao;
            Observacao = observacao;
            Status_atendimento = status_atendimento;
        }

        public Designacao_tecnico() { }
    }
}
