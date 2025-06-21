using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;

namespace WindowsFormsApp1
{
    public partial class TrialExamForm : Form
    {
        private int? studentId;
        public TrialExamForm()
        {
            InitializeComponent();
        }

        public TrialExamForm(int studentId)
        {
            InitializeComponent(); // Öğrenci kullanımı
            this.studentId = studentId;
        }

        private void TrialExamForm_Load(object sender, EventArgs e)
        {
            // Eğer studentId varsa (öğrenci girişi), ComboBox gizlenir
            if (studentId.HasValue)
            {
                cmbStudent.Visible = false;
                label1.Visible = false;
            }
            else
            {
                LoadStudents(); // admin ekranıysa öğrencileri yükle
            }

            txtCorrectMath.TextChanged += (s, ev) => UpdateEmpty(txtCorrectMath, txtWrongMath, txtEmptyMath, 20);
            txtWrongMath.TextChanged += (s, ev) => UpdateEmpty(txtCorrectMath, txtWrongMath, txtEmptyMath, 20);

            txtCorrectScience.TextChanged += (s, ev) => UpdateEmpty(txtCorrectScience, txtWrongScience, txtEmptyScience, 20);
            txtWrongScience.TextChanged += (s, ev) => UpdateEmpty(txtCorrectScience, txtWrongScience, txtEmptyScience, 20);

            txtCorrectTurkish.TextChanged += (s, ev) => UpdateEmpty(txtCorrectTurkish, txtWrongTurkish, txtEmptyTurkish, 20);
            txtWrongTurkish.TextChanged += (s, ev) => UpdateEmpty(txtCorrectTurkish, txtWrongTurkish, txtEmptyTurkish, 20);

            txtCorrectHistory.TextChanged += (s, ev) => UpdateEmpty(txtCorrectHistory, txtWrongHistory, txtEmptyHistory, 10);
            txtWrongHistory.TextChanged += (s, ev) => UpdateEmpty(txtCorrectHistory, txtWrongHistory, txtEmptyHistory, 10);

            txtCorrectReligion.TextChanged += (s, ev) => UpdateEmpty(txtCorrectReligion, txtWrongReligion, txtEmptyReligion, 10);
            txtWrongReligion.TextChanged += (s, ev) => UpdateEmpty(txtCorrectReligion, txtWrongReligion, txtEmptyReligion, 10);

            txtCorrectEnglish.TextChanged += (s, ev) => UpdateEmpty(txtCorrectEnglish, txtWrongEnglish, txtEmptyEnglish, 10);
            txtWrongEnglish.TextChanged += (s, ev) => UpdateEmpty(txtCorrectEnglish, txtWrongEnglish, txtEmptyEnglish, 10);


        }

        private void UpdateEmpty(TextBox txtCorrect, TextBox txtWrong, TextBox txtEmpty, int totalQuestions)
        {
            if (int.TryParse(txtCorrect.Text, out int correct) && int.TryParse(txtWrong.Text, out int wrong))
            {
                int calculatedEmpty = totalQuestions - (correct + wrong);
                txtEmpty.Text = calculatedEmpty >= 0 ? calculatedEmpty.ToString() : "";
            }
            else
            {
                txtEmpty.Text = "";
            }
        }

        private void LoadStudents()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SchoolDBConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT ID, NAME FROM STUDENT";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable students = new DataTable();
                    adapter.Fill(students);

