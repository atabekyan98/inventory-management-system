using InventoryManagement.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configure services using extension methods
builder.ConfigureServices()
       .AddAuthenticationWithJwt()
       .AddAuthorizationPolicies()
       .AddAutoMapperConfig()
       .AddApiVersioningConfig()
       .AddSwaggerConfig()
       .AddHealthChecksConfig();

var app = builder.Build();

// Configure the HTTP request pipeline
app.ConfigurePipeline();

app.Run();

// Added for testing
public partial class Program { }