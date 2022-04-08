using System;
using CoJourney.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoJourney.DAL.Seeds;

public static class JourneySeeds
{
    public static readonly JourneyEntity Journey1 = new(
        Id: Guid.Parse(input: "54417849196c18b16616616233c5f5c7"),
        StartLocation: "Brno",
        TargetLocation: CarEventSeeds.Event1.TargetLocation,
        BeginTime: CarEventSeeds.Event1.BeginTime,
        DriverId: UserSeeds.User1.Id,
        CarId: CarSeeds.Car1.Id
    );

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<JourneyEntity>().HasData(
            Journey1
        );
    }
}