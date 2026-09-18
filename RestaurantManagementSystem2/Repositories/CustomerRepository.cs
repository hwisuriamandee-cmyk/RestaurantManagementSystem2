using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using RestaurantManagementSystem2.Models;
using System.Configuration;

namespace RestaurantManagementSystem2.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["RestaurantDB"].ConnectionString;

        public List<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Customers";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Customer c = new Customer
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FullName = reader["FullName"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Email = reader["Email"] == DBNull.Value ? null : reader["Email"].ToString()
                    };
                    customers.Add(c);
                }
            }

            return customers;
        }

        public void Add(Customer entity)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Customers (FullName, Phone, Email) VALUES (@FullName, @Phone, @Email)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FullName", entity.FullName);
                cmd.Parameters.AddWithValue("@Phone", entity.Phone);
                cmd.Parameters.AddWithValue("@Email", (object)entity.Email ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }
        public void Update(Customer entity)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Customers SET FullName = @FullName, Phone = @Phone, Email = @Email WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FullName", entity.FullName);
                cmd.Parameters.AddWithValue("@Phone", entity.Phone);
                cmd.Parameters.AddWithValue("@Email", (object)entity.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Id", entity.Id);
                cmd.ExecuteNonQuery();
            }
        }
        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Customers WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}