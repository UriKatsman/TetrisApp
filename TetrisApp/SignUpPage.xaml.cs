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
        private Page PreviousPage;
        private Language ChoosenLanguage;
        public SignUpPage()
        {
            this.PreviousPage = new EntrancePage();
            InitializeComponent();

            SetContentForLangugesComboBox();
        }
        public SignUpPage(Page PreviousPage)
        {
            this.PreviousPage = PreviousPage;
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
            nv.Navigate(this.PreviousPage);
        }
        private void GoToSettings(object sender, MouseButtonEventArgs e)
        {
            
        }

        private async void SignUp(object sender, RoutedEventArgs e)
        {            
            Apiservice api = new();

            //User GivenUser = new User() { Password = PasswordBox.Password, UserName = UsernameBox.Text, language =this.ChoosenLanguage, ProfilePicture=""};
            User GivenUser = new User() { Password = PasswordBox.Password, UserName = UsernameBox.Text, language = this.ChoosenLanguage};

            List<User> users = await api.GetAllUsers();

            User potentialUser = users.Find(x => x.UserName == GivenUser.UserName);

            if (potentialUser != null)
            {
                this.ErrorSigningUpText.Visibility = Visibility.Visible;
            }
            else
            {
                this.ErrorSigningUpText.Visibility = Visibility.Hidden;

                //await api.InsertPlayer(new Player() {TetrisCurrentScore=0,TetrisHighScore=0, Password = PasswordBox.Password, UserName = UsernameBox.Text, language = this.ChoosenLanguage, ProfilePicture = ""});
                
                await api.InsertPlayer(new Player() { TetrisCurrentScore = 0, TetrisHighScore = 0, Password = PasswordBox.Password, UserName = UsernameBox.Text, language = this.ChoosenLanguage});

                MessageBox.Show("Account added");
            }
        }        
        private async void LanguageChosen(object sender, SelectionChangedEventArgs e)
        {
            Apiservice api = new();
            List<Language> languages = await api.GetAllLanguages();

            this.ChoosenLanguage = languages[this.LanguagesComboBox.SelectedIndex];
        }
    }
}
