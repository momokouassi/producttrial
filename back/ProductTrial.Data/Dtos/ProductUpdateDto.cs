using ProductTrial.Data.Entities;

namespace ProductTrial.Data.Dtos
{
    public class ProductUpdateDto
    {
        public string Category { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string InternalReference { get; set; }
        public InventoryStatus InventoryStatus { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Rating { get; set; }
        public int ShellId { get; set; }
    }
}