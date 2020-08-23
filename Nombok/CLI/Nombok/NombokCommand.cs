using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Nombok.CLI.Nombok
{
   [Command(Name = "nombok",
       UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.CollectAndContinue,
       OptionsComparison = StringComparison.InvariantCultureIgnoreCase)]
   [VersionOptionFromMember("--version", MemberName = nameof(GetVersion))]
   [Subcommand(typeof(GenerateNombokCommand))]
   class NombokCommand : NombokCommandBase
   {
      public NombokCommand(ILogger<NombokCommand> logger, IConsole console) : base(logger, console)
      {
      }

      protected override Task<int> OnExecuteAsync(CommandLineApplication app)
      {
         // this shows help even if the --help option isn't specified
         //app.ShowHelp();
         return Task.FromResult(0);
      }

      private static string GetVersion()
          => typeof(NombokCommand).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

   }
}
