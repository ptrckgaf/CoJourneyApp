using CoJourney.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoJourney.Common.Tests.Seeds;

public static class CarEventSeeds
{
    public static readonly UserEntity EmptyUser = new(
       Id: default,
       Name: default!,
       Surname: default!,
       ImageUrl: default!,
       State: default!

       );
    public static readonly CarEventEntity Event1 = new(
        Id: Guid.Parse(input: "83AFF835-1E38-40AB-9348-36E6BB5B40B9"),
        BeginTime: new DateTime(2022, 2, 1, 10, 00, 00),
        EndTime: new DateTime(2022,3,1,12,00,00),
        Name: "do Prahy",
        TargetLocation: "Praha"
    );

    public static readonly CarEventEntity Event2 = new(
        Id: Guid.Parse(input: "ABCAAC54-1E38-40AB-9348-36E66554ef77"),
        BeginTime: new DateTime(2022, 7, 13, 15, 10, 00),
        EndTime: new DateTime(2022, 7, 15, 23, 00, 00),
        Name: "K moři",
        TargetLocation: "Monaco"
    );


    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarEventEntity>().HasData(
            Event1,
            Event2
        );
    }
}