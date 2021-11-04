using Ecommerce.Application.Core;
using Ecommerce.Application.Customers.Commands;
using Ecommerce.Application.Notifications;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Customers.Handlers.Commands
{
    public class CustomerCreateCommandHandler : IRequestHandler<CustomerCreateCommand, GenericResponse>
    {
        private readonly ICustomerRepository _repository;
        private readonly NotificationContext _notification;

        public CustomerCreateCommandHandler(ICustomerRepository repository, NotificationContext notification)
        {
            _repository = repository;
            _notification = notification;
        }

        public async Task<GenericResponse> Handle(CustomerCreateCommand request, CancellationToken cancellationToken)
        {
            var client = new Customer(request.Name, request.Email, request.Genre, request.RG, request.CPF, request.MotherName, request.Status);
            if (!client.Valid)
            {
                _notification.AddNotifications(client.ValidationResult);

                return new GenericResponse
                {
                    Notifications = _notification.Notifications,
                };
            }

            var result = await _repository.AddAsync(client);
            if (result == null)
            {
                _notification.AddNotification("Error", "Error When try to add new client!");

                return new GenericResponse { Notifications = _notification.Notifications, };
            }

            return new GenericResponse { Result = result, };
        }
    }
}
