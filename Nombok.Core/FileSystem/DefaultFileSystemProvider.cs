using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;
using Microsoft.Extensions.Logging;
using Nombok.Shared;
using Nombok.Shared.FileSystem;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Nombok.Core.FileSystem
{
    public class DefaultFileSystemProvider : IFileSystemProvider
    {
        private readonly IFactory<IFileProvider, string> _fileProviderFactory;

        public DefaultFileSystemProvider(IFactory<IFileProvider, string> fileProviderFactory)
        {
            _fileProviderFactory = fileProviderFactory ?? throw new ArgumentNullException(nameof(fileProviderFactory));
        }

        public FileSearchResult Enumerate(FileSearchOptions options)
        {
            options = options ?? throw new ArgumentNullException(nameof(options));

            var matcher = new Matcher(options.ComparsionOption);
            matcher.AddIncludePatterns(options.IncludePatterns);
            matcher.AddExcludePatterns(options.ExcludePatterns);

            var matched = matcher.Execute(GetDirectory(options.BaseFolder));
            if (!matched.HasMatches)
            {
                return new FileSearchResult();
            }

            var fileProvider = _fileProviderFactory.Create(options.BaseFolder);
            return new FileSearchResult(
               matched.Files.Select(x => fileProvider.GetFileInfo(x.Path)),
               matched.HasMatches
            );
        }

        protected virtual DirectoryInfoBase GetDirectory(string path) 
            => new DirectoryInfoWrapper(new DirectoryInfo(path));
    }
}
