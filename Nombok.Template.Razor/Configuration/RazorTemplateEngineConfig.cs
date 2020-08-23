using System.Collections.Generic;
using Nombok.Template.Configuration;

namespace Nombok.Template.Razor.Configuration
{
    public class RazorTemplateEngineConfig : TemplateConfig
   {
      public const string DefaultSectionName = "Razor";

      public HashSet<string> Namespaces { get; } = new HashSet<string>();
      public IDictionary<string, string> DynamicTemplates { get; } = new Dictionary<string, string>();
      public HashSet<string> ExcludedAssemblies { get; } = new HashSet<string>();
      public bool DisableEncoding { get; set; }
   }
}
