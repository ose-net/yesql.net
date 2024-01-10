using CPlugin.Net;
using DotEnv.Core;
using PluginApp.Contracts;
using System.Reflection;
using YeSql.Net;

var builder = WebApplication.CreateBuilder(args);

// Load .env file.
new EnvLoader()
    .AllowOverwriteExistingVars()
    .Load();

// Load plugins from the .env file.
var envConfiguration = new CPluginEnvConfiguration();
PluginLoader.Load(envConfiguration);

// Add services to the container.
var startups = TypeFinder.FindSubtypesOf<IPluginStartup>();
foreach (IPluginStartup startup in startups)
    startup.ConfigureServices(builder.Services);

// Adds the plugin assemblies as part of the application.
// Allows to register controllers of each plugin.
var mvcBuilder = builder.Services.AddControllers();
foreach (Assembly assembly in PluginLoader.Assemblies)
    mvcBuilder.AddApplicationPart(assembly);

var sqlLoader = new YeSqlLoader();
// Load SQL files from host application.
var sqlStatements = sqlLoader.Load();

// Load SQL files from plugins.
// Not all plugins need to have SQL files.
var directories = envConfiguration
    .GetPluginFiles()
    .Select(Path.GetDirectoryName)
    .ToArray();
sqlLoader.LoadFromDirectories(directories);

builder.Services.AddSingleton(sqlStatements);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
