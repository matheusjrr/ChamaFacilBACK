using WebChama.Infrastructure;
using WebChama.Model;

var builder = WebApplication.CreateBuilder(args);

// Adiciona suporte aos controllers (API)
builder.Services.AddControllers();

//Swagger 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registro dos repositórios no container de injeção de dependência.
// AddTransient = cria uma nova instância sempre que for solicitado.
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddTransient<IChamadoRepository, ChamadoRepository>();
builder.Services.AddTransient<IDesignacao_tecnicoRepository, Designacao_tecnicoRepository>();
builder.Services.AddTransient<IEspecialidadeRepository, EspecialidadeRepository>();
builder.Services.AddTransient<ISetorRepository, SetorRepository>();
builder.Services.AddTransient<ITecnicoRepository, TecnicoRepository>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

// Configuração do CORS (libera acesso da aplicação front-end)
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo", policy =>
    {
        policy
            .AllowAnyOrigin()  // Permite qualquer domínio acessar
            .AllowAnyHeader()  // Permite qualquer header
            .AllowAnyMethod(); // Permite qualquer método(GET, POST, PUT, DELETE, etc.)
    });
});

// Registro do HttpClient, usado para requisições externas (como a API da OpenAI)
builder.Services.AddHttpClient();

// Serviço da IA registrado como Singleton:
// Uma única instância é criada e usada durante toda a aplicação
builder.Services.AddSingleton<WebChama.Infrastructure.OpenAiService>();

var app = builder.Build();

// Exibe Swagger apenas em ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redireciona HTTP -> HTTPS automaticamente
app.UseHttpsRedirection();

// Middleware de autorização (caso use autenticação no futuro)
app.UseAuthorization();

// Mapeia os controllers (rotas da API)
app.MapControllers();

// Ativa o CORS com a política definida acima
app.UseCors("PermitirTudo");

// Inicia a aplicação
app.Run();
