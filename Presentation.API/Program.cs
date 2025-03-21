using SimpleRestAPI.Infra.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SimpleRestAPI.Infra.Database.Context;
using SimpleRestAPI.Shared.Validations;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SimpleRestAPI.Shared.Shared.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<SimpleRestDB>();
builder.Services.AddDependencies();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
byte[] key = Encoding.ASCII.GetBytes("g`jpOR(*;?y%oSimpleCRUDoCEE?]mA6SR2e-rfSimpleCRUD[di2rhf[:eDl}H)I6IALP`RN}SimpleCRUD`spq*;y");
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
//builder.AddSqlServerClient(connectionName: "SimpleRestDB");
builder.AddSqlServerDbContext<SimpleRestDB>("SimpleRestDB");
//builder.Services.AddDbContext<SimpleRestDB>(options => options.UseSqlServer(AppSettings.ConnectionString.ToString()));

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Cors
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());


FluentValidationConfig.ConfigureFluentValidation();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetService<SimpleRestDB>();
    await dbContext.Database.MigrateAsync();
}
// Execute: dotnet ef --startup-project ..\Presentation.API\Presentation.API.csproj migrations add <InitialMigration>
app.Run();
