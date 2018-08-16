using System.Collections.Generic;
using Wox.EasyHelper.Core.Service;

namespace Wox.EasyHelper.Test.Mock.Service
{
    public class SystemServiceMock : ISystemService
    {
        public void OpenUrl(string url)
        {
            UrlOpened.Add(url);
        }

        public void StartCommandLine(string command, string arguments)
        {
            CommandLineStarted.Add(new CommandLineMock(command, arguments));
        }

        public List<string> UrlOpened { get; private set; } = new List<string>();

        public List<CommandLineMock> CommandLineStarted { get; private set; } = new List<CommandLineMock>();

        public string ApplicationDataPath { get; set; }

        public class CommandLineMock
        {
            public CommandLineMock(string command, string arguments)
            {
                Command = command;
                Arguments = arguments;
            }

            public string Command { get; set; }
            public string Arguments { get; set; }
        }
    }
}