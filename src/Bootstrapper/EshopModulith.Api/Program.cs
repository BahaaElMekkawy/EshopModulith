
var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration)
);
//Add services to the container.

builder.Services.AddCarterModulesFromAssemblies(typeof(CatalogModule).Assembly);

builder.Services
    .AddBasketModule(builder.Configuration)
    .AddCatalogModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

//Configure the HTTP request pipeline.

app.MapCarter();
app.UseExceptionHandler(options => { }); // the empty options to use our custom exception handler
app.UseSerilogRequestLogging();

app.UseBasketModule()
   .UseCatalogModule()
   .UseOrderingModule();


app.Run();
