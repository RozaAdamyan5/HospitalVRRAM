using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HospitalClasses;
using HospitalForms;


namespace HospitalForms
{
    public partial class AdminProfileWindow : Form
    {
        static Bitmap removeIcon;
        List<Doctor> doctors;
        List<Medicine> medicines;
        Admin admin;

        public AdminProfileWindow()
        {
            InitializeComponent();
            universalPanel.Hide();
            doctorsPanel.Hide();
            removeIcon = new Bitmap((Bitmap)(global::HospitalVRRAM.Properties.Resources.fileclose), new Size(20, 20));

            newCost.TextChanged += check;
            newName.TextChanged += check;
            newSurname.TextChanged += check;
            newPassportID.TextChanged += check;
            newSpec.TextChanged += check;
            newLogin.TextChanged += check;
            newPassword.TextChanged += check;

            addDoctor.Enabled = false;

            // Load all doctors from db
            // doctors = Admin.ShowDoctors();

            doctors = new List<Doctor>() {
                new Doctor("Aaa", "BBB", 513, "aaa", new DateTime(), 686),
                new Doctor("Aba", "BtB", 513, "aaa", new DateTime(), 686),
                new Doctor("Aca", "BrB", 513, "aaa", new DateTime(), 686),
                new Doctor("Ada", "BBe", 513, "aaa", new DateTime(), 686),
                new Doctor("Aea", "BcB", 513, "aaa", new DateTime(), 686),
                new Doctor("Ara", "BrB", 513, "aaa", new DateTime(), 686),
                new Doctor("Ata", "BBe", 513, "aaa", new DateTime(), 686),
                new Doctor("Aya", "BcB", 513, "aaa", new DateTime(), 686)};
        }

        private void allDoctors_Click(object sender, EventArgs e)
        {
            //universalPanel.Controls.Clear();
            universalPanel.Hide();
            // TODO
            // Show all doctors in doctorsPanel
            
            doctorsTable.Controls.Clear();
            doctorsTable.RowCount = 1;

            foreach(var doc in doctors)
            {
                doctorsTable.Controls.Add(new Label() { Text = doctorsTable.RowCount.ToString() });
                doctorsTable.Controls.Add(new Label() { Text = doc.Name });
                doctorsTable.Controls.Add(new Label() { Text = doc.Surname });
                doctorsTable.Controls.Add(new Label() { Text = doc.PassportID.ToString() });
                Label remover = new Label() { Image = removeIcon };
                remover.Click += (senderr, ee) => removeDoctor(senderr, ee, doc);
                doctorsTable.Controls.Add(remover);

                doctorsTable.RowCount++;
            }

            doctorsPanel.Show();
            universalPanel.Hide();
            
        }

        private void removeDoctor(object sender, EventArgs e, Doctor doctor)
        {
            DialogResult res = MessageBox.Show("Are you sure you want to fire the doctor " + doctor.Name + " " + doctor.Surname + "?", "Fire", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
                doctors.Remove(doctor);
            //admin.DeleteDoctor(doctor);
            allDoctors_Click(0, EventArgs.Empty);
        }

        private void check(object sender, EventArgs e)
        {
            if (newName.Text.Length == 0 || newSurname.Text.Length == 0 || newPassportID.Text.Length == 0 ||
                newCost.Text.Length == 0 || newSpec.Text.Length == 0 || newLogin.Text.Length == 0 || newPassword.Text.Length == 0)
                addDoctor.Enabled = false;
            else
                addDoctor.Enabled = true;
        }

        private void addDoctor_Click(object sender, EventArgs e)
        {
            Doctor doctor = new Doctor(newName.Text, newSurname.Text, int.Parse(newPassportID.Text),
                newSpec.Text, DateTime.Parse(newEmployDate.Text), decimal.Parse(newCost.Text));


            newName.Text = newSurname.Text = newPassportID.Text = newPassword.Text = newLogin.Text = newSpec.Text = newCost.Text = "";
            doctors.Add(doctor);
            //admin.AddDoctor(doctor);
            allDoctors_Click(0, EventArgs.Empty);
        }

        private void allMedicine_Click(object sender, EventArgs e)
        {
            universalPanel.Controls.Clear();
            // TODO
            // Show all medicine in universalPanel
            doctorsPanel.Hide();
            universalPanel.Show();
        }

        private void changePassword_Click(object sender, EventArgs e)
        {
            universalPanel.Controls.Clear();

            var tmpFont = new Font("Microsoft Sans Serif", 10F);

            Label oldPasswordLabel = new Label() { Text = "Old Password", Width = 140, Left = 20, Top = 22, Font = tmpFont };
            Label newPasswordLabel = new Label() { Text = "New Password", Width = 140, Left = 20, Top = 52, Font = tmpFont };
            Label confirmPasswordLabel = new Label() { Text = "Confirm Password", Width = 140, Left = 20, Top = 82, Font = tmpFont };
            TextBox oldPassword = new TextBox() { Width = 180, Height = 23, Left = 160, Top = 20, UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password, Font = tmpFont };
            TextBox newPassword = new TextBox() { Width = 180, Height = 23, Left = 160, Top = 50, UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password, Font = tmpFont };
            TextBox confirmPassword = new TextBox() { Width = 180, Height = 23, Left = 160, Top = 80, UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password, Font = tmpFont };
            Button saveButton = new Button() { Text = "Save", Left = 260, Top = 110, Font = tmpFont, Height = 30 };

            universalPanel.Controls.AddRange(
                new Control[] { oldPasswordLabel, oldPassword, newPasswordLabel, newPassword, confirmPasswordLabel, confirmPassword, saveButton });
        }

        private void addPicture_Click(object sender, EventArgs e)
        {
            Stream myStream;
            OpenFileDialog selectPicture = new OpenFileDialog() { InitialDirectory = "C:\\", Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG" };
            if (selectPicture.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = selectPicture.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            profilePicBox.Image = new Bitmap(myStream);
                            char[] chArr = (Convert.ToString(myStream)).ToCharArray();
                            // doctor.AddPicture(Array.ConvertAll(chArr, Convert.ToByte));
                            profilePicBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            addPicture.Text = "Change Picture"; addPicture.Left = 45;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void AdminProfileWindow_Paint(object sender, PaintEventArgs e)
        {
            if (this.ClientRectangle.IsEmpty)
                return;
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                                                               Color.GhostWhite,
                                                               Color.SteelBlue,
                                                               90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void AdminProfileWindow_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        
    }
}
