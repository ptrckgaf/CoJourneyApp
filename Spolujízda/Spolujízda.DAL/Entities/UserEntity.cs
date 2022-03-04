﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using CoDrive.DAL.Entities;

namespace coDrive.DAL.Entities
{
    public record UserEntity(
        Guid Id,
        string Name,
        string Surname,
        string? ImageUrl,
        string? State
    ) : IEntityPart
    {
        public ICollection<CarEntity> OwnedCars { get; init; } = new List<CarEntity>();
        public ICollection<JourneyEntity> DrivingJourneys { get; init; } = new List<JourneyEntity>();
        public ICollection<JourneyEntity> CoRidingJourneys { get; init; } = new List<JourneyEntity>();
        public ICollection<InvitationEntity> SendedInvitations { get; init; } = new List<InvitationEntity>();
        public ICollection<InvitationEntity> RecvievedInvitations { get; init; } = new List<InvitationEntity>();
    }
}
