using Customer;
using Customer.service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<Customerservice>();
builder.Services.AddCors(o => o.AddPolicy("ReactPolicy", builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
}));
builder.Services.AddMvc()
.AddNewtonsoftJson(
options => {
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

builder.Services.AddControllers();

// Configure and enable Swagger/OpenAPI documentation
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}





app.UseCors("ReactPolicy");



app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
