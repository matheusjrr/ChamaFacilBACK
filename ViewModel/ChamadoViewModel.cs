namespace WebChama.ViewModel
{
    public class ChamadoViewModel
    {
        public int Id_chamado { get; set; }

        public string Descricao { get; set; } = string.Empty;

        public int Numero_chamado { get; set; }

        public DateTime Data_abertura { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Pendente";

        public int Id_usuario { get; set; }

        public int Id_categoria { get; set; }
    }
}
