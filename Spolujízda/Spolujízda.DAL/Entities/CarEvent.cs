using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoDrive.DAL.Entities
{
    public class CarEvent : IEntityPart
    {
        public Guid Id { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Name { get; set; }
        public string TargetLocation { get; set; }
        
        public ICollection<Journey> Journeys { get; set; }
    }
}
