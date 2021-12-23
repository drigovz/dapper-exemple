using Ecommerce.Application.Core;
using Ecommerce.Application.Customers.Queries;
using Ecommerce.Application.Notifications;
using Ecommerce.Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Customers.Handlers.Queries
{
    public class ListCustomersQueryHandler : IRequestHandler<ListCustomersQuery, GenericResponse>
    {
        private readonly ICustomerRepository _repository;
        private readonly NotificationContext _notification;

        public ListCustomersQueryHandler(ICustomerRepository repository, NotificationContext notification)
        {
            _repository = repository;
            _notification = notification;
        }

        public async Task<GenericResponse> Handle(ListCustomersQuery request, CancellationToken cancellationToken)
        {
            var dictionary = new Dictionary<string, object>();
            string filter = "\n";

            if (!string.IsNullOrEmpty(request.Name) ||
                !string.IsNullOrEmpty(request.Email) ||
                !string.IsNullOrEmpty(request.Genre) ||
                !string.IsNullOrEmpty(request.Status) ||
                !string.IsNullOrEmpty(request.Date))
            {
                filter += "WHERE ";

                if (!string.IsNullOrEmpty(request.Name))
                {
                    var parts = request.Name.Split(' ');
                    if (parts.Length > 1)
                    {
                        for (int i = 0; i < parts.Length; i++)
                        {
                            if (i == 0)
                            {
                                dictionary.Add($"@Name_{i}", parts[i]);
                                filter += $"LOWER(Name) LIKE LOWER('%' + @Name_{i} + '%') \n";
                            }
                            else
                            {
                                dictionary.Add($"@Name_{i}", parts[i]);
                                filter += $"AND LOWER(Name) LIKE LOWER('%' + @Name_{i} + '%') \n";
                            }
                        }
                    }
                    else
                    {
                        dictionary.Add("@Name", request.Name);
                        filter += $"LOWER(Name) LIKE LOWER('%' + @Name + '%') \n";
                    }
                }

                if (!string.IsNullOrEmpty(request.Email))
                {
                    if (string.IsNullOrEmpty(request.Name))
                    {
                        dictionary.Add("@Email", request.Email);
                        filter += "LOWER(Email) LIKE LOWER('%' + @Email + '%') \n";
                    }
                    else
                    {
                        dictionary.Add("@Email", request.Email);
                        filter += "AND LOWER(Email) LIKE LOWER('%' + @Email + '%') \n";
                    }
                }

                if (!string.IsNullOrEmpty(request.Genre))
                {
                    if (string.IsNullOrEmpty(request.Name) && string.IsNullOrEmpty(request.Email))
                    {
                        dictionary.Add("@Genre", request.Genre);
                        filter += "Genre = UPPER(@Genre) \n";
                    }
                    else
                    {
                        dictionary.Add("@Genre", request.Genre);
                        filter += "AND Genre = UPPER(@Genre) \n";
                    }
                }

                if (!string.IsNullOrEmpty(request.Status))
                {
                    if (string.IsNullOrEmpty(request.Name) && string.IsNullOrEmpty(request.Email) && string.IsNullOrEmpty(request.Genre))
                    {
                        dictionary.Add("@Status", request.Status);
                        filter += "Status = UPPER(@Status) \n";
                    }
                    else
                    {
                        dictionary.Add("@Status", request.Status);
                        filter += "AND Status = UPPER(@Status) \n";
                    }
                }

                if (!string.IsNullOrEmpty(request.Date))
                {
                    if (string.IsNullOrEmpty(request.Name) && string.IsNullOrEmpty(request.Email) && string.IsNullOrEmpty(request.Genre) && string.IsNullOrEmpty(request.Status))
                        filter += "CAST(CreatedAt AS DATE) ";
                    else
                        filter += "AND CAST(CreatedAt AS DATE) ";

                    var parts = request.Date.Split(',');
                    if (parts.Length > 1)
                    {
                        for (int i = 0; i < parts.Length; i++)
                        {
                            if (i == 0)
                            {
                                dictionary.Add($"@DateInit", parts[i]);
                                filter += $"BETWEEN CAST(@DateInit AS DATE) ";
                            }
                            else
                            {
                                dictionary.Add($"@DateEnd", parts[i]);
                                filter += $"AND CAST(@DateEnd AS DATE) \n";
                            }
                        }
                    }
                }
            }

            string sql = $@"
                    SELECT * FROM Customers
                    { filter }
                    ORDER BY Id ASC";

            var client = await _repository.ListCustomersAsync(sql, dictionary);
            if (client == null)
                return new GenericResponse
                {
                    Notifications = _notification.AddNotification("Error", $"Error when list customers!"),
                };

            return new GenericResponse
            {
                Result = client,
            };
        }
    }
}
