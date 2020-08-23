using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace Nombok.Core
{
   public class GenerationContext
   {
      private GenerationContextOptions _options;
      private ILogger _logger;

      public GenerationContext(IOptions<GenerationContextOptions> options, ILogger<GenerationContext> logger)
         : this((options ?? throw new ArgumentNullException(nameof(options))).Value, logger)
      {
      }

      public GenerationContext(GenerationContextOptions options, ILogger<GenerationContext> logger)
      {
         _options = options ?? throw new ArgumentNullException(nameof(options));
         _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      }

      public GenerationContextOptions Options => _options;
   }
}

