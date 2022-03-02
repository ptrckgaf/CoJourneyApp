using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using coDrive.DAL.Entities;

namespace CoDrive.DAL.Entities
{
    public class Car : IEntityPart
    {
        public Guid Id { get; set; }
        public string Producer { get; set; }
        public string ModelName { get; set; }
        public DateTime FirstRegistrationDate { get; set; }
        public string? ImageUrl { get; set; }
        public int Capacity { get; set; } 

        public ICollection<Journey> Journeys { get; set; }

        public Guid OwnerId { get; set; }
        public User Owner { get; set; }
        
    }
}
