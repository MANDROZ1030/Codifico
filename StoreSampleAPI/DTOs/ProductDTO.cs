namespace StoreSampleAPI.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public bool Discontinued { get; set; }
        public string CategoryName { get; set; }
        public string SupplierName { get; set; }
    }
}
