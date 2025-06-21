namespace WindowsFormsApp1
{
    partial class StudentReportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lblStudentName = new System.Windows.Forms.Label();
            this.dgvTrialResults = new System.Windows.Forms.DataGridView();
            this.chartTrialResults = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnExportPdf = new System.Windows.Forms.Button();
            this.btnLoadData = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrialResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTrialResults)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStudentName
            // 
            this.lblStudentName.AutoSize = true;
            this.lblStudentName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblStudentName.Location = new System.Drawing.Point(38, 43);
            this.lblStudentName.Name = "lblStudentName";
            this.lblStudentName.Size = new System.Drawing.Size(57, 21);
            this.lblStudentName.TabIndex = 0;
            this.lblStudentName.Text = "label1";
            // 
            // dgvTrialResults
            // 
            this.dgvTrialResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTrialResults.Location = new System.Drawing.Point(15, 113);
            this.dgvTrialResults.Name = "dgvTrialResults";
            this.dgvTrialResults.Size = new System.Drawing.Size(1086, 206);
            this.dgvTrialResults.TabIndex = 1;
            // 
            // chartTrialResults
            // 
            chartArea1.Name = "ChartArea1";
            this.chartTrialResults.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartTrialResults.Legends.Add(legend1);
            this.chartTrialResults.Location = new System.Drawing.Point(15, 325);
            this.chartTrialResults.Name = "chartTrialResults";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartTrialResults.Series.Add(series1);
            this.chartTrialResults.Size = new System.Drawing.Size(1086, 301);
            this.chartTrialResults.TabIndex = 2;
            this.chartTrialResults.Text = "chart1";
            // 
            // btnExportPdf
            // 
            this.btnExportPdf.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnExportPdf.Location = new System.Drawing.Point(956, 28);
            this.btnExportPdf.Name = "btnExportPdf";
            this.btnExportPdf.Size = new System.Drawing.Size(129, 51);
            this.btnExportPdf.TabIndex = 3;
            this.btnExportPdf.Text = "Save PDF";
            this.btnExportPdf.UseVisualStyleBackColor = true;
            this.btnExportPdf.Click += new System.EventHandler(this.btnExportPdf_Click);
            // 
            // btnLoadData
            // 
            this.btnLoadData.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnLoadData.Location = new System.Drawing.Point(491, 28);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(147, 51);
            this.btnLoadData.TabIndex = 4;
            this.btnLoadData.Text = "Show Results";
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // StudentReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 638);
            this.Controls.Add(this.btnLoadData);
            this.Controls.Add(this.btnExportPdf);
            this.Controls.Add(this.chartTrialResults);
            this.Controls.Add(this.dgvTrialResults);
            this.Controls.Add(this.lblStudentName);
            this.Name = "StudentReportForm";
            this.Text = "Deneme Sınavı Sonuçları";
            this.Load += new System.EventHandler(this.StudentReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrialResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTrialResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStudentName;
        private System.Windows.Forms.DataGridView dgvTrialResults;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTrialResults;
        private System.Windows.Forms.Button btnExportPdf;
        private System.Windows.Forms.Button btnLoadData;
    }
}