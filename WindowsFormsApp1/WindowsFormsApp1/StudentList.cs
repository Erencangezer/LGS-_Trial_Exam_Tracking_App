using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public partial class StudentList : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["SchoolDBConnection"].ConnectionString;

        public StudentList()
        {
            InitializeComponent();
        }

        

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please write student name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            getStudents(txtName.Text.Trim());
        }

        private void rbSearchByName_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSearchByName.Checked)
            {
                txtName.Visible = true;
                btnSearch.Visible = true;
                label1.Visible = true;
                dgvStudent.DataSource = null;
            }
        }

        private void rbShowAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rbShowAll.Checked)
            {
                txtName.Visible = false;
                btnSearch.Visible = false;
                label1.Visible = false;
                getStudents();
            }
        }

        private void getStudents(string name = null)
        {
            List<Student> listofStudents = new List<Student>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string querySelect = "SELECT * FROM [SchoolDB].[dbo].[STUDENT]";
                    if (!string.IsNullOrEmpty(name))
                    {
                        querySelect += " WHERE NAME = @Name";
                    }

                    using (SqlCommand cmd = new SqlCommand(querySelect, conn))
                    {
                        if (!string.IsNullOrEmpty(name))
                        {
                            cmd.Parameters.AddWithValue("@Name", name);
                        }

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Student std = new Student
                                {
                                    ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                    Name = reader["NAME"]?.ToString(),
                                    Surname = reader["SURNAME"]?.ToString(),
                                    Email = reader["EMAIL"]?.ToString(),
                                    PhoneNumber = reader["PHONE_NUMBER"]?.ToString(),
                                    City = reader["CITY"]?.ToString(),
                                    Address = reader["ADDRESS"]?.ToString(),
                                    PostalCode = reader["POSTAL_CODE"] != DBNull.Value ? Convert.ToInt32(reader["POSTAL_CODE"]) : 0,
                                    Gender = reader["GENDER"]?.ToString()
                                };

                                listofStudents.Add(std);
                            }
                        }
                    }

                    dgvStudent.DataSource = null;
                    dgvStudent.DataSource = listofStudents;

                    if (listofStudents.Count == 0)
                    {
                        MessageBox.Show(name == null ? "Hiç öğrenci bulunamadı." : "No students found.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvStudent.Rows.Count > 0)
            {
                DataGridViewRow row = dgvStudent.Rows[e.RowIndex];

                txtName.Text = row.Cells["Name"].Value?.ToString();
                // Diğer TextBox'lar eklediyseniz buraya doldurma işlemi ekleyebilirsiniz.
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvStudent.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a student to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvStudent.SelectedRows[0];
            Student selectedStudent = new Student
            {
                ID = Convert.ToInt32(row.Cells["ID"].Value),
                Name = row.Cells["Name"].Value?.ToString(),
                Surname = row.Cells["Surname"].Value?.ToString(),
                Email = row.Cells["Email"].Value?.ToString(),
                PhoneNumber = row.Cells["PhoneNumber"].Value?.ToString(),
                City = row.Cells["City"].Value?.ToString(),
                Address = row.Cells["Address"].Value?.ToString(),
                PostalCode = row.Cells["PostalCode"].Value != null ? Convert.ToInt32(row.Cells["PostalCode"].Value) : 0,
                Gender = row.Cells["Gender"].Value?.ToString()
            };

            UpdateStudentForm form = new UpdateStudentForm(selectedStudent);
            if (form.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Student information updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                getStudents();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvStudent.SelectedRows.Count > 0)
            {
                int selectedID = Convert.ToInt32(dgvStudent.SelectedRows[0].Cells["ID"].Value);

                var confirmResult = MessageBox.Show("Are you sure to delete this student?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();
                            string queryDelete = "DELETE FROM [SchoolDB].[dbo].[STUDENT] WHERE ID = @ID";

                            using (SqlCommand cmd = new SqlCommand(queryDelete, conn))
                            {
                                cmd.Parameters.AddWithValue("@ID", selectedID);

                                int affectedRows = cmd.ExecuteNonQuery();
                                if (affectedRows > 0)
                                {
                                    MessageBox.Show("Student deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    getStudents();
                                }
                                else
                                {
                                    MessageBox.Show("Delete operation failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a student to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
