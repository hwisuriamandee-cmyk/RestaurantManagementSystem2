using System;
using System.Windows.Forms;
using RestaurantManagementSystem2.Models;
using RestaurantManagementSystem2.Repositories;

namespace RestaurantManagementSystem2
{
    public partial class EmployeeForm : Form
    {
        private readonly EmployeeRepository employeeRepository = new EmployeeRepository();
        private int selectedEmployeeId = 0;

        public EmployeeForm()
        {
            InitializeComponent();
            this.Load += EmployeeForm_Load;
            dgvEmployees.SelectionChanged += DgvEmployees_SelectionChanged;
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            try
            {
                dgvEmployees.DataSource = employeeRepository.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading employees: " + ex.Message);
            }
        }

        private void DgvEmployees_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEmployees.CurrentRow == null) return;

            Employee selected = dgvEmployees.CurrentRow.DataBoundItem as Employee;
            if (selected == null) return;

            selectedEmployeeId = selected.Id;
            txtFullName.Text = selected.FullName;
            txtPosition.Text = selected.Position;
            txtSalary.Text = selected.Salary.ToString();
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string position = txtPosition.Text.Trim();
            string salaryText = txtSalary.Text.Trim();

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(position) || string.IsNullOrEmpty(salaryText))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (!decimal.TryParse(salaryText, out decimal salary))
            {
                MessageBox.Show("Salary must be a valid number.");
                return;
            }

            try
            {
                Employee newEmployee = new Employee
                {
                    FullName = fullName,
                    Position = position,
                    Salary = salary
                };

                employeeRepository.Add(newEmployee);

                MessageBox.Show("Employee added successfully.");
                ClearFields();
                LoadEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding employee: " + ex.Message);
            }
        }

        private void btnUpdateEmployee_Click(object sender, EventArgs e)
        {
            if (selectedEmployeeId == 0)
            {
                MessageBox.Show("Please select an employee from the list first.");
                return;
            }

            string fullName = txtFullName.Text.Trim();
            string position = txtPosition.Text.Trim();
            string salaryText = txtSalary.Text.Trim();

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(position) || string.IsNullOrEmpty(salaryText))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (!decimal.TryParse(salaryText, out decimal salary))
            {
                MessageBox.Show("Salary must be a valid number.");
                return;
            }

            try
            {
                Employee updated = new Employee
                {
                    Id = selectedEmployeeId,
                    FullName = fullName,
                    Position = position,
                    Salary = salary
                };

                employeeRepository.Update(updated);

                MessageBox.Show("Employee updated successfully.");
                ClearFields();
                LoadEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating employee: " + ex.Message);
            }
        }

        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.CurrentRow == null)
            {
                MessageBox.Show("Please select a row to delete.");
                return;
            }

            Employee selected = (Employee)dgvEmployees.CurrentRow.DataBoundItem;

            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to delete " + selected.FullName + " ?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                employeeRepository.Delete(selected.Id);
                MessageBox.Show("Employee deleted.");
                ClearFields();
                LoadEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting employee: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            txtFullName.Clear();
            txtPosition.Clear();
            txtSalary.Clear();
            selectedEmployeeId = 0;
        }
    }
}