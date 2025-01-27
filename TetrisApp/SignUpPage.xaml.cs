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
    /// Interaction logic for SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        private Language ChoosenLanguage;
        public SignUpPage()
        {
            InitializeComponent();

            SetContentForLangugesComboBox();
        }

        private async void SetContentForLangugesComboBox()
        {
            Apiservice api = new();
            LanguagesComboBox.ItemsSource = await api.GetAllLanguages();
        }

        private void GoBack(object sender, MouseButtonEventArgs e)
        {
            NavigationService nv = NavigationService.GetNavigationService(this);
            nv.Navigate(new EntrancePage());
        }
        private void GoToSettings(object sender, MouseButtonEventArgs e)
        {

        }

        private async void SignUp(object sender, RoutedEventArgs e)
        {            
            Apiservice APIservice = new();
            
            User GivenUser = new User() { Password = PasswordBox.Text, UserName = UsernameBox.Text, language =this.ChoosenLanguage, ProfilePicture=""};

            List<User> users = await APIservice.GetAllUsers();

            User potentialUser = users.Find(x => x.UserName == GivenUser.UserName);

            if (potentialUser != null)
            {
                this.ErrorSigningUpText.Visibility = Visibility.Visible;
            }
            else
            {
                this.ErrorSigningUpText.Visibility = Visibility.Hidden;
                APIservice.InsertUser(GivenUser);
                MessageBox.Show("Account added");
            }
        }

        private void ChooseImageBtn(object sender, RoutedEventArgs e)
        {

        }

        private async void LanguageChosen(object sender, SelectionChangedEventArgs e)
        {
            Apiservice api = new();
            List<Language> languages = await api.GetAllLanguages();

            this.ChoosenLanguage = languages[this.LanguagesComboBox.SelectedIndex];
        }
    }
}
