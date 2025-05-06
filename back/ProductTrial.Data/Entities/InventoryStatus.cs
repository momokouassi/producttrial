namespace ProductTrial.Data.Entities
{
    public enum InventoryStatus
    {
        InStock = 1,
        LowStock = 2,
        OutOfStock = 3,
    }

    public static class InventoryStatusExtensions
    {
        public static string GetStringValue(this InventoryStatus status)
        {
            return status.ToString().ToUpper();
        }
    }
}