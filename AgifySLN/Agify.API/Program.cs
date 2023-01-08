using Agify.BL;
using Agify.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();
builder.Services.AddBLSerivce();
builder.Services.AddDALService();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();
app.MapHealthChecks("/health");

if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
