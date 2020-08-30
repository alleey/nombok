using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nombok.Core;
using Nombok.Shared;
using Nombok.Template;
using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace Nombok.Core.Codebase
{

   public class DefaultCodebaseProvider : ICodebaseProvider
   {
      private readonly IFactory<IFileProvider, string> _fileProviderFactory;
      private readonly ILogger _logger;

      public DefaultCodebaseProvider(
         IFactory<IFileProvider, string> fileProviderFactory,
         ILogger<DefaultCodebaseProvider> logger)
      {
         _fileProviderFactory = fileProviderFactory ?? throw new ArgumentNullException(nameof(fileProviderFactory));
         _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      }

      public void Enumerate(string folder, CodebaseSearchOptions options, Action<IFileInfo> handler)
      {
         folder = folder ?? throw new ArgumentNullException(nameof(folder));
         options = options ?? throw new ArgumentNullException(nameof(options));
         handler = handler ?? throw new ArgumentNullException(nameof(handler)); 

         var matcher = new Matcher(options.ComparsionOption);
         matcher.AddIncludePatterns(options.IncludePatterns);
         matcher.AddExcludePatterns(options.ExcludePatterns);

         var matched = matcher.Execute(new DirectoryInfoWrapper(new DirectoryInfo(folder)));
         if (!matched.HasMatches)
         {
            _logger.LogInformation($"No files found matching patterns");
            return;
         }

         var fileProvider = _fileProviderFactory.Create(folder);
         foreach (var found in matched.Files)
         {
            var fileInfo = fileProvider.GetFileInfo(found.Path);
            handler(fileInfo);
         }
      }
   }
}
