using System;
using AutoMapper;
using CoJourney.DAL.Entities;

namespace CoJourney.BL.Models
{
    public record UsersListModel(string Name) : ModelBase
    {

        public string Name { get; set; } = Name;

        public string? ImageUrl { get; set; }

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<UserEntity, UsersListModel>();
            }
        }
    }
}