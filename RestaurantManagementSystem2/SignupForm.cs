using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RestaurantManagementSystem2
{
    public partial class Singup_form : Form
    {
        public Singup_form()
        {
            InitializeComponent();
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            string username = txtSignupUsername.Text.Trim();
            string password = txtSignupPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RestaurantDB;Integrated Security=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@Username", username);
                    int existingCount = (int)checkCmd.ExecuteScalar();

                    if (existingCount > 0)
                    {
                        MessageBox.Show("That username is already taken.");
                        return;
                    }

                    string insertQuery = "INSERT INTO Users (Username, Password, Role) VALUES (@Username, @Password, 'Staff')";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@Username", username);
                    insertCmd.Parameters.AddWithValue("@Password", RestaurantManagementSystem2.Helpers.PasswordHelper.Hash(password));
                    insertCmd.ExecuteNonQuery();

                    MessageBox.Show("Account created successfully! You can now log in.");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Singup_form_Load(object sender, EventArgs e)
        {

        }
    }
}