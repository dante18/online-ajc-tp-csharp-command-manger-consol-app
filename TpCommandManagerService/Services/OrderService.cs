using TpCommandManagerData.Context;
using TpCommandManagerData.Entities;
using TpCommandManagerData.Repositories;
using TpCommandManagerService.Dtos;

namespace TpCommandManagerService.Services;

public sealed class OrderService
{
    private readonly OrderRepository _repository;

    public OrderService(CommandStoreContext context)
    {
        this._repository = new OrderRepository(context);
    }

    public List<OrderDto> GetAllOrders()
    {
        List<OrderDto> orders = this._repository.GetAllOrders().Select(o => new OrderDto()
        {
            Id = o.Id,
            OrderDate = o.OrderDate,
            DeliveryDate = o.DeliveryDate,
            Status = o.Status,
            User = new UserDto()
            {
                Id = o.User.Id,
                FirstName = o.User.FirstName,
                LastName = o.User.LastName,
                Mail = o.User.Mail,
                Phone = o.User.Phone,
                Address = new AddressDto()
                {
                    Id = o.User.Address.Id,
                    Street = o.User.Address.Street,
                    City = o.User.Address.City,
                    State = o.User.Address.State,
                    Zip = o.User.Address.Zip,
                    Country = o.User.Address.Country,
                }
            },
            DeliveryAddress = new AddressDto()
            {
                Id = o.DeliveryAddress.Id,
                Street = o.DeliveryAddress.Street,
                City = o.DeliveryAddress.City,
                State = o.DeliveryAddress.State,
                Zip = o.DeliveryAddress.Zip,
                Country = o.DeliveryAddress.Country,
            },
            Products = o.OrderProduct.Select(op => new OrderProductDto()
            {
                Id = op.Id,
                Product = new ProductDto()
                {
                    Id = op.Product.Id ?? 0,
                    Name = op.Product.Name,
                    Price = op.Product.Price
                }
            }).ToList()
        }).ToList();

        return orders;
    }

    public OrderDto GetOrder(int id)
    {
        Order order = this._repository.GetOrder(id);

        if (order is null)
        {
            return null;
        }

        OrderDto orderDto = new OrderDto()
        {
            Id = order.Id,
            OrderDate = order.OrderDate,
            DeliveryDate = order.DeliveryDate,
            Status = order.Status,
            User = new UserDto()
            {
                Id = order.User.Id,
                FirstName = order.User.FirstName,
                LastName = order.User.LastName,
                Mail = order.User.Mail,
                Phone = order.User.Phone,
                Address = new AddressDto()
                {
                    Id = order.User.Address.Id,
                    Street = order.User.Address.Street,
                    City = order.User.Address.City,
                    State = order.User.Address.State,
                    Zip = order.User.Address.Zip,
                    Country = order.User.Address.Country,
                }
            },
            DeliveryAddress = new AddressDto()
            {
                Id = order.DeliveryAddress.Id,
                Street = order.DeliveryAddress.Street,
                City = order.DeliveryAddress.City,
                State = order.DeliveryAddress.State,
                Zip = order.DeliveryAddress.Zip,
                Country = order.DeliveryAddress.Country,
            },
            Products = order.OrderProduct.Select(op => new OrderProductDto()
            {
                Id = op.Id,
                Product = new ProductDto()
                {
                    Id = op.Product.Id ?? 0,
                    Name = op.Product.Name,
                    Price = op.Product.Price
                }
            }).ToList()
        };

        return orderDto;
    }

    public void CreateOrder(OrderDto order)
    {
        Order newOrder = new Order()
        {
            OrderDate = order.OrderDate,
            DeliveryDate = order.DeliveryDate,
            Status = order.Status,
            UserId = order.User.Id,
            DeliveryAddressId = order.DeliveryAddress.Id,
            OrderProduct = order.Products.Select(p => new OrderProduct()
            {
                ProductId = p.Product.Id
            }).ToList()
        };

        this._repository.CreateOrder(newOrder);
    }

    public void UpdateOrder(int id, OrderDto data)
    {
        Order existingOrder = this._repository.GetOrder(id);
        existingOrder.Status = data.Status;
        existingOrder.DeliveryDate = data.DeliveryDate;
        existingOrder.UserId = data.User.Id;

        this._repository.UpdateOrder(existingOrder);
    }

    public void UpdateOrderAddProductRow(int id, OrderDto data)
    {
        Order existingOrder = this._repository.GetOrder(id);
        List<int> productIds = data.Products.Select(p => p.Product.Id).ToList();
        this._repository.UpdateOrderAddProduct(existingOrder, productIds);
    }

    public void UpdateOrderRemoveProductRow(int id, int productId)
    {
        Order existingOrder = this._repository.GetOrder(id);
        this._repository.UpdateOrderRemoveProduct(existingOrder, productId);
    }

    public void DeleteOrder(int id)
    {
        Order existingOrder = this._repository.GetOrder(id);
        this._repository.DeleteOrder(existingOrder);
    }
}
