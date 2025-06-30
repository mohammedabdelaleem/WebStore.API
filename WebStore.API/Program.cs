using Hangfire;
using HangfireBasicAuthenticationFilter;
using Serilog;
using WebStore.API;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDependancies(builder.Configuration);

builder.Host.UseSerilog((context, configuration) =>
	configuration.ReadFrom.Configuration(context.Configuration));


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseHangfireDashboard("/jobs",
	new DashboardOptions
	{
		Authorization =
		[
			new HangfireCustomBasicAuthenticationFilter
			{
				// just 2 values no need for class and option pattern
				User = app.Configuration.GetValue<string>("HangfireSettings:UserName"),
				Pass = app.Configuration.GetValue<string>("HangfireSettings:Password")
			}
			],
		DashboardTitle = "Web Store Dashboard"
	}
	);

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
