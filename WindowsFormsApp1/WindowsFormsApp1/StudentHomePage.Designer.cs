namespace WindowsFormsApp1
{
    partial class StudentHomePage
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
            this.lblWelcomeStudent = new System.Windows.Forms.Label();
            this.btnAddTrialExam = new System.Windows.Forms.Button();
            this.btnViewReport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblWelcomeStudent
            // 
            this.lblWelcomeStudent.AutoSize = true;
            this.lblWelcomeStudent.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblWelcomeStudent.Location = new System.Drawing.Point(9, 7);
            this.lblWelcomeStudent.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWelcomeStudent.Name = "lblWelcomeStudent";
            this.lblWelcomeStudent.Size = new System.Drawing.Size(98, 21);
            this.lblWelcomeStudent.TabIndex = 0;
            this.lblWelcomeStudent.Text = "Welcome ...";
            // 
            // btnAddTrialExam
            // 
            this.btnAddTrialExam.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnAddTrialExam.Location = new System.Drawing.Point(39, 114);
            this.btnAddTrialExam.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddTrialExam.Name = "btnAddTrialExam";
            this.btnAddTrialExam.Size = new System.Drawing.Size(183, 60);
            this.btnAddTrialExam.TabIndex = 1;
            this.btnAddTrialExam.Text = "Add Trial Exam";
            this.btnAddTrialExam.UseVisualStyleBackColor = true;
            this.btnAddTrialExam.Click += new System.EventHandler(this.btnAddTrialExam_Click);
            // 
            // btnViewReport
            // 
            this.btnViewReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnViewReport.Location = new System.Drawing.Point(272, 114);
            this.btnViewReport.Name = "btnViewReport";
            this.btnViewReport.Size = new System.Drawing.Size(183, 60);
            this.btnViewReport.TabIndex = 2;
            this.btnViewReport.Text = "Trial Exam Results";
            this.btnViewReport.UseVisualStyleBackColor = true;
            this.btnViewReport.Click += new System.EventHandler(this.btnViewReport_Click);
            // 
            // StudentHomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 224);
            this.Controls.Add(this.btnViewReport);
            this.Controls.Add(this.btnAddTrialExam);
            this.Controls.Add(this.lblWelcomeStudent);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "StudentHomePage";
            this.Text = "Home Page";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWelcomeStudent;
        private System.Windows.Forms.Button btnAddTrialExam;
        private System.Windows.Forms.Button btnViewReport;
    }
}