using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using HospitalClasses;
using HospitalForms;


namespace HospitalForms
{
    public partial class AdminProfileWindow : Form
    {
        public event EventHandler logOutClicked;

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public Bitmap byteArrayToImage(byte[] byteArrayIn, Size size)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Bitmap returnImage = new Bitmap(new Bitmap(ms), size);
            return returnImage;
        }

        static Bitmap removeIcon, editIcon, defaultMedicine;
        List<Doctor> doctors;
        List<Medicine> medicines;
        Admin admin;

        public AdminProfileWindow(Admin adm)
        {
            InitializeComponent();
            admin = adm;
        }

        private void AdminProfileWindow_Load(object sender, EventArgs e)
        {
            doctorsPanel.Hide();
            medicinePanel.Hide();

            nameLabel.Text = admin.Name;
            surnameLabel.Text = admin.Surname;

            removeIcon = new Bitmap((global::HospitalVRRAM.Properties.Resources.fileclose), new Size(20, 20));
            editIcon = new Bitmap((global::HospitalVRRAM.Properties.Resources.EditSign), new Size(20, 20));
            defaultMedicine = new Bitmap((global::HospitalVRRAM.Properties.Resources.medtry1), new Size(30, 30));
            profilePicBox.Image = new Bitmap((global::HospitalVRRAM.Properties.Resources.DefaultProfilePic));

            newCost.TextChanged += checkDoctor;
            newName.TextChanged += checkDoctor;
            newSurname.TextChanged += checkDoctor;
            newPassportID.TextChanged += checkDoctor;
            newSpec.SelectedIndexChanged += checkDoctor;
            newPhone.TextChanged += checkDoctor;
            newLogin.TextChanged += checkDoctor;
            newPassword.TextChanged += checkDoctor;
            newPassword.UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password;

            medicineName.TextChanged += checkMedicine;
            medicinePrice.TextChanged += checkMedicine;
            medicineCountry.TextChanged += checkMedicine;

            addMedicine.Enabled = false;
            addDoctor.Enabled = false;

            newSpec.Items.AddRange(Enum.GetNames(typeof(Specialities)));

            doctors = admin.ShowDoctors();

            medicines = admin.ShowMedicine();         
        }

        private void allDoctors_Click(object sender, EventArgs e)
        {
            
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

            medicinePanel.Hide();
            doctorsPanel.Show();
        }

        private void removeDoctor(object sender, EventArgs e, Doctor doctor)
        {
            DialogResult res = MessageBox.Show("Are you sure you want to fire the doctor " + doctor.Name + " " + doctor.Surname + "?", "Fire", MessageBoxButtons.YesNo);
            if (res == DialogResult.No)
                return;

            doctors.Remove(doctor);
            admin.DeleteDoctor(doctor);
            allDoctors_Click(0, EventArgs.Empty);
        }

        private void checkDoctor(object sender, EventArgs e)
        {
            if (newName.Text.Length == 0 || newSurname.Text.Length == 0 || newPassportID.Text.Length == 0 || newPhone.Text.Length == 0 ||
                newCost.Text.Length == 0 || newSpec.SelectedIndex == -1 || newLogin.Text.Length == 0 || newPassword.Text.Length == 0)
                addDoctor.Enabled = false;
            else
                addDoctor.Enabled = true;
        }

