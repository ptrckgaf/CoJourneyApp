using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoJourney.BL.Facades;
using CoJourney.DAL;
using CoJourney.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using AutoMapper.EquivalencyExpression;

namespace CoJourney.BL
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBLServices(this IServiceCollection services)
        {
            services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();
            services.AddSingleton<CarEventFacade>();
            services.AddSingleton<CarFacade>();
            services.AddSingleton<JourneyFacade>();
            services.AddSingleton<InvitationFacade>();
            services.AddSingleton<UsersFacade>();

            services.AddAutoMapper((serviceProvider, cfg) =>
            {
                cfg.AddCollectionMappers();

                var dbContextFactory = serviceProvider.GetRequiredService<IDbContextFactory<CoJourneyDbContext>>();
                using var dbContext = dbContextFactory.CreateDbContext();
                cfg.UseEntityFrameworkCoreModel<CoJourneyDbContext>(dbContext.Model);
            }, typeof(BusinessLogic).Assembly);
            return services;
        }
    }
}
