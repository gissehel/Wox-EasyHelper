using System;

namespace Wox.EasyHelper.Core.Service
{
    public interface IFileGenerator : IDisposable
    {
        IFileGenerator AddLine(string line);

        void Generate();
    }
}