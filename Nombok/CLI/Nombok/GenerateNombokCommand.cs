using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nombok.Core;
using Nombok.Template;
using Nombok.Template.Razor;
using System;
using System.Threading.Tasks;

namespace Nombok.CLI.nombok
{
   [Command(Name = "generate",
   Description = "run nombok processors",
   UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.CollectAndContinue,
   OptionsComparison = StringComparison.InvariantCultureIgnoreCase)]
   class GenerateNombokCommand : NombokCommandBase
   {
      private readonly GenerationContext _context;
      private readonly ITemplateEngine _templateEngine;

      public GenerateNombokCommand(GenerationContext context,
                ITemplateEngine templateEngine,
                ILogger<GenerateNombokCommand> logger,
                IConsole console) : base(logger, console)
      {
         _context = context ?? throw new ArgumentNullException(nameof(context));
         _templateEngine = templateEngine ?? throw new ArgumentNullException(nameof(templateEngine));
      }

      protected override async Task<int> OnExecuteAsync(CommandLineApplication app)
      {
         foreach (var i in app.RemainingArguments)
         {
            var fileInfo = _context.Codebase.GetFileInfo(i);
            if (!fileInfo.Exists)
            {
               WriteWarning($"File {i} doesn't exist!");
               continue;
            }

            WriteHost($"Processing {fileInfo.Exists}");
         }
         return 0;
      }

   }
}
