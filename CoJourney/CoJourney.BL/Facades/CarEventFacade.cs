using AutoMapper;
using CoJourney.BL.Models;
using CoJourney.DAL.Entities;
using CoJourney.DAL.UnitOfWork;

namespace CoJourney.BL.Facades;

public class CarEventFacade : CRUDFacade<CarEventEntity, CarEventListModel, CarEventDetailModel>
{
    public CarEventFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
    }
}