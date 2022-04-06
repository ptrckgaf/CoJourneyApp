using System;
using System.Collections.Generic;
using AutoMapper;
using CoJourney.DAL.Entities;

namespace CoJourney.BL.Models
{
    public record JourneyListModel(
        string StartLocation,
        string TargetLocation,
        DateTime BeginTime) : ModelBase
    {

        public string StartLocation { get; set; } = StartLocation;

        public string TargetLocation { get; set; } = TargetLocation;

        public DateTime BeginTime { get; set; } = BeginTime;

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<JourneyEntity, JourneyListModel>();
            }
        }
    }
}