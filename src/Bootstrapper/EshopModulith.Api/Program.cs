
var builder = WebApplication.CreateBuilder(args);

//Add services to the container.

builder.Services.AddCarterModulesFromAssemblies(typeof(CatalogModule).Assembly);

builder.Services
    .AddBasketModule(builder.Configuration)
    .AddCatalogModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);

var app = builder.Build();

//Configure the HTTP request pipeline.

app.MapCarter();

app.UseBasketModule()
   .UseCatalogModule()
   .UseOrderingModule();

app.Run();
