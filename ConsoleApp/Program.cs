using DbUp;
using DbUp.ScriptProviders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ConsoleApp;
public class Program
{
  /// <summary>
  /// The main console application entry point
  /// </summary>
  public static async Task Main(string[] args)
  {
    // Set the synchronization context to null to disable context capturing
    SynchronizationContext.SetSynchronizationContext(null);

    // Get configurations
    var configurations = GetConfiguration();

    // Create DI Container
    CreateDependencyInjectionContainer(args);

    // Run DbUp to deploy scripts
    DeploySqlScripts(configurations);

    // Prevent application closure
    Console.ReadLine();
  }

  private static void CreateDependencyInjectionContainer(string[] args)
  {
    using var host = Host.CreateDefaultBuilder(args)
      .ConfigureServices(services =>
      {
        // Register Domain Services
        // TODO

        // Register Application Services
        // TODO
      })
      .Build();
  }

  public static IConfigurationRoot? GetConfiguration()
  {
    // Create configurations
    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    var settingsFile = string.IsNullOrWhiteSpace(env) ? "appsettings.json" : $"appsettings.{env}.json";
    var configuration = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile(settingsFile)
      .Build();
    return configuration;
  }

  public static void DeploySqlScripts(IConfigurationRoot? configurations)
  {
    // DB up configurations
    var connectionString = configurations.GetValue<string>("ConnectionStrings:DatabaseConnection");
    var scriptFolderBase = configurations.GetValue<string>("DbUp:MigrationScriptsFolder");
    var scriptOptions = new FileSystemScriptOptions { IncludeSubDirectories = true };

    // Get ReleaseNotes folder path
    var solutionFolderPath = Enumerable.Range(1, 4).Aggregate(Directory.GetCurrentDirectory(), (current, _) => Directory.GetParent(current).FullName);
    var scriptsPath = Path.Combine(solutionFolderPath, scriptFolderBase);

    // Run DbUp upgrader to deploy scripts
    var upgrader = DeployChanges.To
      .SqlDatabase(connectionString)
      .WithScripts(new FileSystemScriptProvider(scriptsPath, scriptOptions))
      .LogToConsole()
      .Build()
      .PerformUpgrade();

    // Handle DbUp Failures
    if (upgrader.Successful == false)
    {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine("Database migration failed:");
      Console.WriteLine(upgrader.Error);
      Console.ResetColor();
      throw new Exception("Database migration failed");
    }

    // Handle DbUp Success
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Database migration successful!");
    Console.ResetColor();
    return;
  }
}