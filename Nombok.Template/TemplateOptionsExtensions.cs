using System;
using System.Collections.Generic;
using Microsoft.Extensions.FileProviders;

namespace Nombok.Template
{
    public static class TemplateOptionsExtensions
    {
        public static TemplateOptions AddTemplateProvider(this TemplateOptions options, IFileProvider provider)
        {
            provider = provider ?? throw new ArgumentNullException(nameof(provider));
            options.TemplateFileProviders.Add(provider);
            return options;
        }

        public static TemplateOptions AddTemplateFolders(this TemplateOptions options, params string[] folderList)
        {
            folderList = folderList ?? throw new ArgumentNullException(nameof(folderList));
            foreach(var folder in folderList)
                options.Locations.Add(folder);
            return options;
        }

        public static IFileProvider BuildTemplateProvider(this TemplateOptions options)
        {
            options = options ?? throw new ArgumentNullException(nameof(options));
            var providers = new List<IFileProvider>(options.TemplateFileProviders);
            foreach(var folder in options.Locations)
                providers.Add(new PhysicalFileProvider(folder));
            if (providers.Count == 1)
                return providers[0];
            return new CompositeFileProvider(providers);
        }
    }

}

