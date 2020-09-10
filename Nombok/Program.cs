using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nombok.CLI.Nombok;
using Nombok.Core;
using Nombok.Core.Factories;
using Nombok.Core.FileSystem;
using Nombok.Shared;
using Nombok.Shared.Codebase;
using Nombok.Shared.FileSystem;
using Nombok.Template;
using Nombok.Template.Razor;
using Nombok.Template.Razor.Configuration;
using System;
using System.Threading.Tasks;

namespace Nombok
{
    class Program
   {
      private static async Task<int> Main(string[] args)
      {
         var builder = new HostBuilder()
           .ConfigureHostConfiguration((config) =>
           {
              config
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
#if DEBUG
					.AddJsonFile("appsettings.debug.json", optional: true, reloadOnChange: false)
#endif
                .AddEnvironmentVariables(prefix: "NOMBOK_");
           })
           .ConfigureLogging((hostingContext, logging) =>
           {
              logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
              logging.AddConsole();
           })
           .ConfigureServices(ConfigureApplicationServices);

         try
         {
            return await builder.RunCommandLineApplicationAsync<NombokCommand>(args).ConfigureAwait(false);
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
            return 1;
         }
      }

      static void ConfigureApplicationServices(HostBuilderContext hostContext, IServiceCollection services)
      {
         services.Configure<RazorTemplateEngineOptions>(options =>
         {
            var configSection = $"Templates:{RazorTemplateEngineConfig.DefaultSectionName}";
            var config = hostContext.Configuration.GetSection(configSection).Get<RazorTemplateEngineConfig>();

            config.BaseLocation = config.BaseLocation ?? AppDomain.CurrentDomain.BaseDirectory;
            options.AddOptionsFromConfig(config);
            options.UseMemoryCachingProvider();
         });

         services.AddSingleton<IFactory<IFileProvider, string>, FileProviderFactory>();
         services.AddSingleton<IFactory<GenerationContext, GenerationContextOptions>, GenerationContextFactory>();
         services.AddSingleton<IFileSystemProvider, DefaultFileSystemProvider>();

         services.AddTransient<RazorTemplateEngine>();
         services.AddTransient<ITemplateEngine>(x => x.GetRequiredService<RazorTemplateEngine>());
         services.AddTransient<IRazorTemplateEngine>(x => x.GetRequiredService<RazorTemplateEngine>());
      }

      // public static SyntaxNode GenerateViewModel(SyntaxNode node)
      // {
      //     // Find the first class in the syntax node
      //     var classNode = node.DescendantNodes().OfType<ClassDeclarationSyntax>().FirstOrDefault();
      //     if (classNode != null)
      //     {
      //         Console.WriteLine(classNode.FindAttribute("NombokValue"));
      //         foreach(var member in classNode.Properties())
      //         {
      //             member.Debug();
      //         }

      //         // Get the name of the model class
      //         string modelClassName = classNode.Identifier.Text;
      //     }
      //     return node;
      // }
   }
}

