using System.Collections.Generic;

namespace Wox.EasyHelper
{
    public delegate IEnumerable<WoxResult> ResultGetter(WoxQuery query, int position);
}