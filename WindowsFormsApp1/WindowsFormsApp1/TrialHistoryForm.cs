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
    public partial class TrialHistoryForm : Form
    {
        public TrialHistoryForm()
        {
            InitializeComponent();
        }

        private void TrialHistoryForm_Load(object sender, EventArgs e)
        {
            LoadTrialHistory();

            string connectionString = ConfigurationManager.ConnectionStrings["SchoolDBConnection"].ConnectionString;
            string query = "SELECT ID, NAME + ' ' + SURNAME AS FullName FROM STUDENT";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Dictionary<int, string> studentDict = new Dictionary<int, string>();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string fullName = reader.GetString(1);
                    studentDict.Add(id, fullName);
                }

                cmbStudents.DataSource = new BindingSource(studentDict, null);
                cmbStudents.DisplayMember = "Value";
                cmbStudents.ValueMember = "Key";
            }

        }

        private void btnLoadHistory_Click(object sender, EventArgs e)
        {
            LoadTrialHistory();
        }

        private void LoadTrialHistory()
        {
            string query = @"
        SELECT 
            S.NAME + ' ' + S.SURNAME AS [Öğrenci Adı Soyadı],
            TE.MATH_CORRECT AS [Matematik Doğru],
            TE.MATH_WRONG AS [Matematik Yanlış],
            TE.MATH_EMPTY AS [Matematik Boş],
            TE.SCIENCE_CORRECT AS [Fen Doğru],
            TE.SCIENCE_WRONG AS [Fen Yanlış],
            TE.SCIENCE_EMPTY AS [Fen Boş],
            TE.TURKISH_CORRECT AS [Türkçe Doğru],
            TE.TURKISH_WRONG AS [Türkçe Yanlış],
            TE.TURKISH_EMPTY AS [Türkçe Boş],
            TE.HISTORY_CORRECT AS [İnkılap Doğru],
            TE.HISTORY_WRONG AS [İnkılap Yanlış],
            TE.HISTORY_EMPTY AS [İnkılap Boş],
            TE.RELIGION_CORRECT AS [Din Doğru],
            TE.RELIGION_WRONG AS [Din Yanlış],
            TE.RELIGION_EMPTY AS [Din Boş],
            TE.ENGLISH_CORRECT AS [İngilizce Doğru],
            TE.ENGLISH_WRONG AS [İngilizce Yanlış],
            TE.ENGLISH_EMPTY AS [İngilizce Boş]
        FROM 
            TRIAL_EXAM TE
        JOIN 
            STUDENT S ON TE.STUDENT_ID = S.ID
        ORDER BY 
            S.NAME, S.SURNAME";

            string connectionString = ConfigurationManager.ConnectionStrings["SchoolDBConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvTrialHistory.DataSource = dt;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchName = txtSearchName.Text.Trim();
            if (string.IsNullOrEmpty(searchName))
            {
                MessageBox.Show("Lütfen aramak için bir isim girin.");
                return;
            }

            string query = @"
        SELECT 
            S.NAME + ' ' + S.SURNAME AS [Öğrenci Adı Soyadı],
            TE.MATH_CORRECT AS [Matematik Doğru],
            TE.MATH_WRONG AS [Matematik Yanlış],
            TE.MATH_EMPTY AS [Matematik Boş],
            TE.SCIENCE_CORRECT AS [Fen Doğru],
            TE.SCIENCE_WRONG AS [Fen Yanlış],
            TE.SCIENCE_EMPTY AS [Fen Boş],
            TE.TURKISH_CORRECT AS [Türkçe Doğru],
            TE.TURKISH_WRONG AS [Türkçe Yanlış],
            TE.TURKISH_EMPTY AS [Türkçe Boş],
            TE.HISTORY_CORRECT AS [İnkılap Doğru],
            TE.HISTORY_WRONG AS [İnkılap Yanlış],
            TE.HISTORY_EMPTY AS [İnkılap Boş],
            TE.RELIGION_CORRECT AS [Din Doğru],
            TE.RELIGION_WRONG AS [Din Yanlış],
            TE.RELIGION_EMPTY AS [Din Boş],
            TE.ENGLISH_CORRECT AS [İngilizce Doğru],
            TE.ENGLISH_WRONG AS [İngilizce Yanlış],
            TE.ENGLISH_EMPTY AS [İngilizce Boş]
        FROM TRIAL_EXAM TE
        JOIN STUDENT S ON TE.STUDENT_ID = S.ID
        WHERE S.NAME + ' ' + S.SURNAME LIKE @search";

            string connectionString = ConfigurationManager.ConnectionStrings["SchoolDBConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                da.SelectCommand.Parameters.AddWithValue("@search", "%" + searchName + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvTrialHistory.DataSource = dt;
            }
        }

        private void btnLoadSelected_Click(object sender, EventArgs e)
        {
            if (cmbStudents.SelectedItem == null) return;

            int selectedStudentId = ((KeyValuePair<int, string>)cmbStudents.SelectedItem).Key;

            string query = @"
        SELECT 
            S.NAME + ' ' + S.SURNAME AS [Öğrenci Adı Soyadı],
            TE.MATH_CORRECT AS [Matematik Doğru],
            TE.MATH_WRONG AS [Matematik Yanlış],
            TE.MATH_EMPTY AS [Matematik Boş],
            TE.SCIENCE_CORRECT AS [Fen Doğru],
            TE.SCIENCE_WRONG AS [Fen Yanlış],
            TE.SCIENCE_EMPTY AS [Fen Boş],
            TE.TURKISH_CORRECT AS [Türkçe Doğru],
            TE.TURKISH_WRONG AS [Türkçe Yanlış],
            TE.TURKISH_EMPTY AS [Türkçe Boş],
            TE.HISTORY_CORRECT AS [İnkılap Doğru],
            TE.HISTORY_WRONG AS [İnkılap Yanlış],
            TE.HISTORY_EMPTY AS [İnkılap Boş],
            TE.RELIGION_CORRECT AS [Din Doğru],
            TE.RELIGION_WRONG AS [Din Yanlış],
            TE.RELIGION_EMPTY AS [Din Boş],
            TE.ENGLISH_CORRECT AS [İngilizce Doğru],
            TE.ENGLISH_WRONG AS [İngilizce Yanlış],
            TE.ENGLISH_EMPTY AS [İngilizce Boş]
        FROM TRIAL_EXAM TE
        JOIN STUDENT S ON TE.STUDENT_ID = S.ID
        WHERE S.ID = @studentId";

            string connectionString = ConfigurationManager.ConnectionStrings["SchoolDBConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                da.SelectCommand.Parameters.AddWithValue("@studentId", selectedStudentId);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvTrialHistory.DataSource = dt;
            }
        }

    }

}
