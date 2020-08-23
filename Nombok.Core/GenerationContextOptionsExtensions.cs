using Microsoft.Extensions.FileProviders;
using System;

namespace Nombok.Core
{
   public static class GenerationContextOptionsExtensions
   {
      // public static GenerationContextOptions AddTemplateFolders(this GenerationContextOptions options, params string[] folderList)
      // {
      //    options = options ?? throw new ArgumentNullException(nameof(options));
      //    folderList = folderList ?? throw new ArgumentNullException(nameof(folderList));
      //    foreach(var folder in folderList)
      //          options.FileProviders.Add(new PhysicalFileProvider(folder));
      //    return options;
      // }

      // // public static GenerationContextOptions AddTemplateFoldersFromConfig(this GenerationContextOptions options, TemplateConfig config)
      // // {
      // //    config = config ?? throw new ArgumentNullException(nameof(config));
      // //    return options.AddTemplateFolders(config.Locations.ToArray());
      // // }

      // public static GenerationContextOptions AddTemplateProviders(this GenerationContextOptions options, IFileProvider provider)
      // {
      //    options = options ?? throw new ArgumentNullException(nameof(options));
      //    provider = provider ?? throw new ArgumentNullException(nameof(provider));
      //    options.FileProviders.Add(provider);
      //    return options;
      // }
   }
}

