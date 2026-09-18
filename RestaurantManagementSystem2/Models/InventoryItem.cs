namespace RestaurantManagementSystem2.Models
{
    public class InventoryItem : BaseEntity
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public InventoryItem() { }

        public InventoryItem(int id, string itemName, int quantity, decimal price)
        {
            Id = id;
            ItemName = itemName;
            Quantity = quantity;
            Price = price;
        }

        public bool IsLowStock()
        {
            return Quantity < 10;
        }

        public override string ToString()
        {
            return ItemName;
        }
    }
}