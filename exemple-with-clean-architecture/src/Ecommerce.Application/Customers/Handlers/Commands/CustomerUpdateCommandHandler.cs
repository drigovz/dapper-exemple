using Ecommerce.Application.Core;
using Ecommerce.Application.Customers.Commands;
using Ecommerce.Application.Notifications;
using Ecommerce.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Customers.Handlers.Commands
{
    public class CustomerUpdateCommandHandler : IRequestHandler<CustomerUpdateCommand, GenericResponse>
    {
        private readonly ICustomerRepository _repository;
        private readonly NotificationContext _notification;

        public CustomerUpdateCommandHandler(ICustomerRepository repository, NotificationContext notification)
        {
            _repository = repository;
            _notification = notification;
        }

        public async Task<GenericResponse> Handle(CustomerUpdateCommand request, CancellationToken cancellationToken)
        {
            var client = await _repository.GetAsync(request.Id);
            if (client == null)
                return new GenericResponse
                {
                    Notifications = _notification.AddNotification("Error", $"Client with id {request.Id} not found!"),
                };

            client.Update(request.Name, request.Email, request.Genre, request.RG, request.CPF, request.MotherName, request.Status);
            await _repository.UpdateAsync(client);

            return new GenericResponse
            {
                Result = client,
                Notifications = _notification.AddNotification("Success", $"Client with id {request.Id} update succesfull!"),
            };
        }
    }
}
