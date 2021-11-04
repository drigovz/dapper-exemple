using Ecommerce.Application.Core;
using Ecommerce.Application.Customers.Queries;
using Ecommerce.Application.Notifications;
using Ecommerce.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Customers.Handlers.Queries
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, GenericResponse>
    {
        private readonly ICustomerRepository _repository;
        private readonly NotificationContext _notification;

        public GetCustomerByIdQueryHandler(ICustomerRepository repository, NotificationContext notification)
        {
            _repository = repository;
            _notification = notification;
        }

        public async Task<GenericResponse> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var client = await _repository.GetAsync(request.Id);
            if (client == null)
                return new GenericResponse
                {
                    Notifications = _notification.AddNotification("Error", $"Customer with id {request.Id} not found!"),
                };

            return new GenericResponse
            {
                Result = client,
            };
        }
    }
}
