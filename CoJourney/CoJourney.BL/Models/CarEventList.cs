using System;
using System.Collections.Generic;
using AutoMapper;
using CoJourney.DAL.Entities;

namespace CoJourney.BL.Models
{
    public record CarEventList(
        string Name,
        string TargetLocation,
        DateTime BeginTime,
        DateTime EndTime) : ModelBase
    {

        public string Name { get; set; } = Name;

        public string TargetLocation { get; set; } = TargetLocation;

        public DateTime BeginTime { get; set } = BeginTime;

        public DateTime EndTime { get; set } = EndTime;

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<CarEventEntity, CarEventList>();
            }
        }
    }
}