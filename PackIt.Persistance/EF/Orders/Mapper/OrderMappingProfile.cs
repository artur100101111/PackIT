using AutoMapper;
using PackIt.Application.Orders.DTO;
using PackIt.Persistance.EF.Orders.ReadModels;

namespace PackIt.Persistance.EF.Orders.Mapper
{

    internal class OrderMappingProfile: Profile
    {
        public OrderMappingProfile()
        {
            // LocationVoReadModel -> LocationVoDto
            CreateMap<LocationVoReadModel, LocationVoDTO>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Code, opt => opt.MapFrom(s => s.Code))
                .ForMember(d => d.Type, opt => opt.MapFrom(s => s.Type));

            // ItemVOReadModel -> ItemVoDto
            CreateMap<ItemVOReadModel, ItemVoDTO>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Code, opt => opt.MapFrom(s => s.Code))
                .ForMember(d => d.TypeName, opt => opt.MapFrom(s => s.TypeName))
                .ForMember(d => d.TypeCode, opt => opt.MapFrom(s => s.TypeCode));

            // OrderItemReadModel -> OrderItemDto
            CreateMap<OrderItemReadModel, OrderItemDto>()
                .ForMember(d => d.Quantity, opt => opt.MapFrom(s => s.Quantity))
                .ForMember(d => d.ItemVO, opt => opt.MapFrom(s => s.ItemVO));

            // OrderStateChangeReadModel -> OrderStateChangeDto
            CreateMap<OrderStateChangedReadModel, OrderStatedChangeDTO>()
                .ForMember(d => d.PreviousState, opt => opt.MapFrom(s => s.PreviousState))
                .ForMember(d => d.CurrentState, opt => opt.MapFrom(s => s.CurrentState))
                .ForMember(d => d.EventTime, opt => opt.MapFrom(s => s.EventTime));

            // OrderReadModel -> OrderDto
            CreateMap<OrderReadModel, OrderDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.CreationDate, opt => opt.MapFrom(s => s.CreationDate))
                .ForMember(d => d.OrderItems, opt => opt.MapFrom(s => s.OrderItems))
                .ForMember(d => d.RequestedDeliveryLocation, opt => opt.MapFrom(s => s.RequestedDeliveryLocation))
                .ForMember(d => d.DeliveryLocation, opt => opt.MapFrom(s => s.DeliveryLocation))
                .ForMember(d => d.DeliveryDate, opt => opt.MapFrom(s => s.DeliveryDate))
                .ForMember(d => d.State, opt => opt.MapFrom(s => s.State))
                .ForMember(d => d.StateChangesHistory, opt => opt.MapFrom(s => s.StateChangesHistory));

            // OrderTemplateReadModel -> OrderTemplateDto
            CreateMap<OrderTemplateReadModel, OrderTemplateDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.CreationDate, opt => opt.MapFrom(s => s.CreationDate))
                .ForMember(d => d.OrderItems, opt => opt.MapFrom(s => s.OrderItems))
                .ForMember(d => d.RequestedDeliveryLocation, opt => opt.MapFrom(s => s.RequestedDeliveryLocation))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));
        }
    }
}
