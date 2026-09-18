namespace RestaurantManagementSystem2.Models
{
    public class Employee : BaseEntity
    {
        public string FullName { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }

        public Employee() { }

        public Employee(int id, string fullName, string position, decimal salary)
        {
            Id = id;
            FullName = fullName;
            Position = position;
            Salary = salary;
        }

        public override string ToString()
        {
            return FullName + " (" + Position + ")";
        }
    }
}