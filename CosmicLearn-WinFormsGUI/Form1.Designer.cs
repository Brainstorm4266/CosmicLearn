namespace CosmicLearn_WinFormsGUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.WelcomeLabel = new System.Windows.Forms.Label();
            this.LearnButton = new CosmicLearn_WinFormsGUI.RoundedButton();
            this.CreateSet = new CosmicLearn_WinFormsGUI.RoundedButton();
            this.EditSet = new CosmicLearn_WinFormsGUI.RoundedButton();
            this.Settings = new CosmicLearn_WinFormsGUI.RoundedButton();
            this.LoggedInAs = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // WelcomeLabel
            // 
            this.WelcomeLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.WelcomeLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.WelcomeLabel.ForeColor = System.Drawing.Color.White;
            this.WelcomeLabel.Location = new System.Drawing.Point(224, 79);
            this.WelcomeLabel.Name = "WelcomeLabel";
            this.WelcomeLabel.Size = new System.Drawing.Size(369, 108);
            this.WelcomeLabel.TabIndex = 0;
            this.WelcomeLabel.Text = "Welcome to CosmicLearn!";
            this.WelcomeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LearnButton
            // 
            this.LearnButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LearnButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.LearnButton.FlatAppearance.BorderSize = 0;
            this.LearnButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LearnButton.ForeColor = System.Drawing.Color.White;
            this.LearnButton.Location = new System.Drawing.Point(350, 244);
            this.LearnButton.Name = "LearnButton";
            this.LearnButton.Size = new System.Drawing.Size(118, 40);
            this.LearnButton.TabIndex = 1;
            this.LearnButton.Text = "Learn";
            this.LearnButton.UseVisualStyleBackColor = false;
            // 
            // CreateSet
            // 
            this.CreateSet.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CreateSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CreateSet.FlatAppearance.BorderSize = 0;
            this.CreateSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreateSet.ForeColor = System.Drawing.Color.White;
            this.CreateSet.Location = new System.Drawing.Point(224, 244);
            this.CreateSet.Name = "CreateSet";
            this.CreateSet.Size = new System.Drawing.Size(118, 40);
            this.CreateSet.TabIndex = 2;
            this.CreateSet.Text = "Create Set";
            this.CreateSet.UseVisualStyleBackColor = false;
            // 
            // EditSet
            // 
            this.EditSet.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.EditSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.EditSet.FlatAppearance.BorderSize = 0;
            this.EditSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EditSet.ForeColor = System.Drawing.Color.White;
            this.EditSet.Location = new System.Drawing.Point(475, 244);
            this.EditSet.Name = "EditSet";
            this.EditSet.Size = new System.Drawing.Size(118, 40);
            this.EditSet.TabIndex = 3;
            this.EditSet.Text = "Edit Set";
            this.EditSet.UseVisualStyleBackColor = false;
            // 
            // Settings
            // 
            this.Settings.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Settings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Settings.FlatAppearance.BorderSize = 0;
            this.Settings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Settings.ForeColor = System.Drawing.Color.White;
            this.Settings.Location = new System.Drawing.Point(350, 322);
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(118, 40);
            this.Settings.TabIndex = 4;
            this.Settings.Text = "Settings";
            this.Settings.UseVisualStyleBackColor = false;
            // 
            // LoggedInAs
            // 
            this.LoggedInAs.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LoggedInAs.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LoggedInAs.ForeColor = System.Drawing.Color.White;
            this.LoggedInAs.Location = new System.Drawing.Point(224, 382);
            this.LoggedInAs.Name = "LoggedInAs";
            this.LoggedInAs.Size = new System.Drawing.Size(369, 48);
            this.LoggedInAs.TabIndex = 5;
            this.LoggedInAs.Text = "Logged in as: None";
            this.LoggedInAs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.LoggedInAs);
            this.Controls.Add(this.Settings);
            this.Controls.Add(this.EditSet);
            this.Controls.Add(this.CreateSet);
            this.Controls.Add(this.LearnButton);
            this.Controls.Add(this.WelcomeLabel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Label WelcomeLabel;
        private RoundedButton LearnButton;
        private RoundedButton CreateSet;
        private RoundedButton EditSet;
        private RoundedButton Settings;
        private Label LoggedInAs;
    }
}