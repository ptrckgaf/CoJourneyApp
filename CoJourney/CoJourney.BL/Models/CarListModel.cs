using System;
using System.Collections.Generic;
using AutoMapper;
using CoJourney.DAL.Entities;

namespace Cojourney.BL.Models
{
    public record CarListModel(
        string Producer,
        string ModelName,
        DateTime FirstRegistrationDate,
        int Capacity) : ModelBase
    {

        public string Producer { get; set; } = Producer;

        public string ModelName { get; set; } = ModelName;

        public DateTime FirstRegistrationDate { get; set } = FirstRegistrationDate;

        public int Capacity { get; set } = Capacity;

        public string? ImageURl { get; set; } 

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<CarEntity, CarListModel>();
            }
        }
    }
}