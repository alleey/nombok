using Microsoft.CodeAnalysis;
using Nombok.Template.Razor.Configuration;
using Nombok.Template.Razor.Internal;
using RazorLight;
using RazorLight.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Nombok.Template.Razor
{
   public static class RazorTemplateEngineOptionsExtensions
   {
      public static RazorTemplateEngineOptions UseEncoding(this RazorTemplateEngineOptions options, bool encoding)
      {
         options.DisableEncoding = !encoding;
         return options;
      }

      public static RazorTemplateEngineOptions UseMemoryCachingProvider(this RazorTemplateEngineOptions options)
      {
         return options.UseCachingProvider(new MemoryCachingProvider());
      }

      public static RazorTemplateEngineOptions UseCachingProvider(this RazorTemplateEngineOptions options, ICachingProvider cachingProvider)
      {
         options.CachingProvider = cachingProvider ?? throw new ArgumentNullException(nameof(cachingProvider));
         return options;
      }

      public static RazorTemplateEngineOptions AddDefaultNamespaces(this RazorTemplateEngineOptions options, IEnumerable<string> namespaces)
      {
         namespaces = namespaces ?? throw new ArgumentNullException(nameof(namespaces));
         foreach (var @namespace in namespaces)
            options.Namespaces.Add(@namespace);
         return options;
      }

      public static RazorTemplateEngineOptions AddMetadataReferences(this RazorTemplateEngineOptions options, params MetadataReference[] metadata)
      {
         metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
         foreach (var @reference in metadata)
            options.MetadataReferences.Add(@reference);
         return options;
      }

      public static RazorTemplateEngineOptions AddExcludedAssemblies(this RazorTemplateEngineOptions options, IEnumerable<string> assemblyNames)
      {
         assemblyNames = assemblyNames ?? throw new ArgumentNullException(nameof(assemblyNames));
         foreach (var @assemblyName in assemblyNames)
            options.ExcludedAssemblies.Add(@assemblyName);
         return options;
      }

      public static RazorTemplateEngineOptions AddPrerenderCallbacks(this RazorTemplateEngineOptions options, params Action<ITemplatePage>[] callbacks)
      {
         callbacks = callbacks ?? throw new ArgumentNullException(nameof(callbacks));
         options.PrerenderCallbacks.AddRange(@callbacks);
         return options;
      }

      public static RazorTemplateEngineOptions UseDynamicTemplates(this RazorTemplateEngineOptions options, IDictionary<string, string> dynamicTemplates)
      {
         dynamicTemplates = dynamicTemplates ?? throw new ArgumentNullException(nameof(dynamicTemplates));
         foreach (var entry in dynamicTemplates)
            options.DynamicTemplates[entry.Key] = entry.Value;
         return options;
      }

      public static RazorTemplateEngineOptions AddDynamicTemplate(this RazorTemplateEngineOptions options, string name, string value)
      {
         name = name ?? throw new ArgumentNullException(nameof(name));
         value = value ?? throw new ArgumentNullException(nameof(value));
         options.DynamicTemplates[name] = value;
         return options;
      }

      public static RazorTemplateEngineOptions UseOperatingAssembly(this RazorTemplateEngineOptions options, Assembly assembly)
      {
         options.OperatingAssembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
         return options;
      }

      public static RazorTemplateEngineOptions AddOptionsFromConfig(this RazorTemplateEngineOptions options, RazorTemplateEngineConfig config)
      {
         config = config ?? throw new ArgumentNullException(nameof(config));
         options = options ?? throw new ArgumentNullException(nameof(options));
         options.AddDefaultNamespaces(config.Namespaces.ToArray());
         options.AddExcludedAssemblies(config.ExcludedAssemblies.ToArray());
         options.UseDynamicTemplates(config.DynamicTemplates);
         options.UseEncoding(!config.DisableEncoding);
         return options;
      }

      internal static RazorLightEngine BuildEngine(this RazorTemplateEngineOptions options)
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
