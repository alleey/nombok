using System;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nombok.Core;
using Nombok.Template;
using Nombok.Template.Razor;

namespace Nombok.CLI
{
   [Command(Name = "generate",
    Description = "run nombok processors",
    UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.CollectAndContinue,
    OptionsComparison = StringComparison.InvariantCultureIgnoreCase)]
  class GenerateNombokCommand : NombokCommandBase
  {
    GenerationContext _context;
    ITemplateEngine _templateEngine;

    public GenerateNombokCommand(GenerationContext context,
      ITemplateEngine templateEngine,
      ILogger<GenerateNombokCommand> logger,
      IOptions<RazorTemplateEngineOptions> options,
      IConsole console) : base(logger, console)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
      _templateEngine = templateEngine ?? throw new ArgumentNullException(nameof(templateEngine));

      console.WriteLine(string.Join(",", options.Value.Namespaces));
      console.WriteLine(string.Join(",", options.Value.Locations));
    }

    protected override Task<int> OnExecuteAsync(CommandLineApplication app)
    {
      // this shows help even if the --help option isn't specified
      foreach(var i in app.RemainingArguments)
        Console.WriteLine($"remaining {i}");
      return Task.FromResult(0);
    }

  }
}
