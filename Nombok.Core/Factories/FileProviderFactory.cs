using System.Text;
using Microsoft.Extensions.FileProviders;
using Nombok.Shared;

namespace Nombok.Core.Factories
{
    public class FileProviderFactory : IFactory<IFileProvider, string>
    {
        public IFileProvider Create(string root) => new PhysicalFileProvider(root);
    }
}