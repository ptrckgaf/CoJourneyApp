using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CoJourney.BL.Models;
namespace CoJourney.App.Wrappers
{
    public class CarWrapper : ModelWrapper<CarDetailModel>
    {
        public CarWrapper(CarDetailModel model): base(model)
        {
        }
        public string? Producer
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public string? ModelName
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public DateTime? FirstRegistrationDate
        {
            get => GetValue<DateTime>();
            set => SetValue(value);
        }
        public int? Capacity
        {
            get => GetValue<int>();
            set => SetValue(value);
        }

        public string? ImageURl
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public Guid? OwnerId
        {
            get => GetValue<Guid>();
            set => SetValue(value);
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Producer))
            {
                yield return new ValidationResult($"{nameof(Producer)} is required", new[] { nameof(Producer) });
            }

            if (string.IsNullOrWhiteSpace(ModelName))
            {
                yield return new ValidationResult($"{nameof(ModelName)} is required", new[] { nameof(ModelName) });
            }

            if (Capacity is null || Capacity <= 0)
            {
                yield return new ValidationResult($"{nameof(Capacity)} is required", new[] { nameof(Capacity) });
            }

            if (FirstRegistrationDate is null)
            {
                yield return new ValidationResult($"{nameof(FirstRegistrationDate)} is required", new[] { nameof(FirstRegistrationDate) });
            }

        }
        public static implicit operator CarWrapper(CarDetailModel detailModel)
            => new(detailModel);

        public static implicit operator CarDetailModel(CarWrapper wrapper)
            => wrapper.Model;
    }
}
