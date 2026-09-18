using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using RestaurantManagementSystem2.Models;
using System.Configuration;

namespace RestaurantManagementSystem2.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RestaurantDB"].ConnectionString;

        public List<Order> GetAll()
        {
            List<Order> orders = new List<Order>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT o.Id, o.CustomerId, o.ItemId, o.Quantity, o.OrderDate,
                           c.FullName AS CustomerName, i.ItemName, (i.Price * o.Quantity) AS TotalPrice
                    FROM Orders o
                    JOIN Customers c ON o.CustomerId = c.Id
                    JOIN InventoryItems i ON o.ItemId = i.Id
                    ORDER BY o.OrderDate DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Order o = new Order
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        CustomerId = Convert.ToInt32(reader["CustomerId"]),
                        ItemId = Convert.ToInt32(reader["ItemId"]),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                        CustomerName = reader["CustomerName"].ToString(),
                        ItemName = reader["ItemName"].ToString(),
                        TotalPrice = Convert.ToDecimal(reader["TotalPrice"])
                    };
                    orders.Add(o);
                }
            }

            return orders;
        }

        public void Add(Order entity)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Orders (CustomerId, ItemId, Quantity) VALUES (@CustomerId, @ItemId, @Quantity)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CustomerId", entity.CustomerId);
                cmd.Parameters.AddWithValue("@ItemId", entity.ItemId);
                cmd.Parameters.AddWithValue("@Quantity", entity.Quantity);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Orders WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Customer> GetAllCustomers()
        {
            CustomerRepository customerRepo = new CustomerRepository();
            return customerRepo.GetAll();
        }

        public List<InventoryItem> GetAllItems()
        {
            InventoryRepository inventoryRepo = new InventoryRepository();
            return inventoryRepo.GetAll();
        }
    }
}