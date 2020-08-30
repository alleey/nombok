using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using RazorLight.Razor;

namespace Nombok.Template.Razor.Internal
{
   class FileProviderRazorProject : RazorLightProject
   {
      private readonly IFileProvider _fileProvider;

      public FileProviderRazorProject(IFileProvider fileProvider)
      {
         _fileProvider = fileProvider;
      }

      public override Task<RazorLightProjectItem> GetItemAsync(string templateKey)
      {
         var item = new FileInfotItem(templateKey, _fileProvider.GetFileInfo(templateKey));
         if (item.Exists)
         {
               item.ExpirationToken = _fileProvider.Watch(templateKey);
         }
         return Task.FromResult<RazorLightProjectItem>(item);
      }

      public override Task<IEnumerable<RazorLightProjectItem>> GetImportsAsync(string templateKey)
      {
         return Task.FromResult(Enumerable.Empty<RazorLightProjectItem>());
      }

      class FileInfotItem : RazorLightProjectItem
      {
         public FileInfotItem(string templateKey, IFileInfo fileInfo)
         {
               Key = templateKey ?? throw new ArgumentNullException(nameof(templateKey));
               File = fileInfo ?? throw new ArgumentNullException(nameof(fileInfo));
         }

         public IFileInfo File { get; }
         public override string Key { get; }
         public override bool Exists => File.Exists;
         public override Stream Read() => File.CreateReadStream();
      }
   }
}