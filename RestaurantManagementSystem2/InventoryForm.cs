using System;
using System.Windows.Forms;
using RestaurantManagementSystem2.Models;
using RestaurantManagementSystem2.Repositories;

namespace RestaurantManagementSystem2
{
    public partial class InventoryForm : Form
    {
        private readonly InventoryRepository inventoryRepository = new InventoryRepository();
        private int selectedItemId = 0;

        public InventoryForm()
        {
            InitializeComponent();
            this.Load += InventoryForm_Load;
            dgvInventory.SelectionChanged += DgvInventory_SelectionChanged;
        }

        private void InventoryForm_Load(object sender, EventArgs e)
        {
            LoadInventory();
        }

        private void LoadInventory()
        {
            try
            {
                dgvInventory.DataSource = inventoryRepository.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading inventory: " + ex.Message);
            }
        }

        private void DgvInventory_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvInventory.CurrentRow == null) return;

            InventoryItem selected = dgvInventory.CurrentRow.DataBoundItem as InventoryItem;
            if (selected == null) return;

            selectedItemId = selected.Id;
            txtItemName.Text = selected.ItemName;
            txtQuantity.Text = selected.Quantity.ToString();
            txtPrice.Text = selected.Price.ToString();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            string itemName = txtItemName.Text.Trim();
            string quantityText = txtQuantity.Text.Trim();
            string priceText = txtPrice.Text.Trim();

            if (string.IsNullOrEmpty(itemName) || string.IsNullOrEmpty(quantityText) || string.IsNullOrEmpty(priceText))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (!int.TryParse(quantityText, out int quantity))
            {
                MessageBox.Show("Quantity must be a whole number.");
                return;
            }

            if (!decimal.TryParse(priceText, out decimal price))
            {
                MessageBox.Show("Price must be a valid number.");
                return;
            }

            try
            {
                InventoryItem newItem = new InventoryItem
                {
                    ItemName = itemName,
                    Quantity = quantity,
                    Price = price
                };

                inventoryRepository.Add(newItem);

                MessageBox.Show("Item added successfully.");
                ClearFields();
                LoadInventory();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding item: " + ex.Message);
            }
        }

        private void btnUpdateItem_Click(object sender, EventArgs e)
        {
            if (selectedItemId == 0)
            {
                MessageBox.Show("Please select an item from the list first.");
                return;
            }

            string itemName = txtItemName.Text.Trim();
            string quantityText = txtQuantity.Text.Trim();
            string priceText = txtPrice.Text.Trim();

            if (string.IsNullOrEmpty(itemName) || string.IsNullOrEmpty(quantityText) || string.IsNullOrEmpty(priceText))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (!int.TryParse(quantityText, out int quantity))
            {
                MessageBox.Show("Quantity must be a whole number.");
                return;
            }

            if (!decimal.TryParse(priceText, out decimal price))
            {
                MessageBox.Show("Price must be a valid number.");
                return;
            }

            try
            {
                InventoryItem updated = new InventoryItem
                {
                    Id = selectedItemId,
                    ItemName = itemName,
                    Quantity = quantity,
                    Price = price
                };

                inventoryRepository.Update(updated);

                MessageBox.Show("Item updated successfully.");
                ClearFields();
                LoadInventory();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating item: " + ex.Message);
            }
        }


        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (dgvInventory.CurrentRow == null)
            {
                MessageBox.Show("Please select a row to delete.");
                return;
            }

            InventoryItem selected = (InventoryItem)dgvInventory.CurrentRow.DataBoundItem;

            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to delete " + selected.ItemName + "?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                inventoryRepository.Delete(selected.Id);
                MessageBox.Show("Item deleted.");
                ClearFields();
                LoadInventory();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting item: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            txtItemName.Clear();
            txtQuantity.Clear();
            txtPrice.Clear();
            selectedItemId = 0;
        }

        private void btnGoToCustomers_Click(object sender, EventArgs e)
        {
            CustomerForm customerForm = new CustomerForm();
            customerForm.Show();
        }

        private void btnGoToEmployees_Click(object sender, EventArgs e)
        {
            EmployeeForm employeeForm = new EmployeeForm();
            employeeForm.Show();
        }

        private void btnGoToOrders_Click(object sender, EventArgs e)
        {
            OrderForm orderForm = new OrderForm();
            orderForm.Show();
        }

        private void lblItemName_Click(object sender, EventArgs e)
        {

        }
    }
}