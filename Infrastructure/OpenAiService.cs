using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;

namespace WebChama.Infrastructure
{
    public class OpenAiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        // Chave de acesso da API da OpenAI.
        private const string OPENAI_API_KEY = "CHAVE-OPENAI";

        // Endpoint oficial da OpenAI para envio de mensagens.
        private const string OPENAI_URL = "https://api.openai.com/v1/chat/completions";

        // Dicionário para armazenar as conversas de cada usuário separadamente.
        // A chave é o userId, e o valor é uma lista de mensagens (role + content).
        private static readonly ConcurrentDictionary<string, List<Dictionary<string, string>>> userConversations = new();

        public OpenAiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> EnviarMensagemAsync(string userId, string mensagem)
        {
            // Obtém a conversa existente ou cria uma nova caso não exista.
            var conversation = userConversations.GetOrAdd(userId, _ => new List<Dictionary<string, string>>());

            // Adiciona a mensagem do usuário ao histórico.
            conversation.Add(new() { { "role", "user" }, { "content", mensagem } });

            // Cria o cliente HTTP que enviará a requisição.
            var httpClient = _httpClientFactory.CreateClient();

            // Insere o token de autenticação.
            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", OPENAI_API_KEY);

            // Mensagem de sistema contendo todo o comportamento que a IA deve seguir.
            var messages = new List<object>
            {
                new
                {
                    role = "system",
                    content = @"Você é um assistente inteligente, chamado Zé Help, do sistema de chamados Chama Fácil. Sua função é guiar o usuário dentro do sistema, explicando de forma natural e clara o que ele deve fazer.

                    ──────────────────────────────
                    👤 USUÁRIO COMUM (CLIENTE)
                    ──────────────────────────────
                    • O Cliente acessa o sistema clicando em 'Entrar', ou 'Abrir Chamado'(para acessar a página 'Login') na pagina 'Home', e faz login informando sua Funcional e Senha.  
                    • Caso não possua cadastro, pode se registrar clicando em 'Cadastre-se' na página 'Home' ou em 'Cadastrar' na página 'Login'. Para se cadastrar, o usuario deve informar:  
                      - Nome completo;  
                      - Data de nascimento;  
                      - CPF (apenas números);  
                      - Funcional;    
                      - Senha;  
                      - Confirmação de senha.  

                    📌 Após o login, o Cliente é direcionado para sua área principal, onde pode:  
                      - Visualizar suas atividades, clicando em 'Minhas Atividades' para ir até a página 'Atividades';  
                        - Ver atividades recentes, como:  
                        ▪ 'Você abriu o chamado 403';  
                        ▪ 'Chamado 002 foi concluído por Suporte TI';  
                        ▪ 'Seu chamado 105 foi atualizado para Em Andamento';
                      - Criar e ver seus chamados concluídos e pendentes, clicando em 'Chamados', para ir até a página 'Chamados';  
                      - Para criar um novo chamado deve-se infromar a categoria(TI, Equipamento ou Infraestrutura) e a descrição do problema, após clicar em 'Novo chamado', na página 'Chamados';    
                      - Pesquisar respostas prontas e perguntas frequentes, clicando em 'Central de soluções', para ir até a pagina 'Faq';  
                      - Acessar a página 'Perfil', clicando em 'Perfil' 
                      - E sair da conta, clicando em 'Sair'.  

                    📚 O Cliente também pode acessar a área de Artigos e Documentos, clicando em 'Artigos & Documentos', que contém:  
                      ▪ Boas práticas de desenvolvimento;  
                      ▪ Guia de segurança digital;  
                      ▪ Banco de dados;  
                      ▪ Infraestrutura e redes;  
                      ▪ Relatórios e Power BI;  
                      ▪ Inteligência Artificial na automação.  

                    👤 Na área de Perfil, o Cliente pode:  
                      - Visualizar nome completo e e-mail;  
                      - Editar informações pessoais;  
                      - Criar uma nova senha;  
                      - Adicionar ou alterar sua foto de perfil.  

                    ──────────────────────────────
                    🧰 USUÁRIO TÉCNICO
                    ──────────────────────────────
                    • O Técnico acessa o sistema clicando em 'Sou Técnico'.  
                    • Deve informar sua Funcional e Senha para fazer login.  
                    • Caso não possua cadastro, deve entrar em contato com o Administrador para criar sua conta.  

                    🔧 Após o login, o Técnico é direcionado para sua área de trabalho, onde pode:  
                      - Visualizar suas atividades;  
                      - Gerenciar chamados pendentes, em andamento e concluídos e respondê-los;  
                      - Acessar os detalhes de cada chamado;  
                      - Atualizar status e registrar ações tomadas;  
                      - Sair do sistema.               

                    👤 Na área de Perfil, o Técnico pode:  
                      - Visualizar nome completo e e-mail;  
                      - Editar informações pessoais;  
                      - Criar uma nova senha;  
                      - Adicionar ou alterar sua foto de perfil. 

                    ──────────────────────────────
                    💬 ORIENTAÇÕES GERAIS
                    ──────────────────────────────
                    • Sempre responda de forma educada, clara e objetiva.  
                    • Se o usuário quiser abrir um chamado, oriente-o pedindo a categoria e a descrição do problema, e depois direcionando ele para a página de 'Chamados', e clicar em 'Novo Chamado'.  
                    • Se o usuário tiver dúvidas sobre o uso do sistema, explique passo a passo onde ele deve clicar e o que encontrará.  
                    • Evite linguagem técnica excessiva, priorizando clareza e simplicidade."
                }
            };

            // Adiciona ao corpo da requisição todo o histórico da conversa.
            messages.AddRange(conversation.Select(x => new { role = x["role"], content = x["content"] }));

            // Define o modelo de IA e envia as mensagens.
            var body = new { model = "gpt-4o-mini", messages };

            // Serializa o corpo da requisição para JSON.
            var json = JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Envia a requisição para a OpenAI.
            var response = await httpClient.PostAsync(OPENAI_URL, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            // Caso retorne erro, lança uma exceção contendo a resposta da API.
            if (!response.IsSuccessStatusCode)
                throw new Exception(responseBody);

            // Lê a resposta recebida da IA.
            using var doc = JsonDocument.Parse(responseBody);

            // Extrai o texto retornado pelo modelo.
            var text = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            // Salva a resposta da IA no histórico do usuário.
            conversation.Add(new() { { "role", "assistant" }, { "content", text } });

            // Retorna o texto final.
            return text;
        }
    }
}
