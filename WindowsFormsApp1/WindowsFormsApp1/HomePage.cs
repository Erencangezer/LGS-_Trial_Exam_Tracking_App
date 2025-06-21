using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;


namespace WindowsFormsApp1
{
    public partial class HomePage : Form
    {
        private int userId;

        public HomePage(int id)
        {
            InitializeComponent();
            userId = id;
            btnAddCourse.Visible = false;
        }

        private void HomePage_Load(object sender, EventArgs e)
        {
            lblUserID.Text = $"ID: {userId}";

            string connectionString = ConfigurationManager.ConnectionStrings["SchoolDBConnection"].ConnectionString;
            string query = "SELECT NAME, SURNAME FROM USERTABLE WHERE ID = @id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", userId);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string name = reader["NAME"].ToString();
                            string surname = reader["SURNAME"].ToString();
                            lblWelcome.Text = $"Welcome {name} {surname}";
                        }
                        else
                        {
                            lblWelcome.Text = "Welcome";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading user info: " + ex.Message);
                }
            }

        }

        private void btnStudentList_Click(object sender, EventArgs e)
        {
            StudentList studentList = new StudentList();
            studentList.Show();
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            AddForm registerForm = new AddForm();
            registerForm.Show();
        }

        private void btnAddCourse_Click(object sender, EventArgs e)
        {
            CourseForm courseForm = new CourseForm();
            courseForm.ShowDialog();
        }

        private void btnAddTrialExam_Click(object sender, EventArgs e)
        {
            TrialExamForm trialExamForm = new TrialExamForm();
            trialExamForm.Show();  
        }

        private void btnTrialHistory_Click(object sender, EventArgs e)
        {
            TrialHistoryForm historyForm = new TrialHistoryForm();
            historyForm.ShowDialog();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            AdminReportForm reportForm = new AdminReportForm();
            reportForm.Show();
        }
    }

}
