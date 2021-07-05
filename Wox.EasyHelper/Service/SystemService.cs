using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using Wox.EasyHelper.Core.Service;

namespace Wox.EasyHelper.Service
{
    public class SystemService : ISystemService
    {
        public SystemService(string applicationName)
        {
            ApplicationName = applicationName;
        }

        public string ApplicationDataPath => GetApplicationDataPath();

        private string GetApplicationDataPath()
        {
            var appDataPathParent = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var appDataPath = Path.Combine(appDataPathParent, ApplicationName);
            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }
            return appDataPath;
        }

        public string DatabaseName => ApplicationName;

        public string ApplicationName { get; }

        public void OpenUrl(string url)
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true,
                    RedirectStandardOutput = false,
                    CreateNoWindow = false,
                }
            };

            try
            {
                proc.Start();
            }
            catch (Exception)
            {
                // TODO : Find something usefull here...
            }
        }

        public void StartCommandLine(string command, string arguments)
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = command,
                    Arguments = arguments,
                    UseShellExecute = true,
                    RedirectStandardOutput = false,
                    CreateNoWindow = false,
                    WorkingDirectory = ApplicationDataPath
                }
            };

            try
            {
                proc.Start();
            }
            catch (Exception)
            {
                // TODO : Find something usefull here...
            }
        }

        public void CopyTextToClipboard(string text)
        {
            Clipboard.SetText(text);
        }
    }
}