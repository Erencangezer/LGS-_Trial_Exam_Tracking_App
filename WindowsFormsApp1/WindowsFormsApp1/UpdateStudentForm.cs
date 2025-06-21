using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;

namespace WindowsFormsApp1
{
    public partial class UpdateStudentForm : Form
    {
        private Student student;
        string connectionString = ConfigurationManager.ConnectionStrings["SchoolDBConnection"].ConnectionString;

        public UpdateStudentForm(Student std)
        {
            InitializeComponent();
            student = std;
        }

        private void UpdateStudentForm_Load(object sender, EventArgs e)
        {
            // Şehirleri yükle
            string[] cities = { "Adana", "Adıyaman", "Afyonkarahisar", "Ağrı", "Amasya", "Ankara", "Antalya", "Artvin", "Aydın", 
                "Balıkesir", "Bilecik", "Bingöl", "Bitlis", "Bolu", "Burdur", "Bursa", "Çanakkale", "Çankırı", "Çorum", "Denizli", 
                "Diyarbakır", "Edirne", "Elazığ", "Erzincan", "Erzurum", "Eskişehir", "Gaziantep", "Giresun", "Gümüşhane", "Hakkari", 
                "Hatay", "Isparta", "Mersin", "İstanbul", "İzmir", "Kars", "Kastamonu", "Kayseri", "Kırklareli", "Kırşehir", "Kocaeli", 
                "Konya", "Kütahya", "Malatya", "Manisa", "Kahramanmaraş", "Mardin", "Muğla", "Muş", "Nevşehir", "Niğde", "Ordu", "Rize", 
                "Sakarya", "Samsun", "Siirt", "Sinop", "Sivas", "Tekirdağ", "Tokat", "Trabzon", "Tunceli", "Şanlıurfa", "Uşak", "Van", 
                "Yozgat", "Zonguldak", "Aksaray", "Bayburt", "Karaman", "Kırıkkale", "Batman", "Şırnak", "Bartın", "Ardahan", "Iğdır", 
                "Yalova", "Karabük", "Kilis", "Osmaniye", "Düzce" };
            cmbCity.Items.AddRange(cities);

            // Öğrenci bilgilerini forma yükle
            txtName.Text = student.Name;
            txtSurname.Text = student.Surname;
            txtEmail.Text = student.Email;
            txtPhoneNumber.Text = student.PhoneNumber;
            cmbCity.SelectedItem = student.City;
            txtAddress.Text = student.Address;
            txtPostalCode.Text = student.PostalCode.ToString();
            if (student.Gender == "Male") rbMale.Checked = true;
            else if (student.Gender == "Female") rbFemale.Checked = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string gender = rbMale.Checked ? "Male" : rbFemale.Checked ? "Female" : "";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        UPDATE STUDENT SET 
                            NAME = @Name,
                            SURNAME = @Surname,
                            EMAIL = @Email,
                            PHONE_NUMBER = @PhoneNumber,
                            CITY = @City,
                            ADDRESS = @Address,
                            POSTAL_CODE = @PostalCode,
                            GENDER = @Gender
                        WHERE ID = @ID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Surname", txtSurname.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text.Trim());
                    cmd.Parameters.AddWithValue("@City", cmbCity.SelectedItem?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@PostalCode", int.TryParse(txtPostalCode.Text.Trim(), out int pCode) ? pCode : 0);
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    cmd.Parameters.AddWithValue("@ID", student.ID);

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Student updated successfully!");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Update failed.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
