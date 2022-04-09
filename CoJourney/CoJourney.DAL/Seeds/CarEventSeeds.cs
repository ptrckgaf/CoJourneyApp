using System;
using CoJourney.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoJourney.DAL.Seeds;

public static class CarEventSeeds
{
    public static readonly CarEventEntity Event1 = new(
        Id: Guid.Parse(input: "fae16bb7-5b84-4445-aea8-3f42218d52b2"),
        BeginTime: DateTime.Parse("26/4/2022 4:30 PM", System.Globalization.CultureInfo.InvariantCulture),
        EndTime: DateTime.Parse("26/4/2022 5:45 PM", System.Globalization.CultureInfo.InvariantCulture),
        Name: "Pouť na Velehrad",
        TargetLocation: "Velehrad"
    );

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarEventEntity>().HasData(
            Event1
        );
    }
}