using btlz.Services;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddExceptionHandler<ExceptionHandler>();

var app = builder.Build();

app.UseExceptionHandler("/error");

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();