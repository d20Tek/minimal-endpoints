global using D20Tek.Minimal.Endpoints;
global using D20Tek.Minimal.Endpoints.Exceptions;
using D20Tek.Sample.Api.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IKeyValueRepository, KeyValueRepository>();

// Add service endpoints to the container
builder.Services.AddApiEndpoints();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable endpoint global exception handler
app.UseExceptionHandler<EndpointExceptionHandler>();

// Enable logging for all endpoints
app.UseApiEndpointLogging();

// Map all registered endpoints
app.MapApiEndpoints();

app.Run();
