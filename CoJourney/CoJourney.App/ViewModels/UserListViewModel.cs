using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoJourney.BL.Models;
namespace CoJourney.App.ViewModels
{
    public class UserListViewModel : ViewModelBase

    {
    public ObservableCollection<UsersListModel> Users { get; }

    public UserListViewModel()
    {
        Users = new ObservableCollection<UsersListModel>();
        Users.Add(new UsersListModel("Steve", "Jobs", "https://www.designmag.cz/foto/2011/10/apple-steve-jobs-0.jpg"));
        Users.Add(new UsersListModel("Bill", "Gates",
            "https://static01.nyt.com/images/2021/05/17/business/14altGates-print/merlin_183135423_1167fa8a-7940-427e-b690-68876010d286-superJumbo.jpg"));
        Users.Add(new UsersListModel("Elon", "Musk",""));
    }

    }
}
