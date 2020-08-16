using System;
using System.Collections.Generic;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Nombok.Core
{
  public class GenerationContext
  {
    private IFileProvider _codebase;
    private ILogger _logger;

    public GenerationContext(IOptions<GenerationContextOptions> options, ILogger<GenerationContext> logger)
    {
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      _codebase = options.Value.BuildCodebaseProvider();
      _logger.LogInformation($"GenerationContext '{_codebase}'");
    }

    public IFileProvider Codebase => _codebase;

  }
}

