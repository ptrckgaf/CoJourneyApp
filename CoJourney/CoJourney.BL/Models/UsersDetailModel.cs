using System;
using AutoMapper;
using CoJourney.DAL.Entities;

namespace CoJourney.BL.Models
{
    public record UsersDetailModel(
        string Name,
        string Surname,
        string State) : ModelBase
    {
        public string Name { get; set; } = Name;
        public string Surname { get; set; } = Surname;
        public string? ImageUrl { get; set; }
        public string State { get; set; } = State;
        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<UserEntity, UsersDetailModel>()
                    .ReverseMap()
                    .ForMember(entity => entity.OwnedCars, expression => expression.Ignore())
                    .ForMember(entity => entity.DrivingJourneys, expression => expression.Ignore())
                    .ForMember(entity => entity.CoRidingJourneys, expression => expression.Ignore())
                    .ForMember(entity => entity.SentInvitations, expression => expression.Ignore())
                    .ForMember(entity => entity.ReceivedInvitations, expression => expression.Ignore());
            }
        }
    }
}