using AutoMapper;
using FoodDelivery.DTOs;
using FoodDelivery.Models;

namespace FoodDelivery.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        { // Customer
            CreateMap<UpdateCustomerDto, Customer>()
    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Customer, CustomerDto>();

            // Restaurant
            CreateMap<Restaurant, RestaurantDto>(); 
            CreateMap<UpdateRestaurantDto, Restaurant>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // MenuItem
            //CreateMap<MenuItem, MenuItemDto>(); CreateMap<CreateMenuItemDto, MenuItem>();
            CreateMap<MenuItem, MenuItemDto>().ForMember(dest => dest.RestaurantName, opt => opt.MapFrom(src => src.Restaurant.Name));
            CreateMap<CreateMenuItemDto, MenuItem>();
            CreateMap<UpdateMenuItemDto, MenuItem>();

            // Order
            CreateMap<Order, OrderDto>();
            CreateMap<CreateOrderDto, Order>();

            // Delivery
            CreateMap<Delivery, DeliveryDto>()
                .ForMember(dest => dest.AgentName, opt =>
    opt.MapFrom(src => src.Agent != null ? src.Agent.Name : string.Empty))
                .ForMember(dest => dest.RestaurantName, opt =>
    opt.MapFrom(src => src.Order != null && src.Order.Restaurant != null ? src.Order.Restaurant.Name : string.Empty));
            CreateMap<CreateDeliveryDto, Delivery>();
            CreateMap<UpdateDeliveryDto, Delivery>();

            // Cart
            CreateMap<Cart, CartDto>().ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.Item!.Name)).ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Item!.Price)); CreateMap<CreateCartDto, Cart>(); CreateMap<UpdateCartDto, Cart>();

            // Payment
            CreateMap<Payment, PaymentDto>();
            CreateMap<CreatePaymentDto, Payment>();
        }
    }

}




//// ✅ MappingProfiles/AutoMapperProfile.cs using AutoMapper; using FoodDelivery.DTOs; using FoodDelivery.Models;

//using AutoMapper;
//using FoodDelivery.DTOs;
//using FoodDelivery.Models;

//namespace FoodDelivery.MappingProfiles
//{
//    public class AutoMapperProfile : Profile
//    {
//        public AutoMapperProfile()
//        { // Cart mappings
//            CreateMap<Cart, CartDto>() .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.Item != null ? src.Item.Name : string.Empty)) .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Item != null ? src.Item.Price : 0));

//            CreateMap<CreateCartDto, Cart>();

//            // Delivery mappings
//            CreateMap<Delivery, DeliveryDto>()
//                .ForMember(dest => dest.AgentName, opt => opt.MapFrom(src => src.Agent != null ? src.Agent.Name : string.Empty));

//            CreateMap<CreateDeliveryDto, Delivery>();

//            // MenuItem

//            CreateMap<MenuItem, MenuItemDto>().ForMember(dest => dest.RestaurantName, opt => opt.MapFrom(src => src.Restaurant != null ? src.Restaurant.Name : string.Empty));

//            CreateMap<CreateMenuItemDto, MenuItem>();
//        }
//    }

//}



//using AutoMapper;
//using FoodDelivery.DTOs;
//using FoodDelivery.Models;

//namespace FoodDelivery.Profiles
//{
//    public class AutoMapperProfile : Profile
//    {
//        public AutoMapperProfile()
//        {

//            CreateMap<Customer, CustomerDto>();
//            CreateMap<CreateCustomerDto, Customer>();
//            CreateMap<Restaurant, RestaurantDto>().ReverseMap();
//            CreateMap<CreateRestaurantDto, Restaurant>();
//            CreateMap<MenuItem, MenuItemDto>();
//            CreateMap<CreateMenuItemDto, MenuItem>();
//            CreateMap<Order, OrderDto>();
//            CreateMap<CreateOrderDto, Order>();
//            CreateMap<Delivery, DeliveryDto>();
//            CreateMap<CreateDeliveryDto, Delivery>();
//            CreateMap<Payment, PaymentDto>();
//            CreateMap<CreatePaymentDto, Payment>();



//            CreateMap<Cart, CartDto>()
//              .ForMember(dest => dest.ItemName,
//               opt => opt.MapFrom(src => src.Item != null ? src.Item.Name : null))
//              .ForMember(dest => dest.Price,
//               opt => opt.MapFrom(src => src.Item != null ? src.Item.Price : 0));


//            //CreateMap<Cart, CartDto>();
//            CreateMap<CreateCartDto, Cart>();

//        }
//    }
//} 