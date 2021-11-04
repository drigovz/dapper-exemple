using Dapper.Contrib.Extensions;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Text.Json.Serialization;

namespace Ecommerce.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }

        private DateTime? _createdAt;

        public DateTime? CreatedAt
        {
            get => _createdAt;
            set => _createdAt = (value == null ? DateTime.UtcNow : value);
        }

        public DateTime? UpdatedAt { get; set; }

        [Computed]
        [JsonIgnore]
        public bool Valid { get; protected set; }

        [Computed]
        [JsonIgnore]
        public ValidationResult ValidationResult { get; protected set; }

        protected bool EntityValidation<T>(T model, AbstractValidator<T> validator)
        {
            ValidationResult = validator.Validate(model);
            return Valid = ValidationResult.IsValid;
        }
    }
}
