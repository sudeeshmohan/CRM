using Microsoft.EntityFrameworkCore;
using Usermanagement.Application.Extensions;
using Usermanagement.Infrastructure.Data;
using Usermanagement.Infrastructure.Data.Interceptor;
using Usermanagement.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.AddInterceptors(new AuditableEntityInterceptor());
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
//builder.Services.AddMediatR(typeof(Program));
builder.Services.ApplicationServiceCollections();
builder.Services.InsfrastructureServiceCollections();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
//app.UseExceptionHandler();

app.Run();
