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
using Nombok.Shared.FileSystem;

namespace Nombok.CLI.Nombok
{
   [Command(Name = "generate",
      Description = "run nombok processors",
      UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.CollectAndContinue,
      OptionsComparison = StringComparison.InvariantCultureIgnoreCase)]
   class GenerateNombokCommand : NombokCommandBase
   {
      private readonly IFactory<GenerationContext, GenerationContextOptions> _contextFactory;
      private readonly IFileSystemProvider _fileSystem;
      private readonly ITemplateEngine _templateEngine;

      public GenerateNombokCommand(
         IFactory<GenerationContext, GenerationContextOptions> contextFactory,
         IFileSystemProvider fileSystem,
         ITemplateEngine templateEngine,
         ILogger<GenerateNombokCommand> logger) : base(logger)
      {
         _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
         _templateEngine = templateEngine ?? throw new ArgumentNullException(nameof(templateEngine));
         _fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
      }

      [Option(CommandOptionType.SingleValue, ShortName = "f", LongName = "folder", Description = "Source directory")]
      public string InputFolder { get; } = Directory.GetCurrentDirectory();

      protected override async Task<int> DoWorkAsync(CommandLineApplication app)
      {
         WriteHost($"Running in {InputFolder}");

         var searchCodeOptions = new FileSearchOptions()
            .UseBaseFolder(InputFolder)
            .AddIncludePatterns(app.RemainingArguments);

         var result = _fileSystem.Enumerate(searchCodeOptions);
         if (!result.HasMatches)
         {
            WriteHost($"No files found matching patterns");
            return 0;
         }

         foreach(var finfo in result.Files)
         {
            WriteHost($"Processing {finfo.PhysicalPath}");
            var contextOptions = new GenerationContextOptions();
            var context = _contextFactory.Create(contextOptions);
         }

         return 0;
      }

   }
}
