using CoJourney.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoJourney.Common.Tests.Seeds;

public static class InvitationSeeds
{
    public static readonly InvitationEntity EmptyInvitation = new(
        Id: default,
        Accepted: default,
        SenderUserId:default,
        ReceiverUserId: default,
        JourneyId: default
       

    );

    public static readonly InvitationEntity Invit1 = new(
       Id: Guid.Parse(input: "F31B8F1C-ACE2-4914-9FEE-2E93C7A4A2C5"),
       Accepted: true,
       SenderUserId: UserSeeds.Felos.Id,
       ReceiverUserId: UserSeeds.Patejdl.Id,
       JourneyId: JourneySeeds.Journey1.Id
    );

    public static readonly InvitationEntity Invit2 = new(
        Id: Guid.Parse(input: "B18A3DFB-5E13-4DA5-8FB0-4E136E8BBF64"),
        Accepted: false,
        SenderUserId: UserSeeds.Patejdl.Id,
        ReceiverUserId: UserSeeds.Felos.Id,
        JourneyId: JourneySeeds.Journey2.Id
   );

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InvitationEntity>().HasData(
            Invit1
        );
    }
}