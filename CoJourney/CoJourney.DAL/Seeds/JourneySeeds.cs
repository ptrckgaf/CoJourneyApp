using System;
using CoJourney.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoJourney.DAL.Seeds;

public static class JourneySeeds
{
    public static readonly JourneyEntity Journey1 = new(
        Id: Guid.Parse(input: "cc256811-01d9-4011-9d38-65c969a66374"),
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