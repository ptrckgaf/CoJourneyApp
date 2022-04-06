using System;
using AutoMapper;
using CoJourney.DAL.Entities;

namespace CoJourney.BL.Models
{
    public record UsersListModel(string Name, Guid ID) : ModelBase
    {
        public Guid ID { get; set; } = ID;
        public string Name { get; set; } = Name;
        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<UserEntity, UsersListModel>();
            }
        }
    }
}