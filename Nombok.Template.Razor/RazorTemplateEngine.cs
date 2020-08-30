using System;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nombok.Template.Razor.Internal;
using RazorLight;

namespace Nombok.Template.Razor
{
   public class RazorTemplateEngine : IRazorTemplateEngine
   {
      private readonly ILogger _logger;
      private readonly RazorTemplateEngineOptions _options;
      private IRazorLightEngine _engine;
      private bool _disposedValue;

      public RazorTemplateEngine(IOptions<RazorTemplateEngineOptions> options, ILogger<RazorTemplateEngine> logger)
      {
         _options = (options ?? throw new ArgumentNullException(nameof(options))).Value;
         _logger = logger ?? throw new ArgumentNullException(nameof(logger));
         _logger.LogInformation($"Logging information {_options.FileProvider}");
      }

      protected virtual void Dispose(bool disposing)
      {
         if (!_disposedValue)
         {
            if (disposing)
            {
               _engine = null;
            }
            _disposedValue = true;
         }
      }

      public void Dispose()
      {
         Dispose(disposing: true);
         GC.SuppressFinalize(this);
      }

      public RazorTemplateEngineOptions Options { get => _options; }

      public async Task<string> RenderTemplateAsync<T>(string key, string content, T model, ExpandoObject viewBag = null)
      {
         if (_engine == null)
            _engine = BuildEngine(_options);
         _logger.LogInformation($"Rendering content with key ${key}");
         return await _engine.CompileRenderStringAsync(key, content, model, viewBag).ConfigureAwait(false);
      }

        public async Task<string> RenderTemplateFileAsync<T>(string filename, T model, ExpandoObject viewBag = null)
        {
         if (_engine == null)
            _engine = BuildEngine(_options);
         _logger.LogInformation($"Rendering template ${filename}");
         return await _engine.CompileRenderAsync<T>(filename, model, viewBag).ConfigureAwait(false);
        }

      protected virtual IRazorLightEngine BuildEngine(RazorTemplateEngineOptions options)
      {
         var builder = new RazorLightEngineBuilder()
             .AddDefaultNamespaces(options.Namespaces.ToArray())
             .AddDynamicTemplates(options.DynamicTemplates)
             .AddMetadataReferences(options.MetadataReferences.ToArray())
             .AddPrerenderCallbacks(options.PrerenderCallbacks.ToArray())
             .ExcludeAssemblies(options.ExcludedAssemblies.ToArray())
             .SetOperatingAssembly(options.OperatingAssembly)
             .UseProject(new FileProviderRazorProject(options.FileProvider))
             .UseCachingProvider(options.CachingProvider)
             ;
         if (options.DisableEncoding)
            builder.DisableEncoding();
         else
            builder.EnableEncoding();
         return builder.Build();
      }

    }
}
