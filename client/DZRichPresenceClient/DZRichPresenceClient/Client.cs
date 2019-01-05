using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DZRichPresenceClient
{
    class Client : Form
    {
        private NotifyIcon trayIcon;
        private readonly ContextMenu trayMenu;
        private string applicationName;
        
        public Client()
        {
            applicationName = Properties.Settings.Default.ApplicationName;

            trayMenu = CreateTrayMenu();
            trayIcon = CreateTrayIcon(trayMenu);

            ShowBalloon("Hey, I'm running here!");

            Presence.Start();
        }

        /// <summary>
        /// Creates the tray menu with configured items.
        /// </summary>
        /// <returns>
        /// The configured, ready to use tray menu.
        /// </returns>
        private ContextMenu CreateTrayMenu()
        {
            ContextMenu trayMenu = new ContextMenu();

            string separator = "-";

            trayMenu.MenuItems.Add(applicationName);
            trayMenu.MenuItems.Add(separator);
            trayMenu.MenuItems.Add("GitHub", OpenGitHub);
            trayMenu.MenuItems.Add("Check for updates");
            trayMenu.MenuItems.Add(separator);
            trayMenu.MenuItems.Add("Exit", OnExit);

            trayMenu.MenuItems[0].Enabled = false;

            return trayMenu;
        }

        /// <summary>
        /// Creates the tray icon with given tray menu.
        /// </summary>
        /// <returns>
        /// The tray icon with given tray menu.
        /// </returns>
        private NotifyIcon CreateTrayIcon(ContextMenu trayMenu)
        {
            return new NotifyIcon
            {
                Text = applicationName,
                Icon = new Icon(SystemIcons.Application, 40, 40),

                ContextMenu = trayMenu,
                Visible = true
            };
        }

        /// <summary>
        /// Creates and shows the balloon tip with given body and optional timeout.
        /// </summary>
        private void ShowBalloon(string body, int timeout = 3000)
        {
            trayIcon.BalloonTipTitle = applicationName;
            trayIcon.BalloonTipText = body;
            trayIcon.ShowBalloonTip(timeout);
        }

        /// <summary>
        /// Opens the project's GitHub repository in the browser.
        /// </summary>
        private void OpenGitHub(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("https://github.com/Kreyu/dayz-rich-presence");
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
