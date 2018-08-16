using System;
using System.Collections.Generic;

namespace Wox.EasyHelper
{
    public class CommandInfo
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public Action FinalAction { get; set; }
        public Func<WoxQuery, int, IEnumerable<WoxResult>> ResultGetter { get; set; }

        public string Path { get; set; }
    }
}