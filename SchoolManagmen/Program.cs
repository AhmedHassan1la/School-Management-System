using Hangfire;

using SchoolManagmen;

using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });

builder.Services.AddDependencies(builder.Configuration);




builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration)
);
builder.Services.AddAutoMapper(typeof(Program)); // Registers all AutoMapper profiles in the assembly.


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var descriptions = app.DescribeApiVersions();
        foreach (var description in descriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    }
    );
}
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseHangfireDashboard("/jobs");

app.UseCors();
app.UseExceptionHandler();
app.UseAuthorization();
app.UseRateLimiter();

app.MapControllers();

app.Run();

