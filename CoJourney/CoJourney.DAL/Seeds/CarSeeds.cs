using System;
using CoJourney.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoJourney.DAL.Seeds;

public static class CarSeeds
{
    public static readonly CarEntity Car1 = new(
        Id: Guid.Parse(input: "9285f8e0bdf365069a281aef9a9eaa4f"),
        Producer: "Fiat",
        ModelName: "Multipla",
        FirstRegistrationDate: DateTime.Parse("5/1/2008 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture),
        ImageUrl: "https://upload.wikimedia.org/wikipedia/commons/c/c3/Fiat_Multipla_%282002%29_%2829392161886%29.jpg",
        Capacity: 5,
        OwnerId: UserSeeds.User1.Id

    );

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarEntity>().HasData(
            Car1
        );
    }
}