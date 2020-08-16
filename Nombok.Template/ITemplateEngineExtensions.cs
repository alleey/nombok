using System.Dynamic;
using System.Threading.Tasks;
using Nombok.Shared.Extensions;
using Nombok.Shared;
using Microsoft.Extensions.FileProviders;

namespace Nombok.Template
{
  public static class ITemplateEngineExtensions
  {
    public static async Task<string> RenderTemplateFileAsync<T>(this ITemplateEngine engine, string key, IFileInfo fileInfo, T model, ExpandoObject viewBag = null)
    {
      var contents = await fileInfo.ReadAllTextAsync().ConfigureAwait(false);
      return await engine.RenderTemplateAsync<T>(key, contents, model, viewBag).ConfigureAwait(false);
    }

    public static async Task<string> RenderTemplateFileAsync<T>(this ITemplateEngine engine, IFileInfo fileInfo, T model, ExpandoObject viewBag = null)
    {
      var contents = await fileInfo.ReadAllTextAsync().ConfigureAwait(false);
      return await engine.RenderTemplateAsync<T>(fileInfo.PhysicalPath, contents, model, viewBag).ConfigureAwait(false);
    }

    public static async Task<string> RenderTemplateAsync<T>(this ITemplateEngine engine, string contents, T model, ExpandoObject viewBag = null)
    {
      var key = MD5Hash.CreateMD5(contents);
      return await engine.RenderTemplateAsync<T>(key, contents, model, viewBag).ConfigureAwait(false);
    }
  }
}
