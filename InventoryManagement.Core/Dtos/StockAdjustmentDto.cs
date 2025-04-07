namespace InventoryManagement.Core.Dtos
{
    public class StockAdjustmentDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string TransactionType { get; set; }
        public string Notes { get; set; }
    }
}