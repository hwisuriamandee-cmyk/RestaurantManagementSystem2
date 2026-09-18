namespace RestaurantManagementSystem2.Models
{
    public class Customer : BaseEntity
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public Customer() { }

        public Customer(int id, string fullName, string phone, string email)
        {
            Id = id;
            FullName = fullName;
            Phone = phone;
            Email = email;
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}