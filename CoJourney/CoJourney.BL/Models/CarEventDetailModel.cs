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
    public record CarEventDetailModel(
        DateTime BeginTime,
        DateTime EndTime,
        string TargetLocation,
        string Name) : ModelBase
    {
        public DateTime BeginTime { get; set; } = BeginTime;

        public DateTime EndTime { get; set; } = EndTime;

        public string TargetLocation { get; set; } = TargetLocation;
        public string Name { get; set; } = Name;
        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<CarEventEntity, CarEventDetailModel>();
            }
        }
    }
}
