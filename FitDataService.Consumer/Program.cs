using FitDataService.Consumer.Extensions;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.ServiceCollectionConfiguration();

var host = builder.Build();
host.Run();