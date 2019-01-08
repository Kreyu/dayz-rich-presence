using DZRichPresenceClient.Data;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DZRichPresenceClient.Forms
{
    public class About : Form
    {
        private PictureBox Logo;
        private Label TitleLabel;
        private Label Description;
        private LinkLabel RepositoryLink;
        private LinkLabel WorkshopLink;
        private Button CloseButton;

        public About()
        {
            InitializeComponent();

            this.Location = new Point(
                (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2
            );
        }

        private void InitializeComponent()
        {
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
            this.CloseButton.Text = "Close";
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
            this.TitleLabel.Text = "DayZ Rich Presence";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Description
            // 
            this.Description.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Description.Location = new System.Drawing.Point(71, 41);
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(170, 14);
            this.Description.TabIndex = 2;
            this.Description.Text = "0.1.1";
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
            this.RepositoryLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.RepositoryLink_LinkClicked);
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
            this.WorkshopLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.WorkshopLink_LinkClicked);
            // 
            // Logo
            // 
            this.Logo.Image = global::DZRichPresenceClient.Properties.Resources.icon1;
            this.Logo.InitialImage = global::DZRichPresenceClient.Properties.Resources.icon1;
            this.Logo.Location = new System.Drawing.Point(15, 14);
            this.Logo.Name = "Logo";
            this.Logo.Size = new System.Drawing.Size(50, 50);
            this.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Logo.TabIndex = 5;
            this.Logo.TabStop = false;
            // 
            // About
            // 
            this.ClientSize = new System.Drawing.Size(272, 107);
            this.Controls.Add(this.Logo);
            this.Controls.Add(this.WorkshopLink);
            this.Controls.Add(this.RepositoryLink);
            this.Controls.Add(this.Description);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.CloseButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DayZ Rich Presence";
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            this.ResumeLayout(false);

        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RepositoryLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Program.OpenBrowser(Config.RepositoryUrl);
        }

        private void WorkshopLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Program.OpenBrowser(Config.ModWorkshopUrl);
        }
    }
}
