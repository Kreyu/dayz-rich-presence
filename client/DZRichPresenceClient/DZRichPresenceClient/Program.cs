using DZRichPresenceClient.Data;
using DZRichPresenceClient.Forms;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DZRichPresenceClient
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            if (IsAlreadyRunning())
            {
                MessageBox.Show("Client already running!", Config.ApplicationName);
                return;
            }

            CheckForUpdates(true);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SysTray());
        }

        private static bool IsAlreadyRunning()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);

            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static void CheckForUpdates(bool skipSameVersionMessage = false)
        {
            var client = new Octokit.GitHubClient(new Octokit.ProductHeaderValue(Config.RepositoryId));
            dynamic releases = null;

            try
            {
                releases = client.Repository.Release.GetAll(Config.RepositoryOwner, Config.RepositoryId).Result;
            }
            catch (Exception)
            {
                var dialogResult = MessageBox.Show("Couldn't fetch latest release info. Do you want to check manually?", Config.ApplicationName, MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    OpenBrowser(Config.RepositoryReleasesUrl);
                }

                return;
            }

            var latest = releases[0];

            Version currentVersion = new Version(Assembly.GetExecutingAssembly().GetName().Version.ToString());
            Version latestVersion = new Version(Regex.Replace(latest.TagName, "[^0-9.]", ""));

            int versionDifference = latestVersion.CompareTo(currentVersion);

            if (versionDifference > 0)
            {
                var dialogResult = MessageBox.Show("New version available. Do you want to open the releases page?", Config.ApplicationName, MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    OpenBrowser(Config.RepositoryReleasesUrl);
                }

                return;
            };

            if (!skipSameVersionMessage)
            {
                MessageBox.Show("You are running the latest version.", Config.ApplicationName);
            }
        }

        public static void OpenBrowser(string url)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo(url);
            Process.Start(sInfo);

            try
            {
                Process.Start(sInfo);
            }
            catch (Exception)
            {
                Process.Start("iexplore.exe", sInfo.FileName);
            }
        }
    }
}
