using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using coDrive.DAL.Entities;

namespace CoDrive.DAL.Entities
{
    public record CarEntity(
        Guid Id,
        string Producer,
        string ModelName,
        DateTime FirstRegistrationDate,
        string? ImageUrl,
        int Capacity,

        Guid OwnerId
    ) : IEntityPart
    {
        public UserEntity? Owner { get; init; }
        public ICollection<JourneyEntity> Journeys { get; init; } = new List<JourneyEntity>();
    }
}
