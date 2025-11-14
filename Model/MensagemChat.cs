namespace WebChama.Model
{
    public class MensagemChat
    {
        //Texto da mensagem enviada pelo usuário
        public string Message { get; set; }
        //Identificador da sessão para manter o contexto da conversa
        public string SessionId { get; set; }
    }
}
