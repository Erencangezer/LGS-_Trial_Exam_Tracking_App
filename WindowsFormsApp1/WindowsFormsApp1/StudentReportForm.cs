using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp1
{
    public partial class StudentReportForm : Form
    {
        private int studentId;
        private string studentFullName;

        public StudentReportForm(int id, string fullName)
        {
            InitializeComponent();
            studentId = id;
            studentFullName = fullName;
            lblStudentName.Text = $"Student: {studentFullName}";

            // Başlangıçta grafik ve tablo boş olabilir
            dgvTrialResults.DataSource = null;
            chartTrialResults.Series.Clear();
        }

        private void StudentReportForm_Load(object sender, EventArgs e)
        {
            // Şimdilik boş bırakılabilir
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            LoadTrialResults();
        }

        private void LoadTrialResults()
        {
            string connStr = ConfigurationManager.ConnectionStrings["SchoolDBConnection"].ConnectionString;
            string query = @"
                SELECT 
                    MATH_CORRECT, MATH_WRONG,
                    SCIENCE_CORRECT, SCIENCE_WRONG,
                    TURKISH_CORRECT, TURKISH_WRONG,
                    HISTORY_CORRECT, HISTORY_WRONG,
                    RELIGION_CORRECT, RELIGION_WRONG,
                    ENGLISH_CORRECT, ENGLISH_WRONG
                FROM TRIAL_EXAM 
                WHERE STUDENT_ID = @id
                ORDER BY ID";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", studentId);
                conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                dgvTrialResults.DataSource = dt;

                DrawChart(dt);
            }
        }

        private void DrawChart(DataTable dt)
        {
            chartTrialResults.Series.Clear();

            string[] subjects = { "Matematik", "Fen", "Türkçe", "İnkılap", "Din", "İngilizce" };
            string[] dbCorrectCols = { "MATH_CORRECT", "SCIENCE_CORRECT", "TURKISH_CORRECT", "HISTORY_CORRECT", "RELIGION_CORRECT", "ENGLISH_CORRECT" };

            chartTrialResults.ChartAreas[0].AxisX.Title = "Deneme Sırası";
            chartTrialResults.ChartAreas[0].AxisY.Title = "Doğru Sayısı";
            chartTrialResults.ChartAreas[0].AxisX.Interval = 1;
            chartTrialResults.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chartTrialResults.Legends[0].Docking = Docking.Top;
            chartTrialResults.Palette = ChartColorPalette.BrightPastel;

            for (int i = 0; i < subjects.Length; i++)
            {
                var series = new Series(subjects[i])
                {
                    ChartType = SeriesChartType.Column,
                    IsValueShownAsLabel = true
                };
                chartTrialResults.Series.Add(series);
            }

            int trialNumber = 1;
            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < subjects.Length; i++)
                {
                    int correctCount = Convert.ToInt32(row[dbCorrectCols[i]]);
                    chartTrialResults.Series[subjects[i]].Points.AddXY("Deneme " + trialNumber, correctCount);
                }
                trialNumber++;
            }
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            if (dgvTrialResults.Rows.Count == 0)
            {
                MessageBox.Show("PDF oluşturmak için önce verileri yükleyin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF Dosyası|*.pdf";
            sfd.FileName = $"{studentFullName}_DenemeRaporu.pdf";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                GeneratePdf(sfd.FileName);
            }
        }

        private void GeneratePdf(string filePath)
        {
            Document pdfDoc = new Document(PageSize.A4, 25, 25, 30, 30);
            PdfWriter.GetInstance(pdfDoc, new FileStream(filePath, FileMode.Create));
            pdfDoc.Open();

            var fontTitle = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16f);
            var fontHeader = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12f);
            var fontCell = FontFactory.GetFont(FontFactory.HELVETICA, 11f);

            // Başlık
            Paragraph title = new Paragraph($"Deneme Raporu - {studentFullName}", fontTitle);
            title.Alignment = Element.ALIGN_CENTER;
            title.SpacingAfter = 20f;
            pdfDoc.Add(title);

            // Grafik
            using (MemoryStream ms = new MemoryStream())
            {
                chartTrialResults.SaveImage(ms, ChartImageFormat.Png);
                iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(ms.ToArray());
                chartImage.Alignment = Element.ALIGN_CENTER;
                chartImage.ScaleToFit(500f, 300f);
                chartImage.SpacingAfter = 20f;
                pdfDoc.Add(chartImage);
            }

            // Tablo (ders bazlı, her ders altında Doğru / Yanlış)
            int columnCount = 6 * 2; // 6 ders, her ders için Doğru ve Yanlış sütunu

            PdfPTable table = new PdfPTable(columnCount);
            table.WidthPercentage = 100;
            table.SpacingBefore = 20f;
            table.DefaultCell.Padding = 5f;
            table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;

            // Üst başlık: Dersler (6 tane, 2 sütunu kapsıyor)
            string[] subjects = { "Matematik", "Fen", "Türkçe", "Inkilap", "Din", "Ingilizce" };
            foreach (var subject in subjects)
            {
                PdfPCell cell = new PdfPCell(new Phrase(subject, fontHeader));
                cell.Colspan = 2;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.BackgroundColor = new BaseColor(230, 230, 250);
                table.AddCell(cell);
            }

            // Alt başlık: Doğru, Yanlış
            for (int i = 0; i < subjects.Length; i++)
            {
                PdfPCell correctCell = new PdfPCell(new Phrase("D", fontHeader));
                correctCell.HorizontalAlignment = Element.ALIGN_CENTER;
                correctCell.BackgroundColor = new BaseColor(230, 230, 250);
                table.AddCell(correctCell);

                PdfPCell wrongCell = new PdfPCell(new Phrase("Y", fontHeader));
                wrongCell.HorizontalAlignment = Element.ALIGN_CENTER;
                wrongCell.BackgroundColor = new BaseColor(230, 230, 250);
                table.AddCell(wrongCell);
            }

            // Satır verileri
            foreach (DataGridViewRow row in dgvTrialResults.Rows)
            {
                if (row.IsNewRow) continue;

                // Her ders için doğru ve yanlış verileri sırayla ekle
                string[] colNames = {
                    "MATH_CORRECT", "MATH_WRONG",
                    "SCIENCE_CORRECT", "SCIENCE_WRONG",
                    "TURKISH_CORRECT", "TURKISH_WRONG",
                    "HISTORY_CORRECT", "HISTORY_WRONG",
                    "RELIGION_CORRECT", "RELIGION_WRONG",
                    "ENGLISH_CORRECT", "ENGLISH_WRONG"
                };

                foreach (var colName in colNames)
                {
                    var val = row.Cells[colName].Value?.ToString() ?? "0";
                    PdfPCell dataCell = new PdfPCell(new Phrase(val, fontCell));
                    dataCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(dataCell);
                }
            }

            pdfDoc.Add(table);
            pdfDoc.Close();

            MessageBox.Show("PDF başarıyla oluşturuldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
