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
    public record JourneyDetailModel(
        string StartLocation,
        string TargetLocation,
        DateTime BeginTime,
        Guid DriverId,
        Guid CarId) : ModelBase
    {
        public string StartLocation { get; set; } = StartLocation;

        public string TargetLocation { get; set; } = TargetLocation;

        public DateTime BeginTime { get; set; } = BeginTime;

        public Guid CarId { get; set; } = CarId;
        public  int CarCapacity { get; set; }
        public Guid DriverId { get; set; } = DriverId;
        public string DriverName { get; set; }
        public string DriverSurname { get; set; }
        public ICollection<UserEntity> CoRiders { get; init; } = new List<UserEntity>();
        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<JourneyEntity, JourneyDetailModel>()
                    .ReverseMap()
                    .ForMember(entity => entity.Car, expression => expression.Ignore())
                    .ForMember(entity => entity.Driver, expression => expression.Ignore());
            }
        }

        public static readonly JourneyDetailModel Empty = new(default, default, DateTime.Now, default, default);
    }
}
