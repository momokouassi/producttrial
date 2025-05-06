namespace ProductTrial.Data.Entities
{
    public class ProductDto
    {
        public ProductDto(int id, string code, string name, string description, string image, string category, decimal price, int quantity, string internalReference, int shellId, InventoryStatus inventoryStatus, int rating)
        {
            Id = id;
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

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string InternalReference { get; set; }
        public int ShellId { get; set; }
        public virtual InventoryStatus InventoryStatus { get; set; }
        public int Rating { get; set; }
    }
}
