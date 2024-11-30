using MOHU.Integration.WebApi;
using MOHU.Integration.WebApi.Common.Security.AzureKeyVault;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.ConfigureAzureKeyVault();

builder.Services.AddInternalModules(builder.Configuration);

var app = builder.Build();

app.UseInternalModules();

app.Run();