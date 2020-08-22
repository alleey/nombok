using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Nombok.CLI
{
   [HelpOption("--help")]
   class NombokCommandBase
   {
      private readonly ILogger _logger;
      private readonly IConsole _console;

      public NombokCommandBase(ILogger<NombokCommandBase> logger, IConsole console)
      {
         _logger = logger ?? throw new ArgumentNullException(nameof(logger));
         _console = console ?? throw new ArgumentNullException(nameof(console));
      }

      protected virtual Task<int> OnExecuteAsync(CommandLineApplication app)
      {
         // this shows help even if the --help option isn't specified
         app.ShowHelp();
         return Task.FromResult(0);
      }

      protected void WriteDebug(string message, params object[] args)
      {
         _logger.LogDebug(message, args);
      }

      protected void WriteHost(string message, params object[] args)
      {
         _logger.LogInformation(message, args);
      }

      protected void WriteWarning(string message, params object[] args)
      {
         _logger.LogWarning(message, args);
      }

      protected void WriteException(Exception ex)
      {
         _logger.LogError(ex.Message);
      }
   }
}
