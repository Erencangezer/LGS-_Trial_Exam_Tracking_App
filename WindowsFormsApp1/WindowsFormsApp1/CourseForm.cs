using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class CourseForm : Form
    {
        public CourseForm()
        {
            InitializeComponent();
        }

        private void CourseForm_Load(object sender, EventArgs e)
        {
            LoadCourses();
        }

        private void btnAddCourse_Click(object sender, EventArgs e)
        {
            string courseName = txtCourseName.Text.Trim();
            string courseCode = txtCourseCode.Text.Trim();

            if (string.IsNullOrEmpty(courseName) || string.IsNullOrEmpty(courseCode))
            {
                MessageBox.Show("Please enter both course name and course code.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["SchoolDBConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO COURSE (COURSE_NAME, COURSE_CODE) VALUES (@name, @code)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", courseName);
                    cmd.Parameters.AddWithValue("@code", courseCode);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Course added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCourses(); // Refresh DataGridView
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error adding course: " + ex.Message);
                    }
                }
            }
        }

        private void LoadCourses()
        {
            string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=SchoolDB;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COURSE_NAME, COURSE_CODE FROM COURSE";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvCourses.DataSource = dt;

                    // Kolon başlıklarını değiştirme
                    dgvCourses.Columns[0].HeaderText = "Course";
                    dgvCourses.Columns[1].HeaderText = "Code";

                    // Kolon genişliklerini manuel olarak ayarlama
                    dgvCourses.Columns[0].Width = 150;  // Course sütununun genişliği
                    dgvCourses.Columns[1].Width = 100;  // Code sütununun genişliği
                }
            }
        }

    }
}
