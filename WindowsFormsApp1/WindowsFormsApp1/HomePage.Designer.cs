namespace WindowsFormsApp1
{
    partial class HomePage
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
            this.lblUserID = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnStudentList = new System.Windows.Forms.Button();
            this.btnAddStudent = new System.Windows.Forms.Button();
            this.btnAddCourse = new System.Windows.Forms.Button();
            this.btnAddTrialExam = new System.Windows.Forms.Button();
            this.btnTrialHistory = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblUserID.Location = new System.Drawing.Point(86, 56);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(26, 16);
            this.lblUserID.TabIndex = 0;
            this.lblUserID.Text = "ID:";
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblWelcome.Location = new System.Drawing.Point(25, 19);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(87, 20);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = "Welcome!";
            // 
            // btnStudentList
            // 
            this.btnStudentList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnStudentList.Location = new System.Drawing.Point(316, 160);
            this.btnStudentList.Name = "btnStudentList";
            this.btnStudentList.Size = new System.Drawing.Size(183, 60);
            this.btnStudentList.TabIndex = 2;
            this.btnStudentList.Text = "Student List";
            this.btnStudentList.UseVisualStyleBackColor = true;
            this.btnStudentList.Click += new System.EventHandler(this.btnStudentList_Click);
            // 
            // btnAddStudent
            // 
            this.btnAddStudent.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnAddStudent.Location = new System.Drawing.Point(89, 160);
            this.btnAddStudent.Name = "btnAddStudent";
            this.btnAddStudent.Size = new System.Drawing.Size(183, 60);
            this.btnAddStudent.TabIndex = 3;
            this.btnAddStudent.Text = "Add Student / Admin";
            this.btnAddStudent.UseVisualStyleBackColor = true;
            this.btnAddStudent.Click += new System.EventHandler(this.btnAddStudent_Click);
            // 
            // btnAddCourse
            // 
            this.btnAddCourse.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnAddCourse.Location = new System.Drawing.Point(605, 19);
            this.btnAddCourse.Name = "btnAddCourse";
            this.btnAddCourse.Size = new System.Drawing.Size(183, 60);
            this.btnAddCourse.TabIndex = 4;
            this.btnAddCourse.Text = "Add Course";
            this.btnAddCourse.UseVisualStyleBackColor = true;
            this.btnAddCourse.Click += new System.EventHandler(this.btnAddCourse_Click);
            // 
            // btnAddTrialExam
            // 
            this.btnAddTrialExam.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnAddTrialExam.Location = new System.Drawing.Point(553, 160);
            this.btnAddTrialExam.Name = "btnAddTrialExam";
            this.btnAddTrialExam.Size = new System.Drawing.Size(183, 60);
            this.btnAddTrialExam.TabIndex = 5;
            this.btnAddTrialExam.Text = "Add Trial Exam";
            this.btnAddTrialExam.UseVisualStyleBackColor = true;
            this.btnAddTrialExam.Click += new System.EventHandler(this.btnAddTrialExam_Click);
            // 
            // btnTrialHistory
            // 
            this.btnTrialHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnTrialHistory.Location = new System.Drawing.Point(204, 248);
            this.btnTrialHistory.Name = "btnTrialHistory";
            this.btnTrialHistory.Size = new System.Drawing.Size(183, 60);
            this.btnTrialHistory.TabIndex = 6;
            this.btnTrialHistory.Text = "Trial Results";
            this.btnTrialHistory.UseVisualStyleBackColor = true;
            this.btnTrialHistory.Click += new System.EventHandler(this.btnTrialHistory_Click);
            // 
            // btnReports
            // 
            this.btnReports.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnReports.Location = new System.Drawing.Point(434, 248);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(183, 60);
            this.btnReports.TabIndex = 7;
            this.btnReports.Text = "Report / Graph";
            this.btnReports.UseVisualStyleBackColor = true;
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);
            // 
            // HomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 380);
            this.Controls.Add(this.btnReports);
            this.Controls.Add(this.btnTrialHistory);
            this.Controls.Add(this.btnAddTrialExam);
            this.Controls.Add(this.btnAddCourse);
            this.Controls.Add(this.btnAddStudent);
            this.Controls.Add(this.btnStudentList);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.lblUserID);
            this.Name = "HomePage";
            this.Text = "Home Page";
            this.Load += new System.EventHandler(this.HomePage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Button btnStudentList;
        private System.Windows.Forms.Button btnAddStudent;
        private System.Windows.Forms.Button btnAddCourse;
        private System.Windows.Forms.Button btnAddTrialExam;
        private System.Windows.Forms.Button btnTrialHistory;
        private System.Windows.Forms.Button btnReports;
    }
}