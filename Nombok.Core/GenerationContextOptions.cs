using Microsoft.Extensions.FileProviders;

namespace Nombok.Core
{
   public class GenerationContextOptions
   {
      public IFileProvider CodebaseFileProvider { get; set; }
   }
}

