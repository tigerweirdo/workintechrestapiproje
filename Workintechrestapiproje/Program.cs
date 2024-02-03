
using Workintechrestapiproje.Business;
using Workintechrestapiproje.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//currency service resolver

#region Scrutor resolvers

var typeBaseService = typeof(BaseService);

var assembly = typeBaseService.Assembly;

builder.Services.Scan(selector =>
        selector
            .FromAssemblies(assembly)
            .AddClasses(classSelector => classSelector.AssignableTo(typeof(BaseService)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );



var singletonBaseAssembly = typeof(BaseSingletonService).Assembly;
builder.Services.Scan(selector =>
        selector
            .FromAssemblies(singletonBaseAssembly)
            .AddClasses(classSelector => classSelector.AssignableTo(typeof(BaseSingletonService)))
            .AsImplementedInterfaces()
            .WithSingletonLifetime()
        );

#endregion

var app = builder.Build();

app.UseTimeElapsedCalculate();


#region SerilogConfiguration

var logger = new LoggerConfiguration()
#if DEBUG
    .MinimumLevel.Information()
#endif
#if RELEASE
.MinimumLevel.Error()
#endif
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

Log.Logger = logger;

#endregion


// Configure the HTTP request p
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCustomException();

app.Run();