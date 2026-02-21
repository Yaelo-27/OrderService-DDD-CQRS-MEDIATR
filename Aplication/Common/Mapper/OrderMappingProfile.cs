using Aplication.dto;
using AutoMapper;
using Domain.Orders;
using Domain.ValueObjects;

namespace Aplication.Common.Mapper;
public sealed class OrderMappingProfile : Profile
{
   public OrderMappingProfile(){
     CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.ShippingContactName,
                opt => opt.MapFrom(src => src.ShippingContact.Name))
            .ForMember(dest => dest.ShippingContactEmail,
                opt => opt.MapFrom(src => src.ShippingContact.Email))
            .ForMember(dest => dest.ShippingContactPhone,
                opt => opt.MapFrom(src => src.ShippingContact.PhoneNumber.Value))
            .ForMember(dest => dest.ShippingAddressStreet,
                opt => opt.MapFrom(src => src.ShippingAddress.Street))
            .ForMember(dest => dest.ShippingAddressCity,
                opt => opt.MapFrom(src => src.ShippingAddress.City))
            .ForMember(dest => dest.ShippingAddressState,
                opt => opt.MapFrom(src => src.ShippingAddress.State))
            .ForMember(dest => dest.ShippingAddressPostalCode,
                opt => opt.MapFrom(src => src.ShippingAddress.PostalCode))
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Items,
                opt => opt.MapFrom(src => src.Items));

        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.ProductId,
                opt => opt.MapFrom(src => src.ProductId.Value));
   }
}