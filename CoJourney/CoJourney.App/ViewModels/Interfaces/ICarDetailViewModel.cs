using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoJourney.App.Wrappers;

namespace CoJourney.App.ViewModels
{
    public interface ICarDetailViewModel : IViewModel
    {
        public Guid? OwnerId { get; set; }
        public CarWrapper? Model { get; set; }
    }
}
