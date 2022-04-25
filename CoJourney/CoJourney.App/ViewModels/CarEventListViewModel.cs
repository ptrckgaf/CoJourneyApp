using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoJourney.BL.Models;

namespace CoJourney.App.ViewModels
{
    public class CarEventListViewModel
    {
        public ObservableCollection<CarEventListModel> CarEvents { get; }

        public CarEventListViewModel()
        {
            CarEvents = new ObservableCollection<CarEventListModel>();
            CarEvents.Add(new CarEventListModel("Pouť do Santiaga","Santiago", new DateTime(2022,8,10), new DateTime(2022,10,8)));
            CarEvents.Add(new CarEventListModel("Odvoz na Zábavu", "Polná", new DateTime(2022, 7, 22), new DateTime(2022, 7, 23)));
        }
    }
}
