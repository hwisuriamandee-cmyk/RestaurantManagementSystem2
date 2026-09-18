using System;

namespace RestaurantManagementSystem2.Models
{
    public class Order : BaseEntity
    {
        public int CustomerId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }

        public string CustomerName { get; set; }
        public string ItemName { get; set; }
        public decimal TotalPrice { get; set; }

        public Order() { }

        public override string ToString()
        {
            return "Order #" + Id;
        }
    }
}