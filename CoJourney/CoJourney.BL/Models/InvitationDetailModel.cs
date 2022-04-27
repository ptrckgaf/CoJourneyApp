using System;
using System.Collections.Generic;
using AutoMapper;
using CoJourney.DAL.Entities;

namespace CoJourney.BL.Models
{
    public record InvitationDetailModel(
        Guid SenderUserId,
        string SenderName,
        string SenderSurname,
        Guid JourneyId,
        string JourneyStartLocation,
        string JourneyTargetLocation,
        DateTime JourneyBeginTime,
        bool? Accepted) : ModelBase
    {

        public Guid SenderUserId { get; set; } = SenderUserId;

        public string SenderName { get; set; } = SenderName;

        public string SenderSurname { get; set; } = SenderSurname;

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
                .ForMember(entity => entity.SenderUserId, expression => expression.Ignore())
                .ForMember(entity => entity.JourneyId, expression => expression.Ignore())
                .ForMember(entity => entity.SenderName, expression => expression.Ignore())
                .ForMember(entity => entity.SenderSurname, expression => expression.Ignore())
                .ForMember(entity => entity.JourneyStartLocation, expression => expression.Ignore())
                .ForMember(entity => entity.JourneyTargetLocation, expression => expression.Ignore())
                .ForMember(entity => entity.JourneyBeginTime, expression => expression.Ignore());
            }
        }
    }
}