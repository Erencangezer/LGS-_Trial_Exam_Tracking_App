using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

namespace WindowsFormsApp1
{
    public partial class AddForm: Form
    {
        public AddForm()
        {
            InitializeComponent();
        }

        /*public partial class SummaryForm : Form
        {
            private decimal totalSummarySalesDecimal;

            public decimal TotalSales
            {
                set
                {
                    totalSummarySalesDecimal = value;
                }
            }
        } */

        private void Register_Load(object sender, EventArgs e)
        {
            string[] cities = { "Adana", "Adıyaman", "Afyonkarahisar", "Ağrı", "Amasya", "Ankara",
                        "Antalya", "Artvin", "Aydın", "Balıkesir", "Bilecik", "Bingöl",
                        "Bitlis", "Bolu", "Burdur", "Bursa", "Çanakkale", "Çankırı",
                        "Çorum", "Denizli", "Diyarbakır", "Edirne", "Elazığ", "Erzincan",
                        "Erzurum", "Eskişehir", "Gaziantep", "Giresun", "Gümüşhane",
                        "Hakkari", "Hatay", "Isparta", "Mersin", "İstanbul", "İzmir",
                        "Kars", "Kastamonu", "Kayseri", "Kırklareli", "Kırşehir", "Kocaeli",
                        "Konya", "Kütahya", "Malatya", "Manisa", "Kahramanmaraş",
                        "Mardin", "Muğla", "Muş", "Nevşehir", "Niğde", "Ordu",
                        "Rize", "Sakarya", "Samsun", "Siirt", "Sinop", "Sivas",
                        "Tekirdağ", "Tokat", "Trabzon", "Tunceli", "Şanlıurfa",
                        "Uşak", "Van", "Yozgat", "Zonguldak", "Aksaray", "Bayburt",
                        "Karaman", "Kırıkkale", "Batman", "Şırnak", "Bartın",
                        "Ardahan", "Iğdır", "Yalova", "Karabük", "Kilis",
                        "Osmaniye", "Düzce" };

            cmbCity.Items.AddRange(cities);

            cmbRole.Items.Add("STUDENT");
            cmbRole.Items.Add("ADMIN");
            cmbRole.SelectedIndex = 0;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Terms checkbox kontrolü
            if (!chkTerms.Checked)
            {
                MessageBox.Show("Kayıt olabilmek için şartları kabul etmelisiniz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gender kontrolü
            if (!rbGenderMale.Checked && !rbGenderFemale.Checked)
            {
                MessageBox.Show("Lütfen cinsiyet seçiniz!", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtName.Text == "" || txtSurname.Text == "" || txtEmail.Text == "" ||
                txtPassword.Text == "" || txtPhoneNumber.Text == "" || cmbCity.SelectedIndex == -1 ||
                txtPostalCode.Text == "" || txtAddress.Text == "")
            {
                MessageBox.Show("Alanlar boş bırakılamaz!", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Posta kodunun geçerli bir sayı olup olmadığını kontrol edelim.
            int postalCode;
            if (!int.TryParse(txtPostalCode.Text, out postalCode))
            {
                MessageBox.Show("Posta kodu sadece rakamlardan oluşmalıdır!", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // SQL bağlantı dizesi
            string connectionString = ConfigurationManager.ConnectionStrings["SchoolDBConnection"].ConnectionString;

            // Aynı email var mı kontrol edelim
            string queryCheck = "SELECT COUNT(*) FROM USERTABLE WHERE EMAIL=@Email";
            string queryInsert = "INSERT INTO USERTABLE (ROLE, USER_NAME, PASSWORD, NAME, SURNAME, EMAIL, PHONE_NUMBER, CITY, POSTAL_CODE, ADDRESS, GENDER) " +
                     "VALUES (@Role, @UserName, @Password, @Name, @Surname, @Email, @PhoneNumber, @City, @PostalCode, @Address, @Gender)";


            // Cinsiyeti al
            string gender = rbGenderMale.Checked ? "Erkek" : "Kadın";
            //string gender = rbGenderFemale.Checked ? "Erkek" : "Kadın";



            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Kullanıcının daha önce kayıt olup olmadığını kontrol et
                using (SqlCommand checkCmd = new SqlCommand(queryCheck, conn))
                {
                    checkCmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    int userExists = (int)checkCmd.ExecuteScalar();

                    if (userExists > 0)
                    {
                        MessageBox.Show("Bu email adresi zaten kayıtlı!", "Hata",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Kullanıcıyı kaydet
                using (SqlCommand cmd = new SqlCommand(queryInsert, conn))
                {
                    cmd.Parameters.AddWithValue("@Role", cmbRole.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@UserName", txtUsername.Text); // veya kullanıcı adı alanı varsa onu kullan
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Surname", txtSurname.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text);
                    cmd.Parameters.AddWithValue("@City", cmbCity.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PostalCode", postalCode);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@Gender", gender);


                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Kayıt başarılı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close(); // Formu kapatabiliriz veya temizleyebiliriz.
                    }
                    else
                    {
                        MessageBox.Show("Kayıt başarısız!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        
    }
}
