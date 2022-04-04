using System;
using CoDrive.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoDrive.DAL.Seeds;

public static class CarEventSeeds
{
    public static readonly CarEventEntity Event1 = new(
        Id: Guid.Parse(input: "668c5c234f6a944d7572d4493d5b101f"),
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