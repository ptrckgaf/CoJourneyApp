using System;
using AutoMapper;
using CoJourney.DAL.Entities;

namespace CoJourney.BL.Models
{
    public record UsersListModel(string Name, string Surname,string url) : ModelBase
    {
        public string Name { get; set; } = Name;
        public string Surname { get; set; } = Surname;
        public string url { get; set; } = url;
        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<UserEntity, UsersListModel>();
            }
        }
    }
}