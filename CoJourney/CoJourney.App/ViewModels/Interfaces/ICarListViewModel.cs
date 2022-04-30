using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoJourney.App.ViewModels
{
    public interface ICarListViewModel : IListViewModel
    {
        public Guid LoggedUser { get; set; }
    }
}
