using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nombok.Core;
using Nombok.Shared;
using Nombok.Template;
using Nombok.Template.Razor;
using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace Nombok.CLI.Nombok
{
   [Command(Name = "generate",
      Description = "run nombok processors",
      UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.CollectAndContinue,
      OptionsComparison = StringComparison.InvariantCultureIgnoreCase)]
   class GenerateNombokCommand : NombokCommandBase
   {
      private readonly IFactory<GenerationContext, GenerationContextOptions> _contextFactory;
      private readonly ITemplateEngine _templateEngine;
      private readonly IFactory<IFileProvider, string> _fileProviderFactory;

      public GenerateNombokCommand(
               IFactory<GenerationContext, GenerationContextOptions> contextFactory,
               IFactory<IFileProvider, string> fileProviderFactory,
               ITemplateEngine templateEngine,
               ILogger<GenerateNombokCommand> logger,
               IConsole console) : base(logger, console)
      {
         _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
         _templateEngine = templateEngine ?? throw new ArgumentNullException(nameof(templateEngine));
         _fileProviderFactory = fileProviderFactory ?? throw new ArgumentNullException(nameof(fileProviderFactory));
      }

      [Option(CommandOptionType.SingleValue, ShortName = "f", LongName = "folder", Description = "Source directory")]
      public string InputFolder { get; } = Directory.GetCurrentDirectory();

      protected override async Task<int> OnExecuteAsync(CommandLineApplication app)
      {
         WriteHost($"Running in {InputFolder}");
         var matcher = new Matcher(StringComparison.InvariantCultureIgnoreCase);
         foreach (var i in app.RemainingArguments)
            matcher.AddInclude(i);

         var matched = matcher.Execute(new DirectoryInfoWrapper(new DirectoryInfo(InputFolder)));
         if (!matched.HasMatches)
         {
            WriteHost($"No files found matching patterns");
            return 0;
         }

         var fileProvider = _fileProviderFactory.Create(InputFolder);
         foreach(var found in matched.Files)
         {
            var fileInfo = fileProvider.GetFileInfo(found.Path);

            WriteHost($"Processing {fileInfo.PhysicalPath}");
            var contextOptions = new GenerationContextOptions();
            var context = _contextFactory.Create(contextOptions);
         }

         return 0;
      }

   }
}
