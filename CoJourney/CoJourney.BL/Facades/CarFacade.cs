using AutoMapper;
using CoJourney.BL.Models;
using CoJourney.DAL.Entities;
using CoJourney.DAL.UnitOfWork;

namespace CoJourney.BL.Facades;

public class CarFacade : CRUDFacade<CarEntity, CarListModel, CarDetailModel>
{
    public CarFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
    }
}