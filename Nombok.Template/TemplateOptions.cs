using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;

namespace Nombok.Template
{
   public class TemplateOptions
   {
      public HashSet<string> Locations { get; } = new HashSet<string>();

      public IList<IFileProvider> TemplateFileProviders { get; } = new List<IFileProvider>();
   }
}

