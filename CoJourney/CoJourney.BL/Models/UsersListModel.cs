using System;
using AutoMapper;
using CoJourney.DAL.Entities;

namespace CoJourney.BL.Models
{
    public record UsersListModel(string Name, string Surname,string ImageUrl) : ModelBase
    {
        public string Name { get; set; } = Name;
        public string Surname { get; set; } = Surname;
        public string ImageUrl { get; set; } = ImageUrl;
        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<UserEntity, UsersListModel>();
            }
        }

        public static UsersListModel Empty = new("NoName", "NoSurname",
            "https://znakynaklavesnici.cz/wp-content/uploads/Nahled-otazniku.png");
    }
}