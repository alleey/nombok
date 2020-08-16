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
    public const string DefaultExtension = FileSystemRazorProject.DefaultExtension;
    private readonly IFileProvider _fileProvider;

    public FileProviderRazorProject(IFileProvider fileProvider)
        : this(fileProvider, DefaultExtension)
    {
    }

    public FileProviderRazorProject(IFileProvider fileProvider, string extension)
    {
      Extension = extension ?? throw new ArgumentNullException(nameof(extension));
      _fileProvider = fileProvider;
    }

    public virtual string Extension { get; set; }

    /// <summary>
    /// Looks up for the template source with a given <paramref name="templateKey" />
    /// </summary>
    /// <param name="templateKey">Unique template key</param>
    /// <returns></returns>
    public override Task<RazorLightProjectItem> GetItemAsync(string templateKey)
    {
      if (!templateKey.EndsWith(Extension))
      {
        templateKey = templateKey + Extension;
      }

      var item = new FileSystemRazorProjectItem(templateKey, new FileInfo(templateKey));

      if (item.Exists)
      {
        item.ExpirationToken = _fileProvider.Watch(templateKey);
      }

      return Task.FromResult((RazorLightProjectItem)item);
    }

    public override Task<IEnumerable<RazorLightProjectItem>> GetImportsAsync(string templateKey)
    {
      return Task.FromResult(Enumerable.Empty<RazorLightProjectItem>());
    }
  }
}