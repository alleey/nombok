using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.FileProviders;

namespace Nombok.Template
{
    public static class TemplateOptionsExtensions
    {
        public static TemplateOptions UseFileProvider(this TemplateOptions options, IFileProvider provider)
        {
            options = options ?? throw new ArgumentNullException(nameof(options));
            options.FileProvider = provider ?? throw new ArgumentNullException(nameof(provider));
            return options;
        }

        public static TemplateOptions UseTemplateFolders(this TemplateOptions options, IEnumerable<string> folders)
        {
            options = options ?? throw new ArgumentNullException(nameof(options));
            options.FileProvider = new CompositeFileProvider(folders.Select(x => new PhysicalFileProvider(x)));
            return options;
        }
    }
}

