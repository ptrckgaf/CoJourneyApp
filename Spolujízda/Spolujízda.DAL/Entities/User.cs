using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using CoDrive.DAL.Entities;

namespace coDrive.DAL.Entities
{
    public class User : IEntityPart
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? ImageUrl { get; set; }
        public string? State { get; set; }

        public ICollection<Car> OwnedCars { get; set; }
        public ICollection<Journey> DrivingJourneys { get; set; }
        public ICollection<Journey> CoRidingJourneys { get; set; }
        public ICollection<Invitation> SendedInvitations { get; set; }
        public virtual ICollection<Invitation> RecvievedInvitations { get; set; }
    }
}
