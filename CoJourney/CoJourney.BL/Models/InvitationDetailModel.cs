using System;
using System.Collections.Generic;
using AutoMapper;
using CoJourney.DAL.Entities;

namespace CoJourney.BL.Models
{
    public record InvitationDetailModel(
        Guid SenderUserId,
        string SenderUserName,
        string SenderUserSurname,
        Guid JourneyId,
        string JourneyStartLocation,
        string JourneyTargetLocation,
        DateTime JourneyBeginTime,
        bool? Accepted) : ModelBase
    {

        public Guid SenderUserId { get; set; } = SenderUserId;

        public string SenderUserName { get; set; } = SenderUserName;

        public string SenderUserSurname { get; set; } = SenderUserSurname;

        public Guid JourneyId { get; set; } = JourneyId;

        public string JourneyStartLocation { get; set; } = JourneyStartLocation;

        public string JourneyTargetLocation { get; set; } = JourneyTargetLocation;

        public DateTime JourneyBeginTime { get; set; } = JourneyBeginTime;

        public bool? Accepted { get; set; } = Accepted;

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<InvitationEntity, InvitationDetailModel>()
                    .ReverseMap()
                    .ForMember(entity => entity.SenderUser, expression => expression.Ignore())
                    .ForMember(entity => entity.ReceiverUser, expression => expression.Ignore())
                    .ForMember(entity => entity.Journey, expression => expression.Ignore())
                ;
            }
        }
    }
}