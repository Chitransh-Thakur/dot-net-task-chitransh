using dot_net_task_form_generation.Models.Creds;
using dot_net_task_form_generation.Services;
using dot_net_task_form_generation.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<CosmosDbCreds>(builder.Configuration.GetSection("CosmosDbCreds"));
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<ICandidateInfoService, CandidateInfoService>();
builder.Services.AddScoped<IProgramService, ProgramService>();
builder.Services.AddMemoryCache();

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

app.Run();
