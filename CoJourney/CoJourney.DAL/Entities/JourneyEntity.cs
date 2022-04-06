using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoJourney.DAL.Entities;

namespace CoJourney.DAL.Entities
{
    public record JourneyEntity(
        Guid Id,
        string StartLocation,
        string TargetLocation,
        DateTime BeginTime,

        Guid DriverId,
        Guid CarId
    ) : IEntityPart
    {
        public ICollection<InvitationEntity> Invitation { get; init; } = new List<InvitationEntity>();
        public ICollection<UserEntity> CoRiders { get; init; } = new List<UserEntity>();
        public UserEntity? Driver { get; init; }
        public CarEntity? Car { get; init; }
        public CarEventEntity? CarEvent { get; init; }
    }
}
