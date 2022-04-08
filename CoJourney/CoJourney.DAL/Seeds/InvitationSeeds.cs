using System;
using CoJourney.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoJourney.DAL.Seeds;

public static class InvitationSeeds
{
    public static readonly InvitationEntity Invitation1 = new(
        Id: Guid.Parse(input: "827312d8f2a5adc8288ead8c7a637d82"),
        Accepted: null,
        SenderUserId: UserSeeds.User1.Id,
        ReceiverId: UserSeeds.User2.Id,
        JourneyId: JourneySeeds.Journey1.Id
    );

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InvitationEntity>().HasData(
            Invitation1
        );
    }
}