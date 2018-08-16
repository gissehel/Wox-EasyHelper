using Wox.EasyHelper.Core.Service;

namespace Wox.EasyHelper.Service
{
    public class FileGeneratorService : IFileGeneratorService
    {
        public IFileGenerator CreateGenerator(string path)
        {
            return new FileGenerator(path);
        }
    }
}