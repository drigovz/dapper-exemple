using Ecommerce.Application.Core;
using MediatR;

namespace Ecommerce.Application.Customers.Commands
{
    public class CustomerCommand : IRequest<GenericResponse>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Genre { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string MotherName { get; set; }
        public string Status { get; set; }
    }
}
