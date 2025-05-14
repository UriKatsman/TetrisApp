using Model;
using MyService;
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
        public static User SignedInUser;
        public EntrancePage()
        {
            InitializeComponent();
        }
        
        private void GoToSettings(object sender, MouseButtonEventArgs e)
        {            
            NavigationService nv = NavigationService.GetNavigationService(this);
            nv.Navigate(new SettingsPage(this));
        }

        private async void LogIn(object sender, RoutedEventArgs e)
        {
            User GivenUser = new User() { Password = PasswordBox.Password, UserName = UsernameBox.Text };

            Apiservice APIservice = new();
            List<User> users = await APIservice.GetAllUsers();

            User potentialUser = users.Find(x => x.UserName == GivenUser.UserName 
            && x.Password == GivenUser.Password);

            if (potentialUser != null)
            {
                this.ErrorLoggingInText.Visibility = Visibility.Hidden;
                EntrancePage.SignedInUser = potentialUser;


                if ((await APIservice.GetAllAdmins()).Find(x => x.Id == potentialUser.Id) != null)
                {
                    NavigationService nv = NavigationService.GetNavigationService(this);
                    nv.Navigate(new AdminViewListPage(this));
                }
                else
                {
                    NavigationService nv = NavigationService.GetNavigationService(this);
                    nv.Navigate(new MainPage(this));

                    
                }
            }
            else
            {
                this.ErrorLoggingInText.Visibility = Visibility.Visible;
            }
        }

        private void SignUp(object sender, RoutedEventArgs e)
        {
            NavigationService nv = NavigationService.GetNavigationService(this);
            nv.Navigate(new SignUpPage(this));
        }
    }
}
