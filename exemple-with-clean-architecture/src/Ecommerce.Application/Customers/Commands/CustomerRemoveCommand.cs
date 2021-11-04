using Ecommerce.Application.Core;
using MediatR;

namespace Ecommerce.Application.Customers.Commands
{
    public class CustomerRemoveCommand : IRequest<GenericResponse>
    {
        public int Id { get; set; }
    }
}
