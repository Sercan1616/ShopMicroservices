
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);

    // ValidationBehavior<TRequest, TResponse>, herhangi bir TRequest ve TResponse ile kullanýlabilir.
    // Bu kullaným, ValidationBehavior sýnýfýný MediatR pipeline’a "açýk generic" bir yapý olarak ekler.
    // Yani, TRequest ve TResponse türleri henüz belirlenmemiþtir; isteðin geldiði durumda MediatR,
    // otomatik olarak uygun türleri burada yerine koyacaktýr. Böylece, ValidationBehavior
    // belirli bir TRequest ve TResponse çifti için deðil, herhangi bir istek ve yanýt türü çifti için çalýþýr.
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));

    // LogggingBehavior DI ile ele aldýk ve register ettik.
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

// Uygulama çalýþtýðýnda bu kýsým Assemlbly'deki validatorleri arar ve çalýþtýrýr.
// Kýsacasý, bu kod satýrý FluentValidation doðrulayýcýlarýný uygulamaya dinamik ve otomatik bir þekilde ekler,
// Böylece manuel olarak her bir doðrulayýcýyý ekleme zorunluluðundan kurtulmuþ olursun.
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

// Çalýþtýrýlan uygulama sadece Development ise dummy datayý oluþtur. 
if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

var app = builder.Build();

// Configure the HTTP request pipeline
app.MapCarter();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
