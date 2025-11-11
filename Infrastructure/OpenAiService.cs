using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;

namespace WebChama.Infrastructure
{
    public class OpenAiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string OPENAI_API_KEY = "CHAVE_AQUI";
        private const string OPENAI_URL = "https://api.openai.com/v1/chat/completions";

        private static readonly ConcurrentDictionary<string, List<Dictionary<string, string>>> userConversations = new();

        public OpenAiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> EnviarMensagemAsync(string userId, string mensagem)
        {
            var conversation = userConversations.GetOrAdd(userId, _ => new List<Dictionary<string, string>>());
            conversation.Add(new() { { "role", "user" }, { "content", mensagem } });

            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", OPENAI_API_KEY);

            var messages = new List<object>
            {
                new { role = "system", content = @"Você é um assistente inteligente do sistema de chamados Chama Fácil. Sua função é guiar o usuário dentro do sistema, explicando de forma natural e clara o que ele deve fazer.
                    
                    ──────────────────────────────
                    👤 USUÁRIO COMUM (CLIENTE)
                    ──────────────────────────────
                    • O Cliente acessa o sistema clicando em 'Sou Cliente', e faz login informando sua Funcional e Senha.  
                    • Caso não possua cadastro, pode se registrar informando:  
                      - Nome completo;  
                      - Data de nascimento;  
                      - CPF (apenas números);  
                      - Funcional;  
                      - E-mail;  
                      - Senha;  
                      - Confirmação de senha.  

                    📌 Após o login, o Cliente é direcionado para sua área principal, onde pode:  
                      - Visualizar suas atividades;  
                      - Ver seus chamados abertos, concluídos e pendentes;  
                      - Criar um novo chamado informando a categoria e a descrição do problema;  
                      - Consultar detalhes dos chamados pendentes ou concluídos;  
                      - Ver atividades recentes, como:  
                        ▪ 'Você abriu o chamado 403';  
                        ▪ 'Chamado 002 foi concluído por Suporte TI';  
                        ▪ 'Seu chamado 105 foi atualizado para Em Andamento';  
                      - Pesquisar respostas prontas e perguntas frequentes;  
                      - Acessar seu perfil e sair da conta.  

                    📚 O Cliente também pode acessar a área de Artigos e Documentos, que contém:  
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
                    • Se o usuário quiser abrir um chamado, peça a categoria e a descrição do problema.  
                    • Se o usuário tiver dúvidas sobre o uso do sistema, explique passo a passo onde ele deve clicar e o que encontrará.  
                    • Evite linguagem técnica excessiva, priorizando clareza e simplicidade." }
            };
            messages.AddRange(conversation.Select(x => new { role = x["role"], content = x["content"] }));

            var body = new { model = "gpt-4o-mini", messages };
            var json = JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(OPENAI_URL, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception(responseBody);

            using var doc = JsonDocument.Parse(responseBody);
            var text = doc.RootElement.GetProperty("choices")[0]
                .GetProperty("message").GetProperty("content").GetString();

            conversation.Add(new() { { "role", "assistant" }, { "content", text } });
            return text;
        }
    }
}

