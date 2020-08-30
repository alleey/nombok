using System;
using System.Collections.Generic;

namespace Nombok.Template.Configuration
{
    public static class TemplateConfigExtensions
    {
        public static TemplateConfig AddTemplateFolders(this TemplateConfig options, IEnumerable<string> folderList)
        {
            options = options ?? throw new ArgumentNullException(nameof(options));
            folderList = folderList ?? throw new ArgumentNullException(nameof(folderList));
            foreach(var folder in folderList)
                options.Locations.Add(folder);
            return options;
        }
    }
}

