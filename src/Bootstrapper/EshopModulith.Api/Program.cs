
using Keycloak.AuthServices.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration)
);

//Add services to the container.


//Common services for all modules (Carter,Mediater,FluentValidation)
var catalogAssembly = typeof(CatalogModule).Assembly;
var basketAssembly = typeof(BasketModule).Assembly;
var orderingAssembly = typeof(OrderingModule).Assembly;

// Register Carter modules from multiple assemblies
builder.Services.AddCarterModulesFromAssemblies(catalogAssembly, basketAssembly, orderingAssembly);
// Register MediatR handlers and behaviors from multiple assemblies
builder.Services.AddMediatRWithAssemblies(catalogAssembly, basketAssembly, orderingAssembly);
// Register FluentValidation validators from multiple assemblies
builder.Services.AddValidatorsFromAssemblies([catalogAssembly, basketAssembly, orderingAssembly]); //Can Be to AddMediatRWith.. 

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddMassTransitWithAssemblies(builder.Configuration, catalogAssembly, basketAssembly,orderingAssembly);

builder.Services.AddKeycloakWebApiAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services
    .AddBasketModule(builder.Configuration)
    .AddCatalogModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

//Configure the HTTP request pipeline.

app.MapCarter();
app.UseExceptionHandler(options => { }); // the empty options to use our custom exception handler
app.UseAuthentication();
app.UseAuthorization();
app.UseSerilogRequestLogging();

app.UseCatalogModule()
   .UseBasketModule()
   .UseOrderingModule();


app.Run();
