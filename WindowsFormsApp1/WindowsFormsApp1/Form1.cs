using System.Data.SqlClient;
using System.Windows.Forms;
using System;
using System.Configuration;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            txtPassword.PasswordChar = '●'; // Şifreyi gizle
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Please write username and/or password", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["SchoolDBConnection"].ConnectionString;
            string querySelect = "SELECT ID, ROLE, NAME FROM USERTABLE WHERE USER_NAME=@username AND PASSWORD=@password";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(querySelect, conn))
            {
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                try
                {
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int userId = reader.GetInt32(0); // 0. sütun = ID
                        string userRole = reader.GetString(1); // 1. sütun = ROLE
                        string userName = reader.GetString(2); // NAME
                        MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblMessage.Text = "Welcome " + txtUsername.Text;

                        // Kullanıcı rolüne göre yönlendirme
                        if (userRole == "ADMIN")
                        {
                            // Admin için HomePage açılacak
                            HomePage homePage = new HomePage(userId); // userId ile HomePage'e geçiş yap
                            homePage.FormClosed += (s, args) => Application.Exit();
                            homePage.Show();
                        }
                        else if (userRole == "STUDENT")
                        {
                            // STUDENT tablosundaki gerçek ID'yi ve isim bilgilerini alıyoruz
                            string fullName = "";
                            int studentDbId = -1;

                            string queryStudent = "SELECT ID, NAME + ' ' + SURNAME AS FullName FROM STUDENT WHERE STUDENT_ID = @userId";

                            using (SqlConnection conn2 = new SqlConnection(connectionString))
                            using (SqlCommand cmd2 = new SqlCommand(queryStudent, conn2))
                            {
                                cmd2.Parameters.AddWithValue("@userId", userId); // Bu userId, USERTABLE.ID

                                conn2.Open();
                                SqlDataReader reader2 = cmd2.ExecuteReader();

                                if (reader2.Read())
                                {
                                    studentDbId = reader2.GetInt32(0); // STUDENT.ID
                                    fullName = reader2.GetString(1);   // NAME + SURNAME

                                    // Öğrenci sayfasını aç
                                    StudentHomePage studentHomePage = new StudentHomePage(fullName, studentDbId);
                                    studentHomePage.FormClosed += (s, args) => Application.Exit();
                                    studentHomePage.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("Student record not found in STUDENT table.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }

                        this.Hide();  // Giriş ekranını gizle
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database connection error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


    }
}