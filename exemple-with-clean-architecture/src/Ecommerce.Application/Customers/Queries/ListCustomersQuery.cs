using Ecommerce.Application.Core;
using MediatR;

namespace Ecommerce.Application.Customers.Queries
{
    public class ListCustomersQuery : IRequest<GenericResponse>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Genre { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
    }
}
