using System;
using System.Collections.Generic;
using System.IO;

namespace Nombok.Template.Configuration
{
   public static class TemplateConfigExtensions
   {
      public static TemplateConfig UseBaseLocation(this TemplateConfig options, string folder)
      {
         options = options ?? throw new ArgumentNullException(nameof(options));
         folder = folder ?? throw new ArgumentNullException(nameof(folder));
         options.BaseLocation = folder;
         return options;
      }

      public static IEnumerable<string> GetTemplateFoldersAbsolute(this TemplateConfig options)
      {
         options = options ?? throw new ArgumentNullException(nameof(options));
         foreach(var folder in options.Locations) {
            if(Path.IsPathRooted(folder))
               yield return folder;
            else
               yield return Path.Combine(options.BaseLocation, folder);
         }
      }
   }
}

