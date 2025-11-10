namespace WebChama.ViewModel
{
    public class UsuarioViewModel
    {
        public int Id_usuario { get; set; }

        public string Nome_usuario { get; set; } = string.Empty;

        public string Funcional { get; set; } = string.Empty;

        public string Senha { get; set; } = string.Empty;

        public string Tipo_usuario { get; set; } = string.Empty;

        public int Id_setor { get; set; }
    }
}
