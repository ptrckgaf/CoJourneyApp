using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CoJourney.BL.Models;
using CoJourney.DAL.Entities;

namespace CoJourney.App.Wrappers
{
    public class JourneyWrapper : ModelWrapper<JourneyDetailModel>
    {
        public JourneyWrapper(JourneyDetailModel model): base(model)
        {
        }
        public string? StartLocation
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public string? TargetLocation
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public DateTime? BeginTime
        {
            get => GetValue<DateTime>();
            set => SetValue(value);
        }
        public Guid? CarId
        {
            get => GetValue<Guid>();
            set => SetValue(value);
        }

        public Guid? DriverId
        {
            get => GetValue<Guid>();
            set => SetValue(value);
        }
        public int? CarCapacity
        {
            get => GetValue<int>();
            set => SetValue(value);
        }
        public int? EstimatedCapacity
        {
            get => CarCapacity - CoRiders.Count - 1;
        }
        public string? DriverName
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string? DriverSurname
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public ICollection<UserEntity> CoRiders { get => GetValue<ICollection<UserEntity>>(); }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(StartLocation))
            {
                yield return new ValidationResult($"{nameof(StartLocation)} is required", new[] { nameof(StartLocation) });
            }

            if (string.IsNullOrWhiteSpace(TargetLocation))
            {
                yield return new ValidationResult($"{nameof(TargetLocation)} is required", new[] { nameof(TargetLocation) });
            }

            if (BeginTime is null || DateTime.Now > BeginTime)
            {
                yield return new ValidationResult($"{nameof(BeginTime)} is required", new[] { nameof(BeginTime) });
            }

            if (CarId is null || CarId == Guid.Empty)
            {
                yield return new ValidationResult($"{nameof(CarId)} is required", new[] { nameof(CarId) });
            }
        }
        public static implicit operator JourneyWrapper(JourneyDetailModel detailModel)
            => new(detailModel);

        public static implicit operator JourneyDetailModel(JourneyWrapper wrapper)
            => wrapper.Model;
    }
}
