namespace HospitalForms
{
    partial class LoginWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginWindow));
            this.signIn = new System.Windows.Forms.Button();
            this.signUp = new System.Windows.Forms.Button();
            this.loginBox = new System.Windows.Forms.TextBox();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.showPassword = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.loginSuffix = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // signIn
            // 
            this.signIn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.signIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.signIn.Location = new System.Drawing.Point(571, 380);
            this.signIn.Name = "signIn";
            this.signIn.Size = new System.Drawing.Size(75, 30);
            this.signIn.TabIndex = 10;
            this.signIn.Text = "Sign In";
            this.signIn.UseVisualStyleBackColor = true;
            // 
            // signUp
            // 
            this.signUp.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.signUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.signUp.Location = new System.Drawing.Point(385, 380);
            this.signUp.Name = "signUp";
            this.signUp.Size = new System.Drawing.Size(75, 30);
            this.signUp.TabIndex = 5;
            this.signUp.Text = "Sign Up";
            this.signUp.UseVisualStyleBackColor = true;
            // 
            // loginBox
            // 
            this.loginBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.loginBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.loginBox.Location = new System.Drawing.Point(450, 270);
            this.loginBox.Name = "loginBox";
            this.loginBox.Size = new System.Drawing.Size(127, 24);
            this.loginBox.TabIndex = 6;
            this.loginBox.TextChanged += new System.EventHandler(this.loginBox_TextChanged);
            // 
            // passwordBox
            // 
            this.passwordBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.passwordBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.passwordBox.Location = new System.Drawing.Point(450, 320);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(220, 24);
            this.passwordBox.TabIndex = 8;
            this.passwordBox.TextChanged += new System.EventHandler(this.passwordBox_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.label1.Location = new System.Drawing.Point(350, 269);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 22);
            this.label1.TabIndex = 5;
            this.label1.Text = "Login";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.label2.Location = new System.Drawing.Point(350, 319);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 22);
            this.label2.TabIndex = 5;
            this.label2.Text = "Password";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Monotype Corsiva", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label3.Location = new System.Drawing.Point(377, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(278, 28);
            this.label3.TabIndex = 6;
            this.label3.Text = "Welcome to “VRRAM” LLC";
            // 
            // showPassword
            // 
            this.showPassword.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.showPassword.AutoSize = true;
            this.showPassword.BackColor = System.Drawing.Color.Transparent;
            this.showPassword.Location = new System.Drawing.Point(460, 350);
            this.showPassword.Name = "showPassword";
            this.showPassword.Size = new System.Drawing.Size(101, 17);
            this.showPassword.TabIndex = 9;
            this.showPassword.Text = "Show password";
            this.showPassword.UseVisualStyleBackColor = false;
            this.showPassword.CheckedChanged += new System.EventHandler(this.showPassword_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(440, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(170, 170);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // loginSuffix
            // 
            this.loginSuffix.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.loginSuffix.AutoCompleteCustomSource.AddRange(new string[] {
            "@pat.hosp",
            "@doc.hosp",
            "@adm.hosp"});
            this.loginSuffix.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.loginSuffix.FormattingEnabled = true;
            this.loginSuffix.Items.AddRange(new object[] {
            "@pat.hosp",
            "@doc.hosp",
            "@adm.hosp"});
            this.loginSuffix.Location = new System.Drawing.Point(580, 270);
            this.loginSuffix.Name = "loginSuffix";
            this.loginSuffix.Size = new System.Drawing.Size(90, 24);
            this.loginSuffix.TabIndex = 7;
            // 
            // LoginWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1034, 636);
            this.Controls.Add(this.loginSuffix);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.showPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.loginBox);
            this.Controls.Add(this.signUp);
            this.Controls.Add(this.signIn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LoginWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.LoginWindow_Paint);
            this.Resize += new System.EventHandler(this.LoginWindow_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button signIn;
        private System.Windows.Forms.Button signUp;
        private System.Windows.Forms.TextBox loginBox;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox showPassword;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox loginSuffix;
    }
}

