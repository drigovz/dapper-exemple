using Ecommerce.Application.Core;
using Ecommerce.Application.Customers.Queries;
using Ecommerce.Application.Notifications;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Customers.Handlers.Queries
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, GenericResponse>
    {
        private readonly ICustomerRepository _repository;
        private readonly NotificationContext _notification;

        public GetCustomersQueryHandler(ICustomerRepository repository, NotificationContext notification)
        {
            _repository = repository;
            _notification = notification;
        }

        public async Task<GenericResponse> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var customer = new Customer { };
            return new GenericResponse { Result = await _repository.GetWithParamsAsync("CreatedAt", "Name", "Email"), };
        }
    }
}
