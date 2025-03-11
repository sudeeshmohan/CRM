using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Usermanagement.Application.Extensions;
using Usermanagement.Infrastructure.Data;
using Usermanagement.Infrastructure.Data.Interceptor;
using Usermanagement.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.SetMinimumLevel(LogLevel.Debug);
});
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
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options =>
   {
       options.Authority = "https://localhost:5001";
       options.RequireHttpsMetadata = false;

       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = false, // Disable explicit audience check
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
       };
   });
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
//app.UseExceptionHandler();

app.Run();
