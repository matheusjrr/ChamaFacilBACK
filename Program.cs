using WebChama.Infrastructure;
using WebChama.Model;

// Cria o builder da aplicação, responsável por configurar serviços e pipeline
var builder = WebApplication.CreateBuilder(args);

// ==================== Adicionando serviços ==================== //

// Adiciona suporte a controllers (API REST)
builder.Services.AddControllers();

// Adiciona suporte a Swagger/OpenAPI para documentação e teste de endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ==================== Injeção de dependências ==================== //
// Registra os repositórios no contêiner de serviços para injeção automática
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddTransient<IChamadoRepository, ChamadoRepository>();
builder.Services.AddTransient<IDesignacao_tecnicoRepository, Designacao_tecnicoRepository>();
builder.Services.AddTransient<IEspecialidadeRepository, EspecialidadeRepository>();
builder.Services.AddTransient<ISetorRepository, SetorRepository>();
builder.Services.AddTransient<ITecnicoRepository, TecnicoRepository>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

// Configuração do CORS (Cross-Origin Resource Sharing)
// Define a política "PermitirTudo", permitindo qualquer origem, cabeçalho e método HTTP
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Adiciona suporte a HttpClient para realizar requisições HTTP externas
builder.Services.AddHttpClient();

// Registra o serviço OpenAiService como Singleton, garantindo que haja apenas uma instância
builder.Services.AddSingleton<WebChama.Infrastructure.OpenAiService>();

// ==================== Construção da aplicação ==================== //
var app = builder.Build();

// ==================== Configuração do pipeline ==================== //

// Habilita Swagger apenas em ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Força redirecionamento de HTTP para HTTPS
app.UseHttpsRedirection();

// Adiciona middleware de autorização (para endpoints protegidos)
app.UseAuthorization();

// Mapeia controllers para rotas da API
app.MapControllers();

// Aplica a política de CORS definida anteriormente
app.UseCors("PermitirTudo");

// Inicializa a aplicação e começa a ouvir requisições
app.Run();
