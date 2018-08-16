using System.IO;
using Wox.EasyHelper.Core.Service;

namespace Wox.EasyHelper.Service
{
    public class FileReaderService : IFileReaderService
    {
        public bool FileExists(string path) => File.Exists(path);

        public IFileReader Read(string path)
        {
            return new FileReader(path);
        }
    }
}