using System;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace Nombok.CLI
{
    [HelpOption("--help")]
    class NombokCommandBase
    {
        protected ILogger _logger;
        protected IConsole _console;

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

        protected void OnException(Exception ex)
        {
            _logger.LogError(ex.Message);
            _logger.LogDebug(ex, ex.Message);
        }
    }
}
