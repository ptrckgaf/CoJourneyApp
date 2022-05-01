using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CoJourney.App.Services;
using CoJourney.App.ViewModels;
using CoJourney.App.Views;
using CoJourney.BL.Facades;

namespace CoJourney.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel mainViewModel, UsersFacade userfacade, IMediator mediator)
        {   
            InitializeComponent();
            DataContext = mainViewModel;
            //TODO odkomentovat do finální verze 
            /*var loginWindowViewModel = new LoginWindowViewModel(userfacade, mediator);
            LoginWindow loginWindow = new LoginWindow(userfacade, loginWindowViewModel);
            loginWindow.ShowDialog();
            if (! loginWindowViewModel.AllowStart)
            {
                this.Close();
            }
            else
            {Show(); }*/
            
        }
    }
}
