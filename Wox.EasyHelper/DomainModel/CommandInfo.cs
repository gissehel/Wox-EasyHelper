using System;

namespace Wox.EasyHelper
{
    public class CommandInfo
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public Action FinalAction { get; set; }
        public ResultGetter ResultGetter { get; set; }

        public string Path { get; set; }
    }
}