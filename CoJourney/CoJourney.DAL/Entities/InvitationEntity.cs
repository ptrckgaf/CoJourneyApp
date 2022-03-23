using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using coDrive.DAL.Entities;

namespace CoDrive.DAL.Entities
{
    public record InvitationEntity(
        Guid Id,
        bool? Accepted, //pokud je accepted null, nebylo na zadost odpovezeno

        Guid SenderUserId,
        Guid ReceiverId,
        Guid JourneyId
    ) : IEntityPart
    {
        public UserEntity? SenderUser { get; init; }
        public UserEntity? ReceiverUser { get; init; }
        public JourneyEntity? Journey { get; init; }
    }
}
