using System;

namespace Wox.EasyHelper.Core.Service
{
    public interface IFileReader : IDisposable
    {
        string ReadLine();
    }
}