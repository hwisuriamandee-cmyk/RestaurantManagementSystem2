using System;
using System.Windows.Forms;
using RestaurantManagementSystem2.Models;
using RestaurantManagementSystem2.Repositories;

namespace RestaurantManagementSystem2
{
    public partial class OrderForm : Form
    {
        private readonly OrderRepository orderRepository = new OrderRepository();

        public OrderForm()
        {
            InitializeComponent();
            this.Load += OrderForm_Load;
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            LoadDropdowns();
            LoadOrders();
        }

        private void LoadDropdowns()
        {
            try
            {
                cmbCustomer.DataSource = orderRepository.GetAllCustomers();
                cmbCustomer.DisplayMember = "FullName";
                cmbCustomer.ValueMember = "Id";

                cmbItem.DataSource = orderRepository.GetAllItems();
                cmbItem.DisplayMember = "ItemName";
                cmbItem.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading dropdowns: " + ex.Message);
            }
        }

        private void LoadOrders()
        {
            try
            {
                dgvOrders.DataSource = orderRepository.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading orders: " + ex.Message);
            }
        }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            if (cmbCustomer.SelectedValue == null || cmbItem.SelectedValue == null)
            {
                MessageBox.Show("Please select a customer and an item.");
                return;
            }

            if (!int.TryParse(txtQuantity.Text.Trim(), out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Quantity must be a positive whole number.");
                return;
            }

            try
            {
                Order newOrder = new Order
                {
                    CustomerId = (int)cmbCustomer.SelectedValue,
                    ItemId = (int)cmbItem.SelectedValue,
                    Quantity = quantity
                };

                orderRepository.Add(newOrder);

                MessageBox.Show("Order placed successfully.");
                txtQuantity.Clear();
                LoadOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error placing order: " + ex.Message);
            }
        }

        private void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            if (dgvOrders.CurrentRow == null)
            {
                MessageBox.Show("Please select a row to delete.");
                return;
            }

            Order selected = (Order)dgvOrders.CurrentRow.DataBoundItem;

            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to delete this order?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                orderRepository.Delete(selected.Id);
                MessageBox.Show("Order deleted.");
                LoadOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting order: " + ex.Message);
            }
        }
        private void OrderForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}