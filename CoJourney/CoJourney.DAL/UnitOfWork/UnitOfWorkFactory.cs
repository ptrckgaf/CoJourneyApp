using System;
using Microsoft.EntityFrameworkCore;

namespace CoJourney.DAL.UnitOfWork;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IDbContextFactory<CoJourneyDbContext> _dbContextFactory;

    public UnitOfWorkFactory(IDbContextFactory<CoJourneyDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
    public IUnitOfWork Create() => new UnitOfWork(_dbContextFactory.CreateDbContext());
}