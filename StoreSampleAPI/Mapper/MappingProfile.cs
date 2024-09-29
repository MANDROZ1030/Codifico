using AutoMapper;
using StoreSampleAPI.DTOs;
using StoreSampleAPI.Models;

namespace StoreSampleAPI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDTO>();
            CreateMap<Customer, CustomerOrderPredictionDTO>();
            CreateMap<Order, OrderDTO>();
            CreateMap<OrderDetail, OrderDetailDTO>();
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<Shipper, ShipperDTO>();
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Categoryname))
                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.Companyname));
            CreateMap<OrderDTO, Order>()
                  .ForMember(dest => dest.Custid, opt => opt.MapFrom(src => src.CustId))
                  .ForMember(dest => dest.Orderid, opt => opt.Ignore())
                  .ForMember(dest => dest.Orderdate, opt => opt.MapFrom(src => src.OrderDate.ToUniversalTime()))
                  .ForMember(dest => dest.Requireddate, opt => opt.MapFrom(src => src.RequiredDate.ToUniversalTime()))
                  .ForMember(dest => dest.Shippeddate, opt => opt.MapFrom(src => src.ShippedDate.HasValue ? (DateTime?)src.ShippedDate.Value.ToUniversalTime() : null));

            CreateMap<OrderDetailDTO, OrderDetail>()
                  .ForMember(dest => dest.Orderid, opt => opt.Ignore())
                  .ForMember(dest => dest.Order, opt => opt.Ignore())
                  .ForMember(dest => dest.Product, opt => opt.Ignore())
                  .ForMember(dest => dest.Unitprice, opt => opt.MapFrom(src => (decimal)src.UnitPrice))
                  .ForMember(dest => dest.Qty, opt => opt.MapFrom(src => (short)src.Quantity))
                  .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => (float)src.Discount));
        }

    }
}
