using E_commerce.Data;
using E_commerce.Interface.Payment;
using E_commerce.Repository.Payment;
using MediatR;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddMediatR(typeof(Program).Assembly);

builder.Services.AddScoped<E_commerceDB>();
builder.Services.AddScoped<IPaymentInterface, IPaymentRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<E_commerceDB>();
    await db.EnsureDatabaseObjectsAsync();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
