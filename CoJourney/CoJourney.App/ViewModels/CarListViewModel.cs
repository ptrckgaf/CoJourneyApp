using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoJourney.BL.Models;

namespace CoJourney.App.ViewModels
{
    public class CarListViewModel
    {
        public ObservableCollection<CarListModel> Cars { get; }

        public CarListViewModel()
        {
            Cars = new ObservableCollection<CarListModel>();
            Cars.Add(new CarListModel("Volkswagen","New Beatle", 5){ImageURl = "https://www.autorevue.cz/GetFile.aspx?id_file=474009238" });
            Cars.Add(new CarListModel("Škoda", "Octavia", 5));
            Cars.Add(new CarListModel("Volkswagen", "Touran", 7){ImageURl = "https://cdn-cz.volkswagen.at/media/Content_Model_Variants_Variant_Image_Component/6718-83047-83051-2315-image/dh-960-7836a0/72ec930f/1649230618/maraton.jpg" });
        }
    }
}
