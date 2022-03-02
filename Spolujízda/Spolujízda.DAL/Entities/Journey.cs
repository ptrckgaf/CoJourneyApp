using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using coDrive.DAL.Entities;

namespace CoDrive.DAL.Entities
{
    public class Journey : IEntityPart
    {
        public Guid Id { get; set; }
        public string StartLocation { get; set; }
        public string TargetLocation { get; set; }
        public DateTime BeginTime { get; set; }

        public User Driver { get; set; }
        public Guid DriverId { get; set; }
        
        public virtual ICollection<User> CoRiders { get; set; }
        
        public Guid CarId { get; set; }
        public Car Car { get; set; }

        public CarEvent CarEvent { get; set; }

        public ICollection<Invitation> Invations { get; set; }
    }
}
