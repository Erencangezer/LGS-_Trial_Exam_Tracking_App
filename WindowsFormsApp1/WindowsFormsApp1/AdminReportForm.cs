using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing.Imaging;


namespace WindowsFormsApp1
{
    public partial class AdminReportForm: Form
    {
        public AdminReportForm()
        {
            InitializeComponent();
        }

        private string selectedStudentFullName = "";

        private void AdminReportForm_Load(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["SchoolDBConnection"].ConnectionString;
            string query = "SELECT ID, NAME + ' ' + SURNAME AS FullName FROM STUDENT";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);

                cmbStudent.DisplayMember = "FullName";
                cmbStudent.ValueMember = "ID";
                cmbStudent.DataSource = dt;

                cmbStudent.SelectedIndexChanged += (s, ev) =>
                {
                    if (cmbStudent.SelectedValue != null)
                    {
                        // Seçilen öğrencinin tam adını al (DisplayMember zaten FullName)
                        selectedStudentFullName = cmbStudent.Text;
                    }
                };
            }
        }

        private void btnLoadChart_Click(object sender, EventArgs e)
        {
            int studentId = Convert.ToInt32(cmbStudent.SelectedValue);
            string query = @"
SELECT 
    MATH_CORRECT, MATH_WRONG,
    SCIENCE_CORRECT, SCIENCE_WRONG,
    TURKISH_CORRECT, TURKISH_WRONG,
    HISTORY_CORRECT, HISTORY_WRONG,
    RELIGION_CORRECT, RELIGION_WRONG,
    ENGLISH_CORRECT, ENGLISH_WRONG
FROM TRIAL_EXAM WHERE STUDENT_ID = @id";

            string connStr = ConfigurationManager.ConnectionStrings["SchoolDBConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", studentId);
                conn.Open();

                DataTable dt = new DataTable();
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);

                // DataGridView'i doldur
                dgvResults.DataSource = dt;

                // Grafik ayarları (doğru sayılar için)
                chartResults.Series.Clear();
                chartResults.ChartAreas[0].AxisX.Title = "Deneme Sırası";
                chartResults.ChartAreas[0].AxisY.Title = "Doğru Sayısı";
                chartResults.ChartAreas[0].AxisX.Interval = 1;
                chartResults.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
                chartResults.Legends[0].Docking = Docking.Top;
                chartResults.Palette = ChartColorPalette.BrightPastel;

                string[] dersler = { "Matematik", "Fen", "Türkçe", "İnkılap", "Din", "İngilizce" };
                foreach (string ders in dersler)
                {
                    Series s = new Series(ders)
                    {
                        ChartType = SeriesChartType.Column,
                        IsValueShownAsLabel = true
                    };
                    chartResults.Series.Add(s);
                }

                int denemeNo = 1;
                foreach (DataRow row in dt.Rows)
                {
                    chartResults.Series["Matematik"].Points.AddXY("Deneme " + denemeNo, Convert.ToInt32(row["MATH_CORRECT"]));
                    chartResults.Series["Fen"].Points.AddXY("Deneme " + denemeNo, Convert.ToInt32(row["SCIENCE_CORRECT"]));
                    chartResults.Series["Türkçe"].Points.AddXY("Deneme " + denemeNo, Convert.ToInt32(row["TURKISH_CORRECT"]));
                    chartResults.Series["İnkılap"].Points.AddXY("Deneme " + denemeNo, Convert.ToInt32(row["HISTORY_CORRECT"]));
                    chartResults.Series["Din"].Points.AddXY("Deneme " + denemeNo, Convert.ToInt32(row["RELIGION_CORRECT"]));
                    chartResults.Series["İngilizce"].Points.AddXY("Deneme " + denemeNo, Convert.ToInt32(row["ENGLISH_CORRECT"]));
                    denemeNo++;
                }
            }
        }




        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            if (dgvResults.Rows.Count == 0)
            {
                MessageBox.Show("Rapor oluşturmak için önce verileri yükleyin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF dosyası|*.pdf";
            saveFileDialog.Title = "Raporu Kaydet";
            saveFileDialog.FileName = $"{selectedStudentFullName}_DenemeRaporu.pdf";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(selectedStudentFullName) && cmbStudent.SelectedItem != null)
                {
                    selectedStudentFullName = cmbStudent.Text;
                }

                Document pdfDoc = new Document(PageSize.A4.Rotate(), 25, 25, 30, 30); // YATAY format
                PdfWriter.GetInstance(pdfDoc, new FileStream(saveFileDialog.FileName, FileMode.Create));
                pdfDoc.Open();

                var fontTitle = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16f);
                var fontHeader = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12f);
                var fontCell = FontFactory.GetFont(FontFactory.HELVETICA, 11f);

                Paragraph baslik = new Paragraph($"Deneme Raporu - {selectedStudentFullName}", fontTitle);
                baslik.Alignment = Element.ALIGN_CENTER;
                baslik.SpacingAfter = 20f;
                pdfDoc.Add(baslik);

                // Grafik
                using (MemoryStream chartImageStream = new MemoryStream())
                {
                    chartResults.SaveImage(chartImageStream, ChartImageFormat.Png);
                    iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(chartImageStream.ToArray());
                    chartImage.Alignment = Element.ALIGN_CENTER;
                    chartImage.ScaleToFit(750f, 400f);
                    chartImage.SpacingAfter = 20f;
                    pdfDoc.Add(chartImage);
                }

                // Yeni tablo: sütun başlıkları dersler olacak, altlarında doğru ve yanlış
                string[] dersler = { "Matematik", "Fen", "Türkçe", "Inkilap", "Din", "Ingilizce" };
                string[] alanlar = { "MATH", "SCIENCE", "TURKISH", "HISTORY", "RELIGION", "ENGLISH" };

                // Toplam kolon sayısı = her ders için 2 (doğru ve yanlış)
                PdfPTable table = new PdfPTable(dersler.Length * 2 + 1); // +1: Deneme başlığı
                table.WidthPercentage = 100;
                table.SpacingBefore = 20f;
                table.DefaultCell.Padding = 5f;
                table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;

                // 1. SATIR: Ana başlık (ders isimleri)
                PdfPCell emptyCell = new PdfPCell(new Phrase("")) { Colspan = 1, Border = 0 };
                table.AddCell(emptyCell);

                foreach (string ders in dersler)
                {
                    PdfPCell headerCell = new PdfPCell(new Phrase(ders, fontHeader));
                    headerCell.Colspan = 2;
                    headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    headerCell.BackgroundColor = new BaseColor(200, 200, 230);
                    table.AddCell(headerCell);
                }

                // 2. SATIR: Alt başlıklar (Doğru / Yanlış)
                table.AddCell(new PdfPCell(new Phrase("Deneme", fontHeader))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = new BaseColor(220, 220, 250)
                });

                for (int i = 0; i < dersler.Length; i++)
                {
                    PdfPCell dogruCell = new PdfPCell(new Phrase("D", fontHeader))
                    {
                        BackgroundColor = new BaseColor(230, 230, 250),
                        HorizontalAlignment = Element.ALIGN_CENTER
                    };
                    PdfPCell yanlisCell = new PdfPCell(new Phrase("Y", fontHeader))
                    {
                        BackgroundColor = new BaseColor(230, 230, 250),
                        HorizontalAlignment = Element.ALIGN_CENTER
                    };
                    table.AddCell(dogruCell);
                    table.AddCell(yanlisCell);
                }

                // VERİ SATIRLARI
                int denemeNo = 1;
                foreach (DataGridViewRow row in dgvResults.Rows)
                {
                    if (row.IsNewRow) continue;

                    table.AddCell(new PdfPCell(new Phrase($"Deneme {denemeNo++}", fontCell))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER
                    });

                    foreach (string alan in alanlar)
                    {
                        string dogru = row.Cells[$"{alan}_CORRECT"].Value?.ToString() ?? "0";
                        string yanlis = row.Cells[$"{alan}_WRONG"].Value?.ToString() ?? "0";

                        table.AddCell(new PdfPCell(new Phrase(dogru, fontCell)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        table.AddCell(new PdfPCell(new Phrase(yanlis, fontCell)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    }
                }

                pdfDoc.Add(table);



                pdfDoc.Close();

                MessageBox.Show("PDF başarıyla oluşturuldu.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


    }

}

