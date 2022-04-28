using AutoMapper;
using CoJourney.BL.Models;
using CoJourney.DAL.Entities;
using CoJourney.DAL.UnitOfWork;

namespace CoJourney.BL.Facades;

public class InvitationFacade : CRUDFacade<InvitationEntity, InvitationListModel, InvitationDetailModel>
{
    public InvitationFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
    }
}