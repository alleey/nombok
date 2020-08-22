using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nombok.CLI;
using Nombok.Core;
using Nombok.Template;
using Nombok.Template.Razor;
using System;
using System.IO;
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
#if Debug
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
            return await builder.RunCommandLineApplicationAsync<NombokCommand>(args);
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
            return 1;
         }
      }

      static void ConfigureApplicationServices(HostBuilderContext hostContext, IServiceCollection services)
      {
         services.AddSingleton<RazorTemplateEngine>();
         services.AddTransient<ITemplateEngine>(x => x.GetRequiredService<RazorTemplateEngine>());
         services.AddTransient<IRazorTemplateEngine>(x => x.GetRequiredService<RazorTemplateEngine>());

         services.Configure<RazorTemplateEngineOptions>(hostContext.Configuration.GetSection($"Templates:{RazorTemplateEngineOptions.OptionsName}"));
         services.Configure<RazorTemplateEngineOptions>(options =>
         {
            var currFolder = Directory.GetCurrentDirectory();
            options.UseMemoryCachingProvider();
         });

         services.AddTransient<GenerationContext>();
         services.Configure<GenerationContextOptions>(options =>
         {
            var currFolder = Directory.GetCurrentDirectory();
            options.UseCodebaseFolder(currFolder);
         });
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

