using WebStore.API;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDependancies(builder.Configuration);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
