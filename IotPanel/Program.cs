using DatabaseProject.DatabaseContext;
using DatabaseProject.Interface;
using DatabaseProject.Repositories;
using IotPanel.NewFolder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
const string apiCorsPolicy = "ApiCorsPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: apiCorsPolicy,
                      builder =>
                      {
                          builder //.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials()
                            .SetIsOriginAllowed(hostName => true);
                          //.WithMethods("OPTIONS", "GET");
                      });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SqlServerContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
builder.Services.AddScoped<NewPanelRepository, PanelRepository>();
builder.Services.AddSingleton<MQTTHub>();
builder.Services.AddHostedService<MQTTHub>(q => q.GetRequiredService<MQTTHub>());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(apiCorsPolicy);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
