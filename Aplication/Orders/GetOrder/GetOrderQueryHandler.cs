using Aplication.Common.Constants;
using Aplication.dto;
using Aplication.Interfaces;
using AutoMapper;
using Domain.Orders;
using MediatR;

namespace Aplication.Orders
{
    public sealed class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public GetOrderQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderDto> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            OrderId orderId = new(request.OrderId);

            Order order = await _orderRepository.GetByIdAsync(orderId) ?? throw new KeyNotFoundException($"Order with ID {request.OrderId} not found.");
            // Automapper
            return _mapper.Map<OrderDto>(order);
        }
    }
}