using AutoMapper;
using CoJourney.BL.Models;
using CoJourney.DAL.Entities;
using CoJourney.DAL.UnitOfWork;

namespace CoJourney.BL.Facades;

public class JourneyFacade : CRUDFacade<JourneyEntity, JourneyListModel, JourneyDetailModel>
{
    public JourneyFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
    }
}