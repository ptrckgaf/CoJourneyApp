using System;
using CoJourney.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoJourney.DAL.Seeds;

public static class UserSeeds
{
    public static readonly UserEntity User1 = new(
        Id: Guid.Parse(input: "9510f6bd-a86b-42f0-96f7-ce21eedb87aa"),
        Name: "Vaso",
        Surname: "Patejdl",
        ImageUrl: "https://www.phono.cz/vimage/1000x1000/data/image/zbozi/vaso-patejdl-lov-na-city.jpg",
        State: "Available"

    );

    public static readonly UserEntity User2 = new(
        Id: Guid.Parse(input: "7ba5b7d4-baa4-467e-8973-2ad14fccbc16"),
        Name: "Brano",
        Surname: "Mojsej",
        ImageUrl: "https://1884403144.rsc.cdn77.org/foto/brano-mojsej/Zml0LWluLzk3OHg5OTk5L2ZpbHRlcnM6cXVhbGl0eSg4NSk6bm9fdXBzY2FsZSgpL2ltZw/1848007.jpg?v=0&st=jK5FVq2tgUqCHWXPJN-dJ-nuqpwo1R9GsFARa9J4Jzs&ts=1600812000&e=0",
        State: "Available"

    );

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            User1,
            User2
        );
    }
}