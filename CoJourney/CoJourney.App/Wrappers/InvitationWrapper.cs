using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CoJourney.BL.Models;

namespace CoJourney.App.Wrappers
{
    public class InvitationWrapper : ModelWrapper<InvitationDetailModel>
    {
        public InvitationWrapper(InvitationDetailModel model) : base(model)
        {
        }

        public static implicit operator InvitationWrapper(InvitationDetailModel detailModel)
            => new(detailModel);

        public static implicit operator InvitationDetailModel(InvitationWrapper wrapper)
            => wrapper.Model;
    }
}
