using System;
using System.Windows.Forms;
using RestaurantManagementSystem2.Models;
using RestaurantManagementSystem2.Repositories;

namespace RestaurantManagementSystem2
{
    public partial class CustomerForm : Form
    {
        private readonly CustomerRepository customerRepository = new CustomerRepository();
        private int selectedCustomerId = 0;

        public CustomerForm()
        {
            InitializeComponent();
            this.Load += CustomerForm_Load;
            dgvCustomers.SelectionChanged += DgvCustomers_SelectionChanged;
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            try
            {
                dgvCustomers.DataSource = customerRepository.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customers: " + ex.Message);
            }
        }

        private void DgvCustomers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow == null) return;

            Customer selected = dgvCustomers.CurrentRow.DataBoundItem as Customer;
            if (selected == null) return;

            selectedCustomerId = selected.Id;
            txtFullName.Text = selected.FullName;
            txtPhone.Text = selected.Phone;
            txtEmail.Text = selected.Email;
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Full name and phone are required.");
                return;
            }

            try
            {
                Customer newCustomer = new Customer
                {
                    FullName = fullName,
                    Phone = phone,
                    Email = string.IsNullOrEmpty(email) ? null : email
                };

                customerRepository.Add(newCustomer);

                MessageBox.Show("Customer added successfully.");
                ClearFields();
                LoadCustomers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding customer: " + ex.Message);
            }
        }

        private void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            if (selectedCustomerId == 0)
            {
                MessageBox.Show("Please select a customer from the list first.");
                return;
            }

            string fullName = txtFullName.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Full name and phone are required.");
                return;
            }

            try
            {
                Customer updated = new Customer
                {
                    Id = selectedCustomerId,
                    FullName = fullName,
                    Phone = phone,
                    Email = string.IsNullOrEmpty(email) ? null : email
                };

                customerRepository.Update(updated);

                MessageBox.Show("Customer updated successfully.");
                ClearFields();
                LoadCustomers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating customer: " + ex.Message);
            }
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow == null)
            {
                MessageBox.Show("Please select a row to delete.");
                return;
            }

            Customer selected = (Customer)dgvCustomers.CurrentRow.DataBoundItem;

            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to delete " + selected.FullName + "?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                customerRepository.Delete(selected.Id);
                MessageBox.Show("Customer deleted.");
                ClearFields();
                LoadCustomers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting customer: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            txtFullName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            selectedCustomerId = 0;
        }

        private void dgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}