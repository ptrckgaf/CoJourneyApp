using AutoMapper;
using CoJourney.BL.Models;
using CoJourney.DAL.Entities;
using CoJourney.DAL.UnitOfWork;

namespace CoJourney.BL.Facades;

public class UsersFacade : CRUDFacade<UserEntity, UsersListModel, UsersDetailModel>
{
    public UsersFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
    }
}