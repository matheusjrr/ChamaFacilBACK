namespace WebChama.ViewModel
{
    // Classe que representa os dados de um chamado para envio/recebimento via API.
    // Esta ViewModel é usada para transferir informações entre a camada de Controller e a View/API,
    // sem expor diretamente a entidade do banco de dados.
    public class ChamadoViewModel
    {
        // Identificador único do chamado.
        // Serve para diferenciar cada chamado no sistema.
        public int Id_chamado { get; set; }

        // Descrição detalhada do chamado.
        // Pode incluir informações sobre o problema, observações ou solicitações do usuário.
        public string Descricao { get; set; } = string.Empty;

        // Número sequencial do chamado.
        // Utilizado como referência rápida interna e também para relatórios.
        public int Numero_chamado { get; set; }

        // Data e hora em que o chamado foi aberto.
        // Inicializado automaticamente com a data e hora atual ao criar um novo chamado.
        public DateTime Data_abertura { get; set; } = DateTime.Now;

        // Status atual do chamado.
        // Pode assumir valores como "Pendente", "Em Andamento" ou "Concluído".
        // Inicializado como "Pendente" por padrão.
        public string Status { get; set; } = "Pendente";

        // Identificador do usuário responsável ou que criou o chamado.
        // Permite vincular o chamado a um usuário específico do sistema.
        public int Id_usuario { get; set; }

        // Identificador da categoria do chamado.
        // Relaciona o chamado a uma categoria específica, facilitando filtragem e organização.
        public int Id_categoria { get; set; }
    }
}
