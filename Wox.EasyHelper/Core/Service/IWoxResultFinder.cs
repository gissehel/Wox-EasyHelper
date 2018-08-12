using System.Collections.Generic;

namespace Wox.EasyHelper.Core.Service
{
    public interface IWoxResultFinder
    {
        IEnumerable<WoxResult> GetResults(WoxQuery woxQuery);
    }
}