using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using coDrive.DAL.Entities;

namespace CoDrive.DAL.Entities
{
    public class Invitation : IEntityPart
    {
        public Guid Id { get; set; }
        public bool? Accepted { get; set; } //pokud je accepted null, nebylo na zadost odpovezeno
        
        public Guid SenderUeserId { get; set; }
        public User SenderUser { get; set; }

        public Guid RecieverId { get; set; }
        public User RecieverUser { get; set; }

        public Guid JourneyId { get; set; }
        public Journey Journey { get; set; }
    }
}
