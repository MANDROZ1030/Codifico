namespace StoreSampleAPI.DTOs
{
    public class CustomerOrdersDTO
    {
        public string CompanyName { get; set; }
        public IEnumerable<OrderDTO> Orders { get; set; }
    }
}