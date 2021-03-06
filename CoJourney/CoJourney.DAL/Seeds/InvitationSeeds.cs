using System;
using CoJourney.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoJourney.DAL.Seeds;

public static class InvitationSeeds
{
    public static readonly InvitationEntity Invitation1 = new(
        Id: Guid.Parse(input: "92d5c8db-365f-47e4-b20b-d7c82dce6704"),
        Accepted: null,
        SenderUserId: UserSeeds.User1.Id,
        ReceiverUserId: UserSeeds.User2.Id,
        JourneyId: JourneySeeds.Journey1.Id
    );

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InvitationEntity>().HasData(
            Invitation1
        );
    }
}