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
using CoJourney.App.ViewModels;
using CoJourney.App.Views;

namespace CoJourney.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel mainViewModel, BL.Facades.UsersFacade userfacade, Services.IMediator mediator)
        {   
            InitializeComponent();
            DataContext = mainViewModel;
            var loginWindowViewModel = new LoginWindowViewModel(userfacade, mediator);
            LoginWindow loginWindow = new LoginWindow(userfacade, loginWindowViewModel);
            loginWindow.ShowDialog();
            if (! loginWindowViewModel.AllowStart)
            {
                this.Close();
            }
            else
            {Show(); }
            
        }
    }
}
