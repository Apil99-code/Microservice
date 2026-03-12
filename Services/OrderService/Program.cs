using E_commerce.Data;
using E_commerce.Integrations.Users;
using E_commerce.Interface;
using E_commerce.Interface.OrderItems;
using E_commerce.Repository.OrderItem;
using E_commerce.Repository.Orders;
using MediatR;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddMediatR(typeof(Program).Assembly);

builder.Services.AddScoped<E_commerceDB>();
builder.Services.AddScoped<IOrderInterface, IE_commerceOrdersRepository>();
builder.Services.AddScoped<IOrderItemsInterface, IOrderItemRepository>();
builder.Services.AddHttpClient<IUserValidationClient, UserValidationClient>();

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
