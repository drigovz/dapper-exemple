using Ecommerce.Application.Core;
using MediatR;

namespace Ecommerce.Application.Customers.Queries
{
    public class GetCustomerByIdQuery : IRequest<GenericResponse>
    {
        public int Id { get; set; }
    }
}
