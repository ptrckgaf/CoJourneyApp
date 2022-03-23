using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoDrive.DAL.Entities
{
    public record CarEventEntity(
        Guid Id,
        DateTime BeginTime,
        DateTime EndTime,
        string Name,
        string TargetLocation
    ) : IEntityPart
    {
        public ICollection<JourneyEntity> Journeys { get; init; } = new List<JourneyEntity>();
    }
}
