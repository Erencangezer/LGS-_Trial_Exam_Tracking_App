using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;


namespace WindowsFormsApp1
{
    public partial class StudentHomePage : Form
    {
        private string studentFullName;
        private int studentId;

        public StudentHomePage(string fullName, int id)
        {
            InitializeComponent();
            studentFullName = fullName;
            studentId = id;
            lblWelcomeStudent.Text = $"Welcome {studentFullName}";
        }

        

        private void btnAddTrialExam_Click(object sender, EventArgs e)
        {
            TrialExamForm form = new TrialExamForm(studentId); // Bu id, artık STUDENT tablosundaki ID!
            form.ShowDialog();
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            StudentReportForm reportForm = new StudentReportForm(studentId, studentFullName);
            reportForm.Show();
        }
    }
}
