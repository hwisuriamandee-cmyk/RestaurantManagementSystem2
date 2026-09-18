using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using RestaurantManagementSystem2.Models;
using System.Configuration;

namespace RestaurantManagementSystem2.Repositories
{
    public class InventoryRepository : IRepository<InventoryItem>
    {
        private readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RestaurantDB"].ConnectionString;

        public List<InventoryItem> GetAll()
        {
            List<InventoryItem> items = new List<InventoryItem>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM InventoryItems";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    InventoryItem item = new InventoryItem
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        ItemName = reader["ItemName"].ToString(),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        Price = Convert.ToDecimal(reader["Price"])
                    };
                    items.Add(item);
                }
            }

            return items;
        }

        public void Add(InventoryItem entity)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO InventoryItems (ItemName, Quantity, Price) VALUES (@ItemName, @Quantity, @Price)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ItemName", entity.ItemName);
                cmd.Parameters.AddWithValue("@Quantity", entity.Quantity);
                cmd.Parameters.AddWithValue("@Price", entity.Price);
                cmd.ExecuteNonQuery();
            }
        }
        public void Update(InventoryItem entity)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE InventoryItems SET ItemName = @ItemName, Quantity = @Quantity, Price = @Price WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ItemName", entity.ItemName);
                cmd.Parameters.AddWithValue("@Quantity", entity.Quantity);
                cmd.Parameters.AddWithValue("@Price", entity.Price);
                cmd.Parameters.AddWithValue("@Id", entity.Id);
                cmd.ExecuteNonQuery();
            }
        }
        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM InventoryItems WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}