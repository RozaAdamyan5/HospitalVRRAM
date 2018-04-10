namespace HospitalForms
{
    partial class DiagnosisWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiagnosisWindow));
            this.patientHistory = new System.Windows.Forms.Button();
            this.nameEdit = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.diseaseBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.AddNewMedicine = new System.Windows.Forms.Panel();
            this.addMedicine = new System.Windows.Forms.Button();
            this.medicineName = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.finish = new System.Windows.Forms.Button();
            this.scrollablePanel = new System.Windows.Forms.Panel();
            this.medicineListTable = new System.Windows.Forms.TableLayoutPanel();
            this.historyPanel = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.fullName = new System.Windows.Forms.Label();
            this.AddNewMedicine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.scrollablePanel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // patientHistory
            // 
            this.patientHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.patientHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.patientHistory.Location = new System.Drawing.Point(1084, 158);
            this.patientHistory.Name = "patientHistory";
            this.patientHistory.Size = new System.Drawing.Size(141, 40);
            this.patientHistory.TabIndex = 0;
            this.patientHistory.Text = "History";
            this.patientHistory.UseVisualStyleBackColor = true;
            // 
            // nameEdit
            // 
            this.nameEdit.AutoSize = true;
            this.nameEdit.BackColor = System.Drawing.Color.Transparent;
            this.nameEdit.Font = new System.Drawing.Font("Segoe Print", 13F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.nameEdit.Location = new System.Drawing.Point(46, 167);
            this.nameEdit.Name = "nameEdit";
            this.nameEdit.Size = new System.Drawing.Size(203, 31);
            this.nameEdit.TabIndex = 1;
            this.nameEdit.Text = "Patient\'s Full Name: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe Print", 13F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label2.Location = new System.Drawing.Point(47, 218);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 31);
            this.label2.TabIndex = 2;
            this.label2.Text = "Disease";
            // 
            // diseaseBox
            // 
            this.diseaseBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.diseaseBox.Location = new System.Drawing.Point(131, 220);
            this.diseaseBox.Name = "diseaseBox";
            this.diseaseBox.Size = new System.Drawing.Size(592, 27);
            this.diseaseBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe Print", 13F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label3.Location = new System.Drawing.Point(47, 256);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 31);
            this.label3.TabIndex = 4;
            this.label3.Text = "Adding Medicine";
            // 
            // AddNewMedicine
            // 
            this.AddNewMedicine.BackColor = System.Drawing.Color.Transparent;
            this.AddNewMedicine.Controls.Add(this.addMedicine);
            this.AddNewMedicine.Controls.Add(this.medicineName);
            this.AddNewMedicine.Location = new System.Drawing.Point(51, 290);
            this.AddNewMedicine.Name = "AddNewMedicine";
            this.AddNewMedicine.Size = new System.Drawing.Size(672, 42);
            this.AddNewMedicine.TabIndex = 9;
            // 
            // addMedicine
            // 
            this.addMedicine.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.addMedicine.Location = new System.Drawing.Point(535, 8);
            this.addMedicine.Name = "addMedicine";
            this.addMedicine.Size = new System.Drawing.Size(85, 27);
            this.addMedicine.TabIndex = 2;
            this.addMedicine.Text = "Add";
            this.addMedicine.UseVisualStyleBackColor = true;
            this.addMedicine.Click += new System.EventHandler(this.AddMedicine_Click);
            // 
            // medicineName
            // 
            this.medicineName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.medicineName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.medicineName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.medicineName.FormattingEnabled = true;
            this.medicineName.Location = new System.Drawing.Point(12, 7);
            this.medicineName.Name = "medicineName";
            this.medicineName.Size = new System.Drawing.Size(410, 28);
            this.medicineName.TabIndex = 0;
            this.medicineName.SelectedIndexChanged += new System.EventHandler(this.checkDisableEnable);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(567, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(130, 130);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // finish
            // 
            this.finish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.finish.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.finish.Location = new System.Drawing.Point(1120, 581);
            this.finish.Name = "finish";
            this.finish.Size = new System.Drawing.Size(105, 30);
            this.finish.TabIndex = 14;
            this.finish.Text = "Complete";
            this.finish.UseVisualStyleBackColor = true;
            // 
            // scrollablePanel
            // 
            this.scrollablePanel.AutoScroll = true;
            this.scrollablePanel.BackColor = System.Drawing.Color.Transparent;
            this.scrollablePanel.Controls.Add(this.medicineListTable);
            this.scrollablePanel.Location = new System.Drawing.Point(51, 331);
            this.scrollablePanel.Name = "scrollablePanel";
            this.scrollablePanel.Size = new System.Drawing.Size(672, 235);
            this.scrollablePanel.TabIndex = 15;
            // 
            // medicineListTable
            // 
            this.medicineListTable.AutoSize = true;
            this.medicineListTable.BackColor = System.Drawing.Color.Transparent;
            this.medicineListTable.ColumnCount = 3;
            this.medicineListTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.medicineListTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 432F));
            this.medicineListTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 157F));
            this.medicineListTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.medicineListTable.Font = new System.Drawing.Font("Consolas", 12F);
            this.medicineListTable.Location = new System.Drawing.Point(0, 0);
            this.medicineListTable.Name = "medicineListTable";
            this.medicineListTable.RowCount = 1;
            this.medicineListTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.medicineListTable.Size = new System.Drawing.Size(649, 30);
            this.medicineListTable.TabIndex = 13;
            // 
            // historyPanel
            // 
            this.historyPanel.BackColor = System.Drawing.Color.Transparent;
            this.historyPanel.Location = new System.Drawing.Point(771, 220);
            this.historyPanel.Name = "historyPanel";
            this.historyPanel.Size = new System.Drawing.Size(454, 346);
            this.historyPanel.TabIndex = 16;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this.fullName);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(251, 167);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel1.Size = new System.Drawing.Size(472, 31);
            this.flowLayoutPanel1.TabIndex = 20;
            // 
            // fullName
            // 
            this.fullName.AutoSize = true;
            this.fullName.BackColor = System.Drawing.Color.Transparent;
            this.fullName.Font = new System.Drawing.Font("Segoe Print", 13F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.fullName.Location = new System.Drawing.Point(314, 0);
            this.fullName.Name = "fullName";
            this.fullName.Size = new System.Drawing.Size(155, 31);
            this.fullName.TabIndex = 20;
            this.fullName.Text = "Name Surname";
            // 
            // DiagnosisWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1249, 636);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.historyPanel);
            this.Controls.Add(this.scrollablePanel);
            this.Controls.Add(this.finish);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.AddNewMedicine);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.diseaseBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nameEdit);
            this.Controls.Add(this.patientHistory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DiagnosisWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Diagnosis";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DiagnosisWindow_Paint);
            this.Resize += new System.EventHandler(this.DiagnosisWindow_Resize);
            this.AddNewMedicine.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.scrollablePanel.ResumeLayout(false);
            this.scrollablePanel.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button patientHistory;
        private System.Windows.Forms.Label nameEdit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox diseaseBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel AddNewMedicine;
        private System.Windows.Forms.Button addMedicine;
        private System.Windows.Forms.ComboBox medicineName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button finish;
        private System.Windows.Forms.Panel scrollablePanel;
        private System.Windows.Forms.TableLayoutPanel medicineListTable;
        private System.Windows.Forms.Panel historyPanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label fullName;
    }
}

