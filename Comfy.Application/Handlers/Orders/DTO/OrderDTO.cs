using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain.Entities;

namespace Comfy.Application.Handlers.Orders.DTO;

public sealed record OrderDTO : IMapWith<Order>
{
    public int Id { get; set; }
    public string OrderStatus { get; set; } = null!;

    public decimal TotalPrice { get; set; }

    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Patronymic { get; set; } = null!;

    public string City { get; set; } = null!;
    public string Address { get; set; } = null!;

    public string? UserComment { get; set; }

    public IEnumerable<OrderProductDTO> Products { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Order, OrderDTO>()
            .ForMember(dto => dto.OrderStatus, x => x.MapFrom(order => order.OrderStatus.Name));
    }
}