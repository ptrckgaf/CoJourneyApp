using CoJourney.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoJourney.Common.Tests.Seeds;

public static class UserSeeds
{
    public static readonly UserEntity EmptyUser = new(
        Id: default,
        Name: default!,
        Surname: default!,
        ImageUrl: default!,
        State: default!

        );

    public static readonly UserEntity User = new(
        Id: Guid.Parse(input: "cb198142-5c35-4773-b62b-a11e98c50143"),
        Name: "Vaso",
        Surname: "Patejdl",
        ImageUrl: "https://www.phono.cz/vimage/1000x1000/data/image/zbozi/vaso-patejdl-lov-na-city.jpg",
        State: "Available"

    );

  
    //To ensure that no tests reuse these clones for non-idempotent operations
    public static readonly UserEntity UserUpdate = User with { Id = Guid.Parse("9c4cbbb3-e88f-420d-a931-650faeabf250") };
    public static readonly UserEntity UserDelete = User with { Id = Guid.Parse("0261f979-575c-4c4b-879c-3778b868bdfd") };

    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            UserUpdate,
            UserDelete);
    }
}