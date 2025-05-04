using Model;
using ViewModel;
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
using LibreTranslate.Net;


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
            this.Loaded += EntrancePage_Loaded;
        }

        private void EntrancePage_Loaded(object sender, RoutedEventArgs e)
        {
            /*
            if (SignedInUser != null)
                TranslatePage(SignedInUser.language);
            else
                TranslatePage(new Language() {LanguageName= "Hebrew" });
            //*/
        }

        public void TranslatePage(Language To)
        {
            //var translator = new LibreTranslate();





            //if (To == null)
            //    return;
            //switch (To.LanguageName)
            //{
            //    case "English":
            //        this.UsernameTXT.Text = "Username";
            //        this.PasswordTXT.Text = "Password";
            //        this.LogInButton.Content = "Log in";
            //        this.SignUpButton.Content = "Don't have an account? create one here.";
            //        this.ErrorLoggingInText.Text = "";
            //        break;
            //    case "Hebrew":
            //        this.UsernameTXT.Text = "שם משתמש";
            //        this.PasswordTXT.Text = "סיסמא";
            //        this.LogInButton.Content = "התחבר";
            //        this.SignUpButton.Content = "אין משתמש? צור משתמש כאן.";
            //        this.ErrorLoggingInText.Text = "";
            //        break;
            //    case "German":
            //        this.UsernameTXT.Text = "Benutzername";
            //        this.PasswordTXT.Text = "Passwort";
            //        this.LogInButton.Content = "Einloggen";
            //        this.SignUpButton.Content = "Sie haben noch kein Konto? Erstellen Sie hier eins.";
            //        this.ErrorLoggingInText.Text = "";
            //        break;                
            //}
                  
        }
        private void GoToSettings(object sender, MouseButtonEventArgs e)
        {
            // to be removed
            NavigationService nv = NavigationService.GetNavigationService(this);
            nv.Navigate(new SettingsPage(this));
            //
        }
        //*/

        private async void LogIn(object sender, RoutedEventArgs e)
        {
            User GivenUser = new User() { Password = PasswordBox.Text, UserName = UsernameBox.Text };

            Apiservice APIservice = new();
            List<User> users = await APIservice.GetAllUsers();

            User potentialUser = users.Find(x => x.UserName == GivenUser.UserName && x.Password == GivenUser.Password);

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
                    //NavigationService nv = NavigationService.GetNavigationService(this);
                    //nv.Navigate(new MainPage(this));

                    NavigationService nv = NavigationService.GetNavigationService(this);
                    nv.Navigate(new FriendsPage());
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
