using System;
using System.Windows.Forms;

namespace DZRichPresenceClient
{
    class SysTray : Form
    {
        private NotifyIcon trayIcon;
        private readonly ContextMenu trayMenu;

        public SysTray()
        {
            trayMenu = CreateTrayMenu();
            trayIcon = CreateTrayIcon(trayMenu);

            ShowBalloon("Hey, I'm running here!");

            Presence.Start();
        }

        private ContextMenu CreateTrayMenu()
        {
            ContextMenu trayMenu = new ContextMenu();

            string separator = "-";

            trayMenu.MenuItems.Add(Properties.Settings.Default.ApplicationName, ShowAbout);
            trayMenu.MenuItems.Add(separator);
            trayMenu.MenuItems.Add("GitHub", OpenGitHub);
            trayMenu.MenuItems.Add("Check for updates", CheckForUpdates);
            trayMenu.MenuItems.Add(separator);
            trayMenu.MenuItems.Add("Exit", OnExit);

            return trayMenu;
        }

        private NotifyIcon CreateTrayIcon(ContextMenu trayMenu)
        {
            return new NotifyIcon
            {
                Text = Properties.Settings.Default.ApplicationName,
                Icon = Properties.Resources.icon,

                ContextMenu = trayMenu,
                Visible = true
            };
        }

        private void ShowBalloon(string body, int timeout = 3000)
        {
            trayIcon.BalloonTipTitle = Properties.Settings.Default.ApplicationName;
            trayIcon.BalloonTipText = body;
            trayIcon.ShowBalloonTip(timeout);
        }

        private void ShowAbout(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }

        private void OpenGitHub(object sender, EventArgs e)
        {
            Program.OpenBrowser(Properties.Settings.Default.RepositoryUrl);
        }

        private void CheckForUpdates(object sender = null, EventArgs e = null)
        {
            Program.CheckForUpdates();
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;

            base.OnLoad(e);
        }

        private void OnExit(object sender, EventArgs e)
        {
            Presence.Stop();
            Application.Exit();
        }

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                trayIcon.Dispose();
            }

            base.Dispose(isDisposing);
        }
    }
}
