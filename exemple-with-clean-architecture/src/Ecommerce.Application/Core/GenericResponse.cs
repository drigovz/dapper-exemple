using Ecommerce.Application.Notifications;
using System.Collections.Generic;

namespace Ecommerce.Application.Core
{
    public class GenericResponse
    {
        public dynamic Result { get; set; } = default;

        public IEnumerable<Notification> Notifications { get; set; }
    }
}
