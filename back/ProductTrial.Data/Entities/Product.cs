namespace ProductTrial.Data.Entities
{
    public class Product
    {
        public Product(int id, string code, string name, string description, string image, string category, decimal price, int quantity, string internalReference, int shellId, InventoryStatus inventoryStatus, int rating)
        : this(code, name, description, image, category, price, quantity, internalReference, shellId, inventoryStatus, rating)
        {
            Id = id;
        }

        public Product(string code, string name, string description, string image, string category, decimal price, int quantity, string internalReference, int shellId, InventoryStatus inventoryStatus, int rating)
        {
            Code = code;
            Name = name;
            Description = description;
            Image = image;
            Category = category;
            Price = price;
            Quantity = quantity;
            InternalReference = internalReference;
            ShellId = shellId;
            InventoryStatus = inventoryStatus;
            Rating = rating;
        }

        public string Category { get; set; }
        public string Code { get; set; }
        public long CreatedAt { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public string Image { get; set; }
        public string InternalReference { get; set; }
        public virtual InventoryStatus InventoryStatus { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Rating { get; set; }
        public int ShellId { get; set; }
        public long UpdatedAt { get; set; }
    }
}