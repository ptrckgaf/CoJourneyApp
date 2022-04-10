using CoJourney.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoJourney.Common.Tests.Seeds;

public static class CarSeeds
{
    public static readonly CarEntity EmptyCar = new(
        default,
        default,
        default,
        default,
        default,
        default,
        default
    );

    public static readonly CarEntity Golf = new(
            Id: Guid.Parse(input: "E8D17C13-2A52-4A14-B3D0-EDE2EA15681C"),
            Producer: "Volkswagen",
            ModelName: "Golf",
            FirstRegistrationDate: new DateTime(2001, 1, 11),
            ImageUrl: "https://www.autoweb.cz/wp-content/uploads/2017/12/vg4_per.jpg",
            Capacity: 5,
            OwnerId: UserSeeds.Felos.Id)
    {
        Owner = UserSeeds.Felos
    };

    public static readonly CarEntity Picaso = new(
        Id: Guid.Parse(input: "E8D17C13-2A52-4A14-B3D0-EDE2EA15681D"),
        Producer: "Citroen",
        ModelName: "C4 Grand Picasso",
        FirstRegistrationDate: new DateTime(2008, 4, 14),
        ImageUrl: "https://www.citroencl.cz/images/5-cars/2849-bg-1.jpg",
        Capacity: 7,
        OwnerId: UserSeeds.Felos.Id
    )
    {
        Owner = UserSeeds.Felos
    };

    public static readonly CarEntity Punto = new(
        Id: Guid.Parse(input: "E8D17C13-2A52-4A14-B3D0-EDE2EA15681E"),
        Producer: "Fiat",
        ModelName: "Punto",
        FirstRegistrationDate: new DateTime(1999, 3, 3),
        ImageUrl: "https://aaaautoeuimg.vshcdn.net/thumb/300078516_640x480x95.jpg?10355",
        Capacity: 5,
        OwnerId: UserSeeds.Patejdl.Id
    )
    {
        Owner = UserSeeds.Patejdl
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarEntity>().HasData(
            Golf,
            Punto,
            Picaso);
    }
}