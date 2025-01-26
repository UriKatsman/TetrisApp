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

namespace TetrisApp
{
    /// <summary>
    /// Interaction logic for SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private void GoBack(object sender, MouseButtonEventArgs e)
        {
            NavigationService nv = NavigationService.GetNavigationService(this);
            nv.Navigate(new EntrancePage());
        }
        private void GoToSettings(object sender, MouseButtonEventArgs e)
        {

        }

        private void SignUp(object sender, RoutedEventArgs e)
        {

        }

        private void ChooseImageBtn(object sender, RoutedEventArgs e)
        {

        }
    }
}
