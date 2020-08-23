using Microsoft.CodeAnalysis;
using RazorLight;
using RazorLight.Caching;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Nombok.Template.Razor
{

    public class RazorTemplateEngineOptions : TemplateOptions
   {
      public Assembly OperatingAssembly { get; set; } = Assembly.GetEntryAssembly();
      public HashSet<string> Namespaces { get; } = new HashSet<string>();
      public IDictionary<string, string> DynamicTemplates { get; } = new Dictionary<string, string>();
      public HashSet<MetadataReference> MetadataReferences { get; } = new HashSet<MetadataReference>();
      public HashSet<string> ExcludedAssemblies { get; } = new HashSet<string>();
      public List<Action<ITemplatePage>> PrerenderCallbacks { get; } = new List<Action<ITemplatePage>>();
      public ICachingProvider CachingProvider { get; set; }
      public bool DisableEncoding { get; set; }
   }
}
