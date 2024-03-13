using AzureTest2.Data; // Adjust if your actual namespace is different
using Microsoft.EntityFrameworkCore;
using AzureTest2.Models; // If your models are in a separate namespace, make sure this is correct

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext configuration
builder.Services.AddDbContext<MyAzureContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDbConnection")));

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS with the "AllowAll" policy
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();