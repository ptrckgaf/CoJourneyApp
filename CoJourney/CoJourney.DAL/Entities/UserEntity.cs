using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using CoJourney.DAL.Entities;

namespace CoJourney.DAL.Entities
{
    public record UserEntity(
        Guid Id,
        string Name,
        string Surname,
        string? ImageUrl,
        string State
    ) : IEntityPart
    {
        public ICollection<CarEntity> OwnedCars { get; init; } = new List<CarEntity>();
        public ICollection<JourneyEntity> DrivingJourneys { get; init; } = new List<JourneyEntity>();
        public ICollection<JourneyEntity> CoRidingJourneys { get; init; } = new List<JourneyEntity>();
        public ICollection<InvitationEntity> SentInvitations { get; init; } = new List<InvitationEntity>();
        public ICollection<InvitationEntity> ReceivedInvitations { get; init; } = new List<InvitationEntity>();
    }
}
