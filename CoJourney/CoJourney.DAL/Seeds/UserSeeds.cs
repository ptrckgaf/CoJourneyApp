using System;
using CoDrive.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoDrive.DAL.Seeds;

public static class UserSeeds
{
    public static readonly UserEntity User1 = new(
        Id: Guid.Parse(input: "acb6c5fd6cb168a8472e23daf4580ada"),
        Name: "Vaso",
        Surname: "Patejdl",
        ImageUrl: "https://www.phono.cz/vimage/1000x1000/data/image/zbozi/vaso-patejdl-lov-na-city.jpg",
        State: "Available"

    );

    public static readonly UserEntity User2 = new(
        Id: Guid.Parse(input: "32f84d2744ad47e66368314beb27f3bd"),
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