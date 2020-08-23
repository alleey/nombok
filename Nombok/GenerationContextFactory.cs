using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nombok.Core;
using Nombok.Shared;
using System;

namespace Nombok
{
    public class GenerationContextFactory : IFactory<GenerationContext, GenerationContextOptions>
    {
        IServiceProvider _services;

        public GenerationContextFactory(IServiceProvider services)
        {
            _services = services;
        }

        public GenerationContext Create(GenerationContextOptions options)
        {
            var logger = _services.GetRequiredService<ILogger<GenerationContext>>();
            return new GenerationContext(options, logger);
        }
    }
}

