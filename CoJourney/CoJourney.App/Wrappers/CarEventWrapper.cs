using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CoJourney.BL.Models;

namespace CoJourney.App.Wrappers
{
    public class CarEventWrapper : ModelWrapper<CarEventDetailModel>
    {
        public CarEventWrapper(CarEventDetailModel model): base(model)
        {
        }
        public DateTime BeginTime
        {
            get => GetValue<DateTime>();
            set => SetValue(value);
        }

        public DateTime EndTime
        {
            get => GetValue<DateTime>();
            set => SetValue(value);
        }

        public string? TargetLocation
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string? Name
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public Guid? InstitutorId
        {
            get => GetValue<Guid>();
            set => SetValue(value);
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                yield return new ValidationResult($"{nameof(Name)} is required", new[] { nameof(Name) });
            }

            if (string.IsNullOrWhiteSpace(TargetLocation))
            {
                yield return new ValidationResult($"{nameof(TargetLocation)} is required", new[] { nameof(TargetLocation) });
            }

            if (DateTime.Now > BeginTime || BeginTime > EndTime)
            {
                yield return new ValidationResult($"{nameof(BeginTime)} is required", new[] { nameof(BeginTime) });
            }
            
        }

        public static implicit operator CarEventWrapper(CarEventDetailModel detailModel)
            => new(detailModel);

        public static implicit operator CarEventDetailModel(CarEventWrapper wrapper)
            => wrapper.Model;
    }
}