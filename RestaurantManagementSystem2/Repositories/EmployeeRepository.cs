using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using RestaurantManagementSystem2.Models;
using System.Configuration;

namespace RestaurantManagementSystem2.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RestaurantDB"].ConnectionString;

        public List<Employee> GetAll()
        {
            List<Employee> employees = new List<Employee>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Employees";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Employee emp = new Employee
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FullName = reader["FullName"].ToString(),
                        Position = reader["Position"].ToString(),
                        Salary = Convert.ToDecimal(reader["Salary"])
                    };
                    employees.Add(emp);
                }
            }

            return employees;
        }

        public void Add(Employee entity)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Employees (FullName, Position, Salary) VALUES (@FullName, @Position, @Salary)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FullName", entity.FullName);
                cmd.Parameters.AddWithValue("@Position", entity.Position);
                cmd.Parameters.AddWithValue("@Salary", entity.Salary);
                cmd.ExecuteNonQuery();
            }
        }
        public void Update(Employee entity)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Employees SET FullName = @FullName, Position = @Position, Salary = @Salary WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FullName", entity.FullName);
                cmd.Parameters.AddWithValue("@Position", entity.Position);
                cmd.Parameters.AddWithValue("@Salary", entity.Salary);
                cmd.Parameters.AddWithValue("@Id", entity.Id);
                cmd.ExecuteNonQuery();
            }
        }
        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Employees WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}