using System;
using System.Collections.Generic;

namespace Wox.EasyHelper.Core.Service
{
    public interface IWoxResultFinder : IDisposable
    {
        IEnumerable<WoxResult> GetResults(WoxQuery woxQuery);

        void Init();
    }
}