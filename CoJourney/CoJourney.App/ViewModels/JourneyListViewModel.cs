using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoJourney.BL.Models;

namespace CoJourney.App.ViewModels
{
    public class JourneyListViewModel
    {
        public ObservableCollection<JourneyListModel> Journeys { get; }

        public JourneyListViewModel()
        {
            Journeys = new ObservableCollection<JourneyListModel>();
            Journeys.Add(new JourneyListModel("Brno","Vídeň", new DateTime(2022,5,5)));
            Journeys.Add(new JourneyListModel("Praha", "Brno", new DateTime(2022, 10, 2)));
            Journeys.Add(new JourneyListModel("Kroměříž", "Berlín", new DateTime(2022, 12, 24)));
        }
    }
}
