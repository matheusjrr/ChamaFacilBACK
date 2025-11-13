using WebChama.Infrastructure;
using WebChama.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddTransient<IChamadoRepository, ChamadoRepository>();
builder.Services.AddTransient<IDesignacao_tecnicoRepository, Designacao_tecnicoRepository>();
builder.Services.AddTransient<IEspecialidadeRepository, EspecialidadeRepository>();
builder.Services.AddTransient<ISetorRepository, SetorRepository>();
builder.Services.AddTransient<ITecnicoRepository, TecnicoRepository>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
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
builder.Services.AddHttpClient();
builder.Services.AddSingleton<WebChama.Infrastructure.OpenAiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("PermitirTudo");

app.Run();