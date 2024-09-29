namespace StoreSampleAPI.DTOs
{
    public class CreateOrderDTO
    {
        public OrderDTO Order { get; set; }
        public OrderDetailDTO OrderDetail { get; set; }
    }
}
