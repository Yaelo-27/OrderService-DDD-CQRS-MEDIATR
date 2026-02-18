using MediatR;
using Domain.Orders;
using Domain.Primitives.Domain.Interaces;
using Domain.ValueObjects;
using Domain.Product;

namespace Aplication.Orders
{
    public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderId>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<OrderId> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            if(request.Items is null || !request.Items.Any())
            {
                throw new ArgumentException("Order must have at least one item.");
            }

            if(PhoneNumber.Create(request.ShippingContactPhoneNumber) is not PhoneNumber phoneNumber)
            {
                throw new ArgumentException("Invalid phone number format.");
            }

            if(Contact.Create(request.ShippingContactName, request.ShippingContactEmail, phoneNumber) is not Contact contact)
            {
                throw new ArgumentException("Invalid contact information.");
            }

            if(Address.Create(request.ShippingAddressStreet, request.ShippingAddressCity, request.ShippingAddressState, request.ShippingAddressPostalCode, request.ShippingAddressCountry) is not Address address)
            {
                throw new ArgumentException("Invalid address information.");
            }

            List<OrderItem?> orderItems = request.Items.Select(item => OrderItem.Create(new ProductId(item.ProductId), item.ProductName, item.Quantity, item.UnitPrice)).Where(item => item != null).ToList();
            Order order = new(
                new OrderId(Guid.NewGuid()),
                contact,
                address,
                orderItems!
            );

            await _orderRepository.AddAsync(order);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return order.Id;
        }
    }
}