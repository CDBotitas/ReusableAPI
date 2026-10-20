using ReusableAPI;

var builder = WebApplication.CreateBuilder(args);

// calling the ModuleCharge because we need to load the modulers from the Modules Folder

ModuleCharge.LoadModules(builder.Environment.ContentRootPath, builder.Services);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
