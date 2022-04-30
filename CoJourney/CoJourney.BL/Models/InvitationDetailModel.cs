using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using CoJourney.DAL.Entities;

namespace CoJourney.BL.Models
{
    public record InvitationDetailModel(
        Guid SenderUserId,
        Guid ReceiverUserId,
        Guid JourneyId,
        bool? Accepted) : ModelBase
    {

        public Guid SenderUserId { get; set; } = SenderUserId;
        public Guid ReceiverUserId { get; set; } = ReceiverUserId;

        public Guid JourneyId { get; set; } = JourneyId;

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