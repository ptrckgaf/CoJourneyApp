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
        public List<CarDetailModel> OwnedCars { get; init; } = new();
        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<UserEntity, UsersDetailModel>()
                    .ReverseMap()
                    .ForMember(entity => entity.SentInvitations, expression => expression.Ignore())
                    .ForMember(entity => entity.ReceivedInvitations, expression => expression.Ignore());
            }
        }

        public static UsersDetailModel Empty => new("Noname","Nosurname", "I DONT EXISTS");
    }
}