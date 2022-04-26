using CoJourney.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoJourney.Common.Tests.Seeds;

public static class JourneySeeds
{
   
    public static readonly JourneyEntity Journey1 = new(
        Id: Guid.Parse(input: "cc256811-01d9-4011-9d38-65c969a66374"),
        StartLocation: "Vyškov",
        TargetLocation: CarEventSeeds.Event1.TargetLocation,
        BeginTime: CarEventSeeds.Event1.BeginTime,
        DriverId: UserSeeds.Patejdl.Id,
        CarId: CarSeeds.Golf.Id
    );

    public static readonly JourneyEntity Journey2 = new(
       Id: Guid.Parse(input: "21081E2E-A5D5-4739-991B-B2FDF8DD7A45"),
       StartLocation: "Brno",
       TargetLocation: CarEventSeeds.Event2.TargetLocation,
       BeginTime: CarEventSeeds.Event2.BeginTime,
       DriverId: UserSeeds.Felos.Id,
       CarId: CarSeeds.Picaso.Id
   );

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<JourneyEntity>().HasData(
            Journey1
        );
    }
}