using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoJourney.App.Wrappers;
using CoJourney.BL.Models;
namespace CoJourney.App.Wrappers
{
    public class UserWrapper : ModelWrapper<UsersDetailModel>
    {
        public UserWrapper(UsersDetailModel model): base(model)
        {
        }
        public string? Name
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public string? Surname
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public string? ImageUrl
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public string? State 
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                yield return new ValidationResult($"{nameof(Name)} is required", new[] { nameof(Name) });
            }

            if (string.IsNullOrWhiteSpace(Surname))
            {
                yield return new ValidationResult($"{nameof(Surname)} is required", new[] { nameof(Surname) });
            }
            
        }
        public static implicit operator UserWrapper(UsersDetailModel detailModel)
            => new(detailModel);

        public static implicit operator UsersDetailModel(UserWrapper wrapper)
            => wrapper.Model;
    }
}
