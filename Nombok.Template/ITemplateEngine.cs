using System;
using System.Dynamic;
using System.Threading.Tasks;

namespace Nombok.Template
{
   public interface ITemplateEngine : IDisposable
   {
      public Task<string> RenderTemplateAsync<T>(string key, string content, T model, ExpandoObject viewBag = null);
      public Task<string> RenderTemplateFileAsync<T>(string filename, T model, ExpandoObject viewBag = null);
   }
}
