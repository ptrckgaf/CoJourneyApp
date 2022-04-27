using System;
using System.Collections.Generic;
using AutoMapper;
using CoJourney.DAL.Entities;

namespace CoJourney.BL.Models
{
    public record InvitationListModel(
        Guid SenderUserId,
        Guid JourneyId,
        bool? Accepted) : ModelBase
    {

        public Guid SenderUserId { get; set; } = SenderUserId;

        public Guid JourneyId { get; set; } = JourneyId;

        public bool? Accepted { get; set; } = Accepted;

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<InvitationEntity, InvitationListModel>()
                .ForMember(entity => entity.JourneyId, expression => expression.Ignore())
                .ForMember(entity => entity.SenderUserId, expression => expression.Ignore());
            }
        }
    }
}