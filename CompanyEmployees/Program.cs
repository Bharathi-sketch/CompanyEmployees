using CompanyEmployees.Extensions;

using Microsoft.AspNetCore.HttpOverrides;
using NLog;
var builder = WebApplication.CreateBuilder(args);
//ILogger logs
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers().AddApplicationPart(typeof(CompanyEmployees.Presentation.AssemblyReference).Assembly); 
var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);
// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
    app.UseHsts();

app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseHttpsRedirection();

app.UseAuthorization();


////Middlewares uses 'USE'
//app.Use(async (context, next) =>
//{
//    Console.WriteLine($"Logic before executing the next delegate in the use method");
//    await next.Invoke();
//    Console.WriteLine($"Logic after executing the next delegate in the use method");
//});
////Executing the branch using Map
//app.Map("/usingmapbranch", builder =>
//{
//    builder.Use(async (context, next) =>
//    {
//        Console.WriteLine($"Map branch logic in the Use method before the next delegate");
//        await next.Invoke();
//        Console.WriteLine($"Map branch logic in the Use method after the next delegate");
//    });
//    builder.Run(async context =>
//    {
//        Console.WriteLine($"Map branch Response to the client in the Run Method");
//        await context.Response.WriteAsync("Hello from the map branch");
//    });
//});
//app.MapWhen(context => context.Request.Query.ContainsKey("testqueryString"), builder =>
//{
//    builder.Run(async context =>
//    {
//        await context.Response.WriteAsync("Hello from the MapWhen branch");
//    });
//});

////Terminal middle wares uses Run
//app.Run(async context =>
//{
//    Console.WriteLine($"Writing the response to the the client in the Run method.");
//    await context.Response.WriteAsync("Hello from the middle ware component");
//});

app.MapControllers();

app.Run();
