using System;
using CoJourney.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoJourney.DAL.Seeds;

public static class CarSeeds
{
    public static readonly CarEntity Car1 = new(
        Id: Guid.Parse(input: "90f01bb5-3ef7-4156-b9cf-120337f62e74"),
        Producer: "Fiat",
        ModelName: "Multipla",
        FirstRegistrationDate: DateTime.Parse("5/1/2008 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture),
        ImageUrl: "https://www.portalridice.cz/data/car-vehicle/photos/orig/236395642-5203586706327454-8589254344553112158-n.jpg",
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