using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;
using Microsoft.Extensions.Logging;
using Nombok.Shared;
using Nombok.Shared.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;

namespace Nombok.Core.Codebase
{
    public class DefaultFileSystemProvider : IFileSystemProvider
    {
        private readonly ILogger _logger;

        public DefaultFileSystemProvider(ILogger<DefaultFileSystemProvider> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IEnumerable<string> Enumerate(FileSearchOptions options)
        {
            options = options ?? throw new ArgumentNullException(nameof(options));

            var matcher = new Matcher(options.ComparsionOption);
            matcher.AddIncludePatterns(options.IncludePatterns);
            matcher.AddExcludePatterns(options.ExcludePatterns);

            var matched = matcher.Execute(GetDirectory(options.BaseFolder));
            if (!matched.HasMatches)
            {
                _logger.LogInformation($"No files found matching patterns");
            }
            else foreach (var found in matched.Files)
            {
                yield return found.Path;
            }
        }

        protected virtual DirectoryInfoBase GetDirectory(string path) 
            => new DirectoryInfoWrapper(new DirectoryInfo(path));
    }
}
