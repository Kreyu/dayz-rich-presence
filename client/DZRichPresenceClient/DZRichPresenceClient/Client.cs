using Octokit;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DZRichPresenceClient
{
    class Client : Form
    {
        private NotifyIcon trayIcon;
        private readonly ContextMenu trayMenu;

        private Button CloseButton;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label Description;
        private LinkLabel RepositoryLink;
        private LinkLabel WorkshopLink;
        private PictureBox Logo;
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

            trayMenu.MenuItems.Add(applicationName, ShowAbout);
            trayMenu.MenuItems.Add(separator);
            trayMenu.MenuItems.Add("GitHub", OpenGitHub);
            trayMenu.MenuItems.Add("Check for updates", CheckForUpdates);
            trayMenu.MenuItems.Add(separator);
            trayMenu.MenuItems.Add("Exit", OnExit);

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
                Icon = Properties.Resources.icon,

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
        /// Opens the "about" window.
        /// </summary>
        private void ShowAbout(object sender, EventArgs e)
        {
            Visible = true;
            Focus();
        }

        /// <summary>
        /// Opens the project's GitHub repository url in the browser.
        /// </summary>
        private void OpenGitHub(object sender, EventArgs e)
        {
            OpenBrowser(Properties.Settings.Default.RepositoryUrl);
        }

        /// <summary>
        /// Checks if current version is the latest available.
        /// Opens confirmation dialog to navigate to releases page if so.
        /// </summary>
        private void CheckForUpdates(object sender = null, EventArgs e = null)
        {
            string applicationName = Properties.Settings.Default.ApplicationName;
            string repositoryId = Properties.Settings.Default.RepositoryId;
            string repositoryOwner = Properties.Settings.Default.RepositoryOwner;

            var client = new GitHubClient(new ProductHeaderValue(repositoryId));
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

            MessageBox.Show("You are running the latest version.", applicationName);
        }

        /// <summary>
        /// Opens the given url in the browser.
        /// </summary>
        private void OpenBrowser(string url)
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

        protected override void OnLoad(EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;

            InitializeComponent();

            this.Location = new Point(
                (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2
            );

            base.OnLoad(e);
        }

        private void OnExit(object sender, EventArgs e)
        {
            Presence.Stop();
            System.Windows.Forms.Application.Exit();
        }

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                trayIcon.Dispose();
            }

            base.Dispose(isDisposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Client));
            this.CloseButton = new System.Windows.Forms.Button();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.Description = new System.Windows.Forms.Label();
            this.RepositoryLink = new System.Windows.Forms.LinkLabel();
            this.WorkshopLink = new System.Windows.Forms.LinkLabel();
            this.Logo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(170, 71);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(86, 23);
            this.CloseButton.TabIndex = 0;
            this.CloseButton.Text = "OK";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // TitleLabel
            // 
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.TitleLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TitleLabel.Location = new System.Drawing.Point(71, 18);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(167, 23);
            this.TitleLabel.TabIndex = 1;
            this.TitleLabel.Text = global::DZRichPresenceClient.Properties.Settings.Default.ApplicationName;
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Description
            // 
            this.Description.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Description.Location = new System.Drawing.Point(71, 41);
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(170, 14);
            this.Description.TabIndex = 2;
            this.Description.Text = global::DZRichPresenceClient.Properties.Settings.Default.Version;
            this.Description.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RepositoryLink
            // 
            this.RepositoryLink.Location = new System.Drawing.Point(15, 67);
            this.RepositoryLink.Name = "RepositoryLink";
            this.RepositoryLink.Size = new System.Drawing.Size(43, 31);
            this.RepositoryLink.TabIndex = 3;
            this.RepositoryLink.TabStop = true;
            this.RepositoryLink.Text = "GitHub";
            this.RepositoryLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // WorkshopLink
            // 
            this.WorkshopLink.Location = new System.Drawing.Point(59, 67);
            this.WorkshopLink.Name = "WorkshopLink";
            this.WorkshopLink.Size = new System.Drawing.Size(92, 31);
            this.WorkshopLink.TabIndex = 4;
            this.WorkshopLink.TabStop = true;
            this.WorkshopLink.Text = "Steam Workshop";
            this.WorkshopLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Logo
            // 
            this.Logo.Image = ((System.Drawing.Image)(resources.GetObject("Logo.Image")));
            this.Logo.InitialImage = ((System.Drawing.Image)(resources.GetObject("Logo.InitialImage")));
            this.Logo.Location = new System.Drawing.Point(15, 14);
            this.Logo.Name = "Logo";
            this.Logo.Size = new System.Drawing.Size(50, 50);
            this.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Logo.TabIndex = 5;
            this.Logo.TabStop = false;
            // 
            // Client
            // 
            this.ClientSize = new System.Drawing.Size(272, 107);
            this.Controls.Add(this.Logo);
            this.Controls.Add(this.WorkshopLink);
            this.Controls.Add(this.RepositoryLink);
            this.Controls.Add(this.Description);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.CloseButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Client";
            this.Text = global::DZRichPresenceClient.Properties.Settings.Default.ApplicationName;
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            this.ResumeLayout(false);

        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
