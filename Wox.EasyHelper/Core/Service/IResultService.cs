using System.Collections.Generic;
using Wox.Plugin;

namespace Wox.EasyHelper.Core.Service
{
    public interface IResultService
    {
        List<Result> MapResults(IEnumerable<WoxResult> results);
    }
}