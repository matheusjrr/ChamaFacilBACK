namespace WebChama.ViewModel
{
    public class TecnicoViewModel
    {
        public int Id_tecnico { get; set; }

        public string Nome_tecnico { get; set; } = string.Empty;

        public string Funcional { get; set; } = string.Empty;

        public string Senha { get; set; } = string.Empty;

        public int Id_setor { get; set; }
    }
}
