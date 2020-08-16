using System.Collections.Generic;
using Microsoft.Extensions.FileProviders;

namespace Nombok.Template
{
  public class TemplateOptions
  {
    public HashSet<string> Locations { get; } = new HashSet<string>();
    
    public IList<IFileProvider> TemplateFileProviders { get; } = new List<IFileProvider>();
  }
}

