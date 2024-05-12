using Core.Data.DataContext;
using Core.Hubs;
using FYP__.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
new ServiceConfiguration(builder.Services);
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
// Use CORS middleware with specific settings
app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:3000", "http://localhost:7100", "http://localhost:5173")
    .AllowCredentials()
    
           .AllowAnyHeader()
           .AllowAnyMethod();
});
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapGet("/", () => "Hello World!");
app.MapHub<ChatHub>("/chat");
app.MapControllers();

app.Run();
