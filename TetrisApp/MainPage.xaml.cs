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
using Model;

namespace TetrisApp
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private Page PreviousPage;
        public MainPage()
        {
            InitializeComponent();
            SetPlayer();
            this.Loaded += MainPage_Loaded;
        }
        public MainPage(Page prev)
        {
            InitializeComponent();
            this.PreviousPage = prev;            
            SetPlayer();
            this.Loaded += MainPage_Loaded;
        }
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (EntrancePage.SignedInUser != null)
                TranslatePage(EntrancePage.SignedInUser.language);
            else
                TranslatePage(new Language() { LanguageName = "Hebrew" });
        }
        private void TranslatePage(Language To)
        {
            if (To == null)
                return;
            switch (To.LanguageName)
            {
                case "English":
                    this.greetingTXT.Text = "Hello ";
                    this.PlayButtenText.Text = "Play";
                    this.HighScoreTXT.Text = "Highest Score: ";
                    break;
                case "Hebrew":
                    this.greetingTXT.Text = "שלום";
                    this.PlayButtenText.Text = "שחק";
                    this.HighScoreTXT.Text = "ניקוד שיא:";
                    break;
                case "German":
                    this.greetingTXT.Text = "Hallo";
                    this.PlayButtenText.Text = "Spiel";
                    this.HighScoreTXT.Text = "Höchste Punktzahl: ";
                    break;
            }
        }
        private async void SetPlayer()
        {
            Apiservice api = new Apiservice();
            GamePage.currentPlayer = (await api.GetAllPlayers()).Find(x => x.Id == EntrancePage.SignedInUser.Id);
            this.HighScoreTXT.Text = "Highest Score: "+ GamePage.currentPlayer.TetrisHighScore.ToString();
        }

        private void GoBack(object sender, MouseButtonEventArgs e)
        {
            NavigationService nv = NavigationService.GetNavigationService(this);
            nv.Navigate(this.PreviousPage);
        }

        private void GoToSettings(object sender, MouseButtonEventArgs e)
        {
            NavigationService nv = NavigationService.GetNavigationService(this);
            nv.Navigate(new SettingsPage(this));
        }        

        private void PlayBtn(object sender, RoutedEventArgs e)
        {
            this.PlayButton.IsEnabled = false;
            NavigationService nv = NavigationService.GetNavigationService(this);
            nv.Navigate(new GamePage(this));
        }

        private void ToFriends(object sender, MouseButtonEventArgs e)
        {
            NavigationService nv = NavigationService.GetNavigationService(this);
            nv.Navigate(new FriendsPage(this));
        }
    }
}
