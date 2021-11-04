using Ecommerce.Application.Core;
using Ecommerce.Application.Customers.Commands;
using Ecommerce.Application.Notifications;
using Ecommerce.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Customers.Handlers.Commands
{
    public class CustomerRemoveCommandHandler : IRequestHandler<CustomerRemoveCommand, GenericResponse>
    {
        private readonly ICustomerRepository _repository;
        private readonly NotificationContext _notification;

        public CustomerRemoveCommandHandler(ICustomerRepository repository, NotificationContext notification)
        {
            _repository = repository;
            _notification = notification;
        }

        public async Task<GenericResponse> Handle(CustomerRemoveCommand request, CancellationToken cancellationToken)
        {
            var client = await _repository.GetAsync(request.Id);
            if (client == null)
                return new GenericResponse
                {
                    Notifications = _notification.AddNotification("Error", $"Customer with id {request.Id} not found!"),
                };

            await _repository.RemoveAsync(request.Id);
            return new GenericResponse
            {
                Notifications = _notification.AddNotification("Success", "Customer deleted succesfull!"),
            };
        }
    }
}