        private void checkIfValid()
        {
            if (newName.Text.Length == 0 || newSurname.Text.Length == 0 || newPassportID.Text.Length == 0 ||
                newPhone.Text.Length == 0 || newLogin.Text.Length == 0 || newPassword.Text.Length == 0)
                throw new Exception("Some fields are required!");

            if (newBirth.Value > DateTime.Now)
            {
                throw new Exception("Date of birth must be past! Not future.");
            }

            try
            {
                User.CheckPassportID(newPassportID.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                User.LoginIsValid(newLogin.Text, typeof(Patient));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                User.PasswordIsValid(newPassword.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void addDoctor_Click(object sender, EventArgs e)
        {
            try
            {
                checkIfValid();
                Doctor doctor = new Doctor(newName.Text, newSurname.Text, newPassportID.Text, newLogin.Text, User.getHashSha256(newPassword.Text),
                newSpec.Text, newEmployDate.Value, decimal.Parse(newCost.Text), newBirth.Value, newPhone.Text);

                newName.Text = newSurname.Text = newPassportID.Text = newPassword.Text = newLogin.Text = newCost.Text = newPhone.Text = "";
                newSpec.SelectedIndex = -1;

                doctors.Add(doctor);
                allDoctors_Click(0, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning");
            }
        }

        private void allMedicine_Click(object sender, EventArgs e)
        {
            medicineTable.RowCount = 1;
            medicineTable.Controls.Clear();
            medicineTable.Font = new Font("Consolas", 10F);

            foreach(var medicine in medicines)
            {
                medicineTable.Controls.Add(new Label() { Text = medicineTable.RowCount.ToString() });
                medicineTable.Controls.Add(new Label() { Text = medicine.Name, Width = 140 });
                medicineTable.Controls.Add(new Label() { Image = (!(medicine.Picture == null) ? byteArrayToImage(medicine.Picture, new Size(30, 30)) : defaultMedicine) });
                medicineTable.Controls.Add(new Label() { Text = medicine.ExpiryDate.ToShortDateString() } );
                medicineTable.Controls.Add(new Label() { Text = medicine.Country });

                TextBox price = new TextBox() { Text = medicine.Price.ToString(), Enabled = false };
                medicineTable.Controls.Add(price);

                Label remove = new Label() { Image = removeIcon };
                remove.Click += (senderr, ee) => removeMedicine(senderr, ee, medicine);
                medicineTable.Controls.Add(remove);

                Label edit = new Label() { Image = editIcon };
                edit.Click += (senderr, ee) => editPriceOfMedicine(senderr, ee, medicine, price);
                medicineTable.Controls.Add(edit);

                medicineTable.RowCount++;
            }

            doctorsPanel.Hide();
            medicinePanel.Show();
        }

        private void removeMedicine(object sender, EventArgs e, Medicine medicine)
        {
            DialogResult res = MessageBox.Show("Are you sure you don't want any of the medicine: " + medicine.Name + "?", "Medicine", MessageBoxButtons.YesNo);
            if (res == DialogResult.No)
                return;
            medicines.Remove(medicine);
            admin.DeleteMedicine(medicine);
            allMedicine_Click(0, EventArgs.Empty);
        }

        private void editPriceOfMedicine(object sender, EventArgs e, Medicine medicine, TextBox pricee)
        {
            if (pricee.Enabled)
            {
                pricee.Enabled = false;
                admin.ChangePrice(medicine, Convert.ToDecimal(pricee.Text));
            }
            else
            {
                pricee.Enabled = true;
            }
        }

        private void checkMedicine(object sender, EventArgs e)
        {
            if (medicineName.Text.Length == 0 || medicineCountry.Text.Length == 0 || medicinePrice.Text.Length == 0)
                addMedicine.Enabled = false;
            else
                addMedicine.Enabled = true;
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

        private void addMedicine_Click(object sender, EventArgs e)
        {
            Medicine medicine = new Medicine(medicineName.Text, medicineCountry.Text, Convert.ToDecimal(medicinePrice.Text), medicineExpire.Value);
            admin.AddMedicine(medicine);

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
                            medicine.AddPicture(imageToByteArray(new Bitmap(myStream)));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

            medicineCountry.Text = medicineName.Text = medicinePrice.Text = "";

            medicines.Add(medicine);
            allMedicine_Click(0, EventArgs.Empty);
        }

        private void logOut_Click(object sender, EventArgs e)
        {
            logOutClicked(this, EventArgs.Empty);
        }
    }
}
