using Model;
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
    /// Interaction logic for EntrancePage.xaml
    /// </summary>
    public partial class EntrancePage : Page
    {
        public EntrancePage()
        {
            InitializeComponent();
        }

        private void GoToSettings(object sender, MouseButtonEventArgs e)
        {

        }

        private async Task LogIn(object sender, RoutedEventArgs e)
        {
            User GivenUser = new User() { Password = PasswordBox.Text, UserName = UsernameBox.Text };

            
        }

        private void SignUp(object sender, RoutedEventArgs e)
        {

        }
    }
}
