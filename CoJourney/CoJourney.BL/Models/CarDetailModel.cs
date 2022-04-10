using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using CoJourney.DAL.Entities;


namespace CoJourney.BL.Models
{
    public record CarDetailModel(
        string Producer,
        string ModelName,
        DateTime FirstRegistrationDate,
        int Capacity
    ) : ModelBase
    {
        public string Producer { get; set; } = Producer;

        public string ModelName { get; set; } = ModelName;

        public DateTime FirstRegistrationDate { get; set; } = FirstRegistrationDate;

        public int Capacity { get; set; } = Capacity;
        public  Guid OwnerId { get; set; }

        public string? ImageURl { get; set; }

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<CarEntity, CarDetailModel>()
                    .ReverseMap()
                    .ForMember(entity => entity.Journeys, expression => expression.Ignore());
            }
        }
        public static CarDetailModel Empty => new(string.Empty, string.Empty, default, 0);
    }
}
