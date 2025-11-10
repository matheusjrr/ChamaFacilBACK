namespace WebChama.ViewModel
{
    public class Designacao_tecnicoViewModel
    {
        public int Id_designacao { get; set; }

        public int Id_chamado { get; set; }

        public int Id_tecnico { get; set; }

        public DateTime Data_designacao { get; set; } = DateTime.Now;

        public string Observacao { get; set; } = string.Empty;

        public string Status_atendimento { get; set; } = "Aguardando";
    }
}
