using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.FileProviders;
using Nombok.Template.Razor.Internal;
using RazorLight;
using RazorLight.Caching;
using RazorLight.Compilation;
using RazorLight.Generation;

namespace Nombok.Template.Razor
{
  public static class RazorTemplateEngineOptionsExtensions
  {
    public static RazorTemplateEngineOptions DisableEncoding(this RazorTemplateEngineOptions options)
    {
      options.DisableEncoding = true;
      return options;
    }

    public static RazorTemplateEngineOptions EnableEncoding(this RazorTemplateEngineOptions options)
    {
      options.DisableEncoding = false;
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

    public static RazorTemplateEngineOptions AddDefaultNamespaces(this RazorTemplateEngineOptions options, params string[] namespaces)
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

    public static RazorTemplateEngineOptions ExcludeAssemblies(this RazorTemplateEngineOptions options, params string[] assemblyNames)
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
      foreach(var entry in dynamicTemplates)
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

    internal static RazorLightEngine BuildEngine(this RazorTemplateEngineOptions options)
    {
        var builder = new RazorLightEngineBuilder()
            .AddDefaultNamespaces(options.Namespaces.ToArray())
            .AddDynamicTemplates(options.DynamicTemplates)
            .AddMetadataReferences(options.MetadataReferences.ToArray())
            .AddPrerenderCallbacks(options.PrerenderCallbacks.ToArray())
            .ExcludeAssemblies(options.ExcludedAssemblies.ToArray())
            .SetOperatingAssembly(options.OperatingAssembly)
            .UseProject(new FileProviderRazorProject(options.BuildTemplateProvider()))
            .UseCachingProvider(options.CachingProvider)
            ;
        if(options.DisableEncoding)
            builder.DisableEncoding();
        else
            builder.EnableEncoding();
        return builder.Build();
    }
  }
}
