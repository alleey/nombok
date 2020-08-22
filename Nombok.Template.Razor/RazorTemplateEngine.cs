using System;
using System.Dynamic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RazorLight;

namespace Nombok.Template.Razor
{
   public class RazorTemplateEngine : IRazorTemplateEngine
   {
      private readonly ILogger _logger;
      private readonly RazorTemplateEngineOptions _options;
      private RazorLightEngine _engine;
      private bool _disposedValue;

      public RazorTemplateEngine(IOptions<RazorTemplateEngineOptions> options, ILogger<RazorTemplateEngine> logger)
      {
         _options = (options ?? throw new ArgumentNullException(nameof(options))).Value;
         _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
            _engine = _options.BuildEngine();
         return await _engine.CompileRenderStringAsync(key, content, model, viewBag).ConfigureAwait(false);
      }
   }
}
