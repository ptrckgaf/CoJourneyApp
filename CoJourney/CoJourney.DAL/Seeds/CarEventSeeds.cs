using System;
using CoJourney.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoJourney.DAL.Seeds;

public static class CarEventSeeds
{
    public static readonly CarEventEntity Event1 = new(
        Id: Guid.Parse(input: "fae16bb7-5b84-4445-aea8-3f42218d52b2"),
        BeginTime: new DateTime(2022,5,26,16,30,0),
        EndTime: new DateTime(2022,5,26,17,45,0),
        Name: "Pouť na Velehrad",
        TargetLocation: "Velehrad",
        InstitutorId: UserSeeds.User1.Id
    );

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarEventEntity>().HasData(
            Event1
        );
    }
}