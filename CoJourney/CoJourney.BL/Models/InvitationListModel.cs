using System;
using System.Collections.Generic;
using AutoMapper;
using CoJourney.DAL.Entities;

namespace CoJourney.BL.Models
{
    public record InvitationListModel(
        Guid SenderUserId,
        Guid JourneyId,
        Guid ReceiverUserId,
        string SenderUserName,
        string SenderUserSurname,
        string ReceiverUserName,
        string ReceiverUserSurname,
        string JourneyTargetLocation,
        string JourneyStartLocation,
        bool? Accepted) : ModelBase
    {

        public Guid SenderUserId { get; set; } = SenderUserId;
        public Guid ReceiverUserId { get; set; } = ReceiverUserId;

        public Guid JourneyId { get; set; } = JourneyId;

        public bool? Accepted { get; set; } = Accepted;
        public string SenderUserName { get; set; } = SenderUserName;
        public string SenderUserSurname { get; set; }= SenderUserSurname;
        public string ReceiverUserName { get; set; } = ReceiverUserName;
        public string ReceiverUserSurname { get; set; } = ReceiverUserSurname;
        public string JourneyTargetLocation { get; set; } = JourneyTargetLocation;
        public string JourneyStartLocation { get; set; } = JourneyStartLocation;
        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<InvitationEntity, InvitationListModel>()
                    .ReverseMap();
            }
        }
    }
}