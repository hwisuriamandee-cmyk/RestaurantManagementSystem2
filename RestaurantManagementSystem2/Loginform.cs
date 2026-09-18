using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RestaurantManagementSystem2
{
    public partial class Loginform : Form
    {
        public Loginform()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
   
        {
            
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RestaurantDB;Integrated Security=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT Role FROM Users WHERE Username = @Username AND Password = @Password";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", RestaurantManagementSystem2.Helpers.PasswordHelper.Hash(password));

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        MessageBox.Show("Login successful! Role: " + result.ToString());
                        InventoryForm inventoryForm = new InventoryForm();
                        inventoryForm.Show();
                        this.Hide();

                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnGoToSignup_Click(object sender, EventArgs e)
        {
            Singup_form signupForm = new Singup_form();
            signupForm.Show();
        }

        private void Loginform_Load(object sender, EventArgs e)
        {

        }

        private void Password_Click(object sender, EventArgs e)
        {

        }
    }
}