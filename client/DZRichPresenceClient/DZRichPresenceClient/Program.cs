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
                MessageBox.Show("Client already running!", Properties.Settings.Default.ApplicationName);
                return;
            }

            CheckForUpdates(true);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SysTray());
        }

        public static bool IsAlreadyRunning()
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
            string applicationName = Properties.Settings.Default.ApplicationName;
            string repositoryId = Properties.Settings.Default.RepositoryId;
            string repositoryOwner = Properties.Settings.Default.RepositoryOwner;

            var client = new Octokit.GitHubClient(new Octokit.ProductHeaderValue(repositoryId));
            dynamic releases = null;

            try
            {
                releases = client.Repository.Release.GetAll(repositoryOwner, repositoryId).Result;
            }
            catch (Exception)
            {
                var dialogResult = MessageBox.Show("Couldn't fetch latest release info. Do you want to check manually?", applicationName, MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    OpenBrowser(Properties.Settings.Default.ReleasesUrl);
                }

                return;
            }

            var latest = releases[0];

            Version currentVersion = new Version(Assembly.GetExecutingAssembly().GetName().Version.ToString());
            Version latestVersion = new Version(Regex.Replace(latest.TagName, "[^0-9.]", ""));

            int versionDifference = latestVersion.CompareTo(currentVersion);

            if (versionDifference > 0)
            {
                var dialogResult = MessageBox.Show("New version available. Do you want to open the releases page?", applicationName, MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    OpenBrowser(Properties.Settings.Default.ReleasesUrl);
                }

                return;
            };

            if (!skipSameVersionMessage)
            {
                MessageBox.Show("You are running the latest version.", applicationName);
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
