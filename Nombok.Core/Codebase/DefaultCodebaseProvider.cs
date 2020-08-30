using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.Logging;
using Nombok.Shared;
using Nombok.Shared.Codebase;
using Nombok.Shared.FileSystem;
using System;
using System.Collections.Generic;

namespace Nombok.Core.Codebase
{

    public class DefaultCodebaseProvider : ICodebaseProvider
   {
      private readonly IFileSystemProvider _fileSystemProvider;
      private readonly IFileProvider _fileProvider;
      private readonly ILogger _logger;

      public DefaultCodebaseProvider(
         IFileSystemProvider fileSystemProvider,
         IFileProvider fileProvider,
         ILogger<DefaultCodebaseProvider> logger)
      {
         _fileSystemProvider = fileSystemProvider ?? throw new ArgumentNullException(nameof(fileSystemProvider));
         _fileProvider = fileProvider ?? throw new ArgumentNullException(nameof(fileProvider));
         _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      }

      public IEnumerable<IFileInfo> Enumerate(FileSearchOptions options)
      {
         foreach (var found in _fileSystemProvider.Enumerate(options))
         {
            yield return _fileProvider.GetFileInfo(found);
         }
      }
   }
   // public class DefaultCodebaseProvider : ICodebaseProvider
   // {
   //    private readonly IFactory<IFileProvider, string> _fileProviderFactory;
   //    private readonly ILogger _logger;

   //    public DefaultCodebaseProvider(
   //       IFactory<IFileProvider, string> fileProviderFactory,
   //       ILogger<DefaultCodebaseProvider> logger)
   //    {
   //       _fileProviderFactory = fileProviderFactory ?? throw new ArgumentNullException(nameof(fileProviderFactory));
   //       _logger = logger ?? throw new ArgumentNullException(nameof(logger));
   //    }

   //    public void Enumerate(string folder, CodebaseSearchOptions options, Action<IFileInfo> handler)
   //    {
   //       folder = folder ?? throw new ArgumentNullException(nameof(folder));
   //       options = options ?? throw new ArgumentNullException(nameof(options));
   //       handler = handler ?? throw new ArgumentNullException(nameof(handler)); 

   //       var matcher = new Matcher(options.ComparsionOption);
   //       matcher.AddIncludePatterns(options.IncludePatterns);
   //       matcher.AddExcludePatterns(options.ExcludePatterns);

   //       var matched = matcher.Execute(new DirectoryInfoWrapper(new DirectoryInfo(folder)));
   //       if (!matched.HasMatches)
   //       {
   //          _logger.LogInformation($"No files found matching patterns");
   //          return;
   //       }

   //       var fileProvider = _fileProviderFactory.Create(folder);
   //       foreach (var found in matched.Files)
   //       {
   //          var fileInfo = fileProvider.GetFileInfo(found.Path);
   //          handler(fileInfo);
   //       }
   //    }
   // }
}
