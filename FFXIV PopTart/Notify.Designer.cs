namespace FFXIV_PopTart
{
    partial class Notify
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Notify));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.rtb_instance = new System.Windows.Forms.RichTextBox();
            this.btn_close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.Gainsboro;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Cursor = System.Windows.Forms.Cursors.No;
            this.richTextBox1.Font = new System.Drawing.Font("Agenda", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.ForeColor = System.Drawing.Color.LightCoral;
            this.richTextBox1.Location = new System.Drawing.Point(13, 13);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox1.Size = new System.Drawing.Size(318, 26);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "DUTY FINDER POPPED";
            // 
            // rtb_instance
            // 
            this.rtb_instance.BackColor = System.Drawing.Color.Gainsboro;
            this.rtb_instance.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb_instance.Cursor = System.Windows.Forms.Cursors.No;
            this.rtb_instance.Font = new System.Drawing.Font("Agenda", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_instance.ForeColor = System.Drawing.Color.DodgerBlue;
            this.rtb_instance.Location = new System.Drawing.Point(13, 40);
            this.rtb_instance.Name = "rtb_instance";
            this.rtb_instance.ReadOnly = true;
            this.rtb_instance.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rtb_instance.Size = new System.Drawing.Size(318, 55);
            this.rtb_instance.TabIndex = 1;
            this.rtb_instance.Text = "Instance name";
            // 
            // btn_close
            // 
            this.btn_close.Font = new System.Drawing.Font("Agenda", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close.ForeColor = System.Drawing.Color.Red;
            this.btn_close.Location = new System.Drawing.Point(337, 23);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(59, 55);
            this.btn_close.TabIndex = 2;
            this.btn_close.Text = "X";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // Notify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(408, 107);
            this.ControlBox = false;
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.rtb_instance);
            this.Controls.Add(this.richTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Notify";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Notify";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox rtb_instance;
        private System.Windows.Forms.Button btn_close;
    }
}