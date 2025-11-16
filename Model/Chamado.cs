using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebChama.Model
{
    // Classe que representa a entidade "Chamado" no banco de dados.
    // Cada instância corresponde a um registro da tabela Chamado.
    // Contém propriedades com validações para garantir a integridade dos dados.
    public class Chamado
    {
        // Chave primária do chamado, gerada automaticamente pelo banco.
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_chamado { get; set; }

        // Descrição detalhada do chamado.
        // Obrigatória e limitada a 200 caracteres.
        [Required(ErrorMessage = "A descrição do chamado é obrigatória.")]
        [StringLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres.")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; } = string.Empty;

        // Número sequencial do chamado.
        // Obrigatório e deve ser maior que zero.
        [Required(ErrorMessage = "O número do chamado é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O número do chamado deve ser maior que zero.")]
        [Display(Name = "Número do Chamado")]
        public int Numero_chamado { get; set; }

        // Data e hora em que o chamado foi aberto.
        // Inicializado com a data atual por padrão.
        [Required(ErrorMessage = "A data de abertura é obrigatória.")]
        [Display(Name = "Data de Abertura")]
        public DateTime Data_abertura { get; set; } = DateTime.Now;

        // Status atual do chamado.
        // Pode ser "Pendente", "Em Andamento" ou "Concluído".
        // Limitado a 20 caracteres e obrigatório.
        [Required(ErrorMessage = "O status do chamado é obrigatório.")]
        [StringLength(20, ErrorMessage = "O status deve ter no máximo 20 caracteres.")]
        [Display(Name = "Status")]
        public string Status { get; set; } = "Pendente";

        // ID do usuário responsável pelo chamado.
        // Obrigatório para associar o chamado a um usuário específico.
        [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
        [Display(Name = "Usuário Responsável")]
        public int Id_usuario { get; set; }

        // ID da categoria do chamado.
        // Obrigatório para classificar o chamado dentro de uma categoria específica.
        [Required(ErrorMessage = "O ID da categoria é obrigatório.")]
        [Display(Name = "Categoria")]
        public int Id_categoria { get; set; }

        // Construtor que permite inicializar todas as propriedades do chamado.
        // Útil para criar instâncias completas rapidamente.
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

        // Construtor padrão necessário para o Entity Framework e para criar objetos vazios.
        public Chamado() { }
    }
}
