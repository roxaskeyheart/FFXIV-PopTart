namespace FFXIV_PopTart
{
    partial class PopTart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopTart));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mi_listenenable = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_voicetoggle = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_winstart = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notify_master = new System.Windows.Forms.NotifyIcon(this.components);
            this.mi_popuptoggle = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_roulettetoggle = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_listenenable,
            this.mi_voicetoggle,
            this.mi_popuptoggle,
            this.mi_roulettetoggle,
            this.mi_winstart,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(207, 188);
            // 
            // mi_listenenable
            // 
            this.mi_listenenable.Checked = true;
            this.mi_listenenable.CheckOnClick = true;
            this.mi_listenenable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mi_listenenable.Name = "mi_listenenable";
            this.mi_listenenable.Size = new System.Drawing.Size(206, 26);
            this.mi_listenenable.Text = "Enabled";
            this.mi_listenenable.Click += new System.EventHandler(this.mi_listenenable_Click);
            // 
            // mi_voicetoggle
            // 
            this.mi_voicetoggle.Checked = true;
            this.mi_voicetoggle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mi_voicetoggle.Name = "mi_voicetoggle";
            this.mi_voicetoggle.Size = new System.Drawing.Size(206, 26);
            this.mi_voicetoggle.Text = "Enable Voice";
            this.mi_voicetoggle.Click += new System.EventHandler(this.mi_voicetoggle_Click);
            // 
            // mi_winstart
            // 
            this.mi_winstart.Name = "mi_winstart";
            this.mi_winstart.Size = new System.Drawing.Size(206, 26);
            this.mi_winstart.Text = "Start with Windows";
            this.mi_winstart.Click += new System.EventHandler(this.mi_winstart_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(206, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // notify_master
            // 
            this.notify_master.Icon = ((System.Drawing.Icon)(resources.GetObject("notify_master.Icon")));
            this.notify_master.Text = "FFXIV PopTart";
            this.notify_master.Visible = true;
            // 
            // mi_popuptoggle
            // 
            this.mi_popuptoggle.Checked = true;
            this.mi_popuptoggle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mi_popuptoggle.Name = "mi_popuptoggle";
            this.mi_popuptoggle.Size = new System.Drawing.Size(206, 26);
            this.mi_popuptoggle.Text = "Enable Popup";
            this.mi_popuptoggle.Click += new System.EventHandler(this.mi_popuptoggle_Click);
            // 
            // mi_roulettetoggle
            // 
            this.mi_roulettetoggle.Name = "mi_roulettetoggle";
            this.mi_roulettetoggle.Size = new System.Drawing.Size(206, 26);
            this.mi_roulettetoggle.Text = "Roulette Only";
            this.mi_roulettetoggle.Click += new System.EventHandler(this.mi_roulettetoggle_Click);
            // 
            // PopTart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "PopTart";
            this.Text = "Form1";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mi_listenenable;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notify_master;
        private System.Windows.Forms.ToolStripMenuItem mi_winstart;
        private System.Windows.Forms.ToolStripMenuItem mi_voicetoggle;
        private System.Windows.Forms.ToolStripMenuItem mi_popuptoggle;
        private System.Windows.Forms.ToolStripMenuItem mi_roulettetoggle;
    }
}