                    if (students.Rows.Count > 0)
                    {
                        cmbStudent.DataSource = null;
                        cmbStudent.DataSource = students;
                        cmbStudent.DisplayMember = "NAME";
                        cmbStudent.ValueMember = "ID";
                        cmbStudent.SelectedIndex = -1;
                    }
                    else
                    {
                        MessageBox.Show("No students found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private bool KontrolEt(string dersAdi, int dogru, int yanlis, int bos, int toplamSoru)
        {
            if (dogru + yanlis + bos != toplamSoru)
            {
                MessageBox.Show($"For the {dersAdi} course, the total of correct, wrong, and empty answers must equal {toplamSoru} questions.");
                return false;
            }
            return true;
        }


        private bool TryParseInputs(out int mathCorrect, out int mathWrong, out int mathEmpty,
                                    out int scienceCorrect, out int scienceWrong, out int scienceEmpty,
                                    out int turkishCorrect, out int turkishWrong, out int turkishEmpty,
                                    out int historyCorrect, out int historyWrong, out int historyEmpty,
                                    out int religionCorrect, out int religionWrong, out int religionEmpty,
                                    out int englishCorrect, out int englishWrong, out int englishEmpty)
        {
            bool isValid = true;
            string errorMessage = "";

            isValid &= int.TryParse(txtCorrectMath.Text, out mathCorrect) || (errorMessage += "Enter a valid number for Math Correct.\n") == null;
            isValid &= int.TryParse(txtWrongMath.Text, out mathWrong) || (errorMessage += "Enter a valid number for Math Wrong.\n") == null;
            isValid &= int.TryParse(txtEmptyMath.Text, out mathEmpty) || (errorMessage += "Enter a valid number for Math Empty.\n") == null;

            isValid &= int.TryParse(txtCorrectScience.Text, out scienceCorrect) || (errorMessage += "Enter a valid number for Science Correct.\n") == null;
            isValid &= int.TryParse(txtWrongScience.Text, out scienceWrong) || (errorMessage += "Enter a valid number for Science Wrong.\n") == null;
            isValid &= int.TryParse(txtEmptyScience.Text, out scienceEmpty) || (errorMessage += "Enter a valid number for Science Empty.\n") == null;

            isValid &= int.TryParse(txtCorrectTurkish.Text, out turkishCorrect) || (errorMessage += "Enter a valid number for Turkish Correct.\n") == null;
            isValid &= int.TryParse(txtWrongTurkish.Text, out turkishWrong) || (errorMessage += "Enter a valid number for Turkish Wrong.\n") == null;
            isValid &= int.TryParse(txtEmptyTurkish.Text, out turkishEmpty) || (errorMessage += "Enter a valid number for Turkish Empty.\n") == null;

            isValid &= int.TryParse(txtCorrectHistory.Text, out historyCorrect) || (errorMessage += "Enter a valid number for History Correct.\n") == null;
            isValid &= int.TryParse(txtWrongHistory.Text, out historyWrong) || (errorMessage += "Enter a valid number for History Wrong.\n") == null;
            isValid &= int.TryParse(txtEmptyHistory.Text, out historyEmpty) || (errorMessage += "Enter a valid number for History Empty.\n") == null;

            isValid &= int.TryParse(txtCorrectReligion.Text, out religionCorrect) || (errorMessage += "Enter a valid number for Religion Correct.\n") == null;
            isValid &= int.TryParse(txtWrongReligion.Text, out religionWrong) || (errorMessage += "Enter a valid number for Religion Wrong.\n") == null;
            isValid &= int.TryParse(txtEmptyReligion.Text, out religionEmpty) || (errorMessage += "Enter a valid number for Religion Empty.\n") == null;

            isValid &= int.TryParse(txtCorrectEnglish.Text, out englishCorrect) || (errorMessage += "Enter a valid number for English Correct.\n") == null;
            isValid &= int.TryParse(txtWrongEnglish.Text, out englishWrong) || (errorMessage += "Enter a valid number for English Wrong.\n") == null;
            isValid &= int.TryParse(txtEmptyEnglish.Text, out englishEmpty) || (errorMessage += "Enter a valid number for English Empty.\n") == null;

            if (!isValid)
            {
                MessageBox.Show(errorMessage, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return isValid;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Admin ise öğrenci seçilmeli, öğrenci ise studentId zaten belli
            int selectedStudentId;

            if (studentId.HasValue)
            {
                selectedStudentId = studentId.Value;
            }
            else
            {
                if (cmbStudent.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a student.");
                    return;
                }
                selectedStudentId = Convert.ToInt32(cmbStudent.SelectedValue);
            }

            // 2. Boş bırakılan alanlar varsa uyarı ver
            TextBox[] allTextBoxes = {
        txtCorrectMath, txtWrongMath, txtEmptyMath,
        txtCorrectScience, txtWrongScience, txtEmptyScience,
        txtCorrectTurkish, txtWrongTurkish, txtEmptyTurkish,
        txtCorrectHistory, txtWrongHistory, txtEmptyHistory,
        txtCorrectReligion, txtWrongReligion, txtEmptyReligion,
        txtCorrectEnglish, txtWrongEnglish, txtEmptyEnglish
    };

            foreach (TextBox tb in allTextBoxes)
            {
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    MessageBox.Show("Please fill in all the fields before saving.");
                    tb.Focus();
                    return;
                }
            }

            // TryParseInputs ile sayısal değerleri al
            if (!TryParseInputs(out int mathCorrect, out int mathWrong, out int mathEmpty,
                                out int scienceCorrect, out int scienceWrong, out int scienceEmpty,
                                out int turkishCorrect, out int turkishWrong, out int turkishEmpty,
                                out int historyCorrect, out int historyWrong, out int historyEmpty,
                                out int religionCorrect, out int religionWrong, out int religionEmpty,
                                out int englishCorrect, out int englishWrong, out int englishEmpty))
            {
                return;
            }

            Dictionary<string, int> maxSoruSayilari = new Dictionary<string, int>
    {
        {"Math", 20},
        {"Science", 20},
        {"Turkish", 20},
        {"History", 10},
        {"Religion", 10},
        {"English", 10}
    };

            // Derslerin toplam soru sayısı ve doğrulama işlemi
            if (!KontrolEt("Math", mathCorrect, mathWrong, mathEmpty, maxSoruSayilari["Math"]) ||
                !KontrolEt("Science", scienceCorrect, scienceWrong, scienceEmpty, maxSoruSayilari["Science"]) ||
                !KontrolEt("Turkish", turkishCorrect, turkishWrong, turkishEmpty, maxSoruSayilari["Turkish"]) ||
                !KontrolEt("History", historyCorrect, historyWrong, historyEmpty, maxSoruSayilari["History"]) ||
                !KontrolEt("Religion", religionCorrect, religionWrong, religionEmpty, maxSoruSayilari["Religion"]) ||
                !KontrolEt("English", englishCorrect, englishWrong, englishEmpty, maxSoruSayilari["English"]))
            {
                return; // hatalı giriş varsa kaydetme
            }

            // SQL bağlantısı ve kayıt işlemleri aynı kalabilir...
            string connectionString = ConfigurationManager.ConnectionStrings["SchoolDBConnection"].ConnectionString;
            string query = @"INSERT INTO TRIAL_EXAM 
    (STUDENT_ID, MATH_CORRECT, MATH_WRONG, MATH_EMPTY, 
     SCIENCE_CORRECT, SCIENCE_WRONG, SCIENCE_EMPTY,
     TURKISH_CORRECT, TURKISH_WRONG, TURKISH_EMPTY,
     HISTORY_CORRECT, HISTORY_WRONG, HISTORY_EMPTY,
     RELIGION_CORRECT, RELIGION_WRONG, RELIGION_EMPTY,
     ENGLISH_CORRECT, ENGLISH_WRONG, ENGLISH_EMPTY)
    VALUES
    (@StudentId, @MathCorrect, @MathWrong, @MathEmpty, 
     @ScienceCorrect, @ScienceWrong, @ScienceEmpty,
     @TurkishCorrect, @TurkishWrong, @TurkishEmpty,
     @HistoryCorrect, @HistoryWrong, @HistoryEmpty,
     @ReligionCorrect, @ReligionWrong, @ReligionEmpty,
     @EnglishCorrect, @EnglishWrong, @EnglishEmpty)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@StudentId", selectedStudentId);
                cmd.Parameters.AddWithValue("@MathCorrect", mathCorrect);
                cmd.Parameters.AddWithValue("@MathWrong", mathWrong);
                cmd.Parameters.AddWithValue("@MathEmpty", mathEmpty);

                cmd.Parameters.AddWithValue("@ScienceCorrect", scienceCorrect);
                cmd.Parameters.AddWithValue("@ScienceWrong", scienceWrong);
                cmd.Parameters.AddWithValue("@ScienceEmpty", scienceEmpty);

                cmd.Parameters.AddWithValue("@TurkishCorrect", turkishCorrect);
                cmd.Parameters.AddWithValue("@TurkishWrong", turkishWrong);
                cmd.Parameters.AddWithValue("@TurkishEmpty", turkishEmpty);

                cmd.Parameters.AddWithValue("@HistoryCorrect", historyCorrect);
                cmd.Parameters.AddWithValue("@HistoryWrong", historyWrong);
                cmd.Parameters.AddWithValue("@HistoryEmpty", historyEmpty);

                cmd.Parameters.AddWithValue("@ReligionCorrect", religionCorrect);
                cmd.Parameters.AddWithValue("@ReligionWrong", religionWrong);
                cmd.Parameters.AddWithValue("@ReligionEmpty", religionEmpty);

                cmd.Parameters.AddWithValue("@EnglishCorrect", englishCorrect);
                cmd.Parameters.AddWithValue("@EnglishWrong", englishWrong);
                cmd.Parameters.AddWithValue("@EnglishEmpty", englishEmpty);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Trial exam results saved successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving data: " + ex.Message);
                }
            }

            
        }
    }
}
