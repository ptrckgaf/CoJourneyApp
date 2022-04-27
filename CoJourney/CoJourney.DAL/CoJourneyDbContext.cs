using CoJourney.DAL.Entities;
using CoJourney.DAL.Seeds;
using Microsoft.EntityFrameworkCore;


namespace CoJourney.DAL
{
    public class CoJourneyDbContext : DbContext
    {
        private readonly bool _seedDemoData;

        public CoJourneyDbContext(DbContextOptions contextOptions, bool seedDemoData = false)
            : base(contextOptions)
        {
            _seedDemoData = seedDemoData;
        }

        public DbSet<CarEntity> Cars => Set<CarEntity>();
        public DbSet<CarEventEntity> Events => Set<CarEventEntity>();
        public DbSet<InvitationEntity> Invitations => Set<InvitationEntity>();
        public DbSet<JourneyEntity> Journeys => Set<JourneyEntity>();
        public DbSet<UserEntity> Users => Set<UserEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CarEntity>()
                .HasMany(i => i.Journeys)
                .WithOne(i => i.Car)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CarEventEntity>()
                .HasMany(i => i.Journeys)
                .WithOne(i => i.CarEvent)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserEntity>()
                .HasMany(i => i.OwnedCars)
                .WithOne(i => i.Owner)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserEntity>()
                .HasMany(i => i.DrivingJourneys)
                .WithOne(i => i.Driver)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserEntity>()
                .HasMany(i => i.CoRidingJourneys)
                .WithMany(i => i.CoRiders);

            modelBuilder.Entity<InvitationEntity>()
                .HasOne(s => s.SenderUser)
                .WithMany(g => g.SentInvitations)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<InvitationEntity>()
                .HasOne(s => s.ReceiverUser)
                .WithMany(g => g.ReceivedInvitations)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<InvitationEntity>()
                .HasOne(s => s.Journey)
                .WithMany(g => g.Invitation)
                .OnDelete(DeleteBehavior.Cascade);

            if (_seedDemoData)
            {
                CarSeeds.Seed(modelBuilder);
                CarEventSeeds.Seed(modelBuilder);
                InvitationSeeds.Seed(modelBuilder);
                JourneySeeds.Seed(modelBuilder);
                UserSeeds.Seed(modelBuilder);
            }
        }
    }
}