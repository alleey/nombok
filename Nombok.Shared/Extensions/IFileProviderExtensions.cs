using Microsoft.Extensions.FileProviders;
using System.Threading.Tasks;
using System.IO;
using System;

namespace Nombok.Shared.Extensions
{
    public static class IFileProviderExtensions
    {
        public static async Task<string> ReadAllTextAsync(this IFileProvider provider, string filename)
        {
            var fileInfo = provider.GetFileInfo(filename);
            if (fileInfo == null) 
                throw new NombokException($"Exception when reading from file: {filename}");

            using (var reader = new StreamReader(fileInfo.CreateReadStream()))
                return await reader.ReadToEndAsync().ConfigureAwait(false);
        }
        
        public static async Task<string> ReadAllTextAsync(this IFileInfo fileInfo)
        {
            fileInfo = fileInfo ?? throw new ArgumentNullException(nameof(fileInfo));
            using (var reader = new StreamReader(fileInfo.CreateReadStream()))
                return await reader.ReadToEndAsync().ConfigureAwait(false);
        }
    }
}
