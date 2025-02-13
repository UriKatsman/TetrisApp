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
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        private Page PreviousPage;
        public SettingsPage(Page PreviousPage)
        {
            InitializeComponent();

            Apiservice api = new();
            this.PreviousPage = PreviousPage;            
            SetContentForLangugesComboBox();
        }
        private async void SetContentForLangugesComboBox()
        {
            Apiservice api = new();
            this.LanguagesComboBox.Text = EntrancePage.SignedInUser.language.ToString();
            this.LanguagesComboBox.ItemsSource = await api.GetAllLanguages();
        }

        private void GoBack(object sender, MouseButtonEventArgs e)
        {
            SaveChanges();

            NavigationService nv = NavigationService.GetNavigationService(this);
            nv.Navigate(this.PreviousPage);
        }
        private async void SaveChanges()
        {
            Apiservice api = new();
            EntrancePage.SignedInUser.language = (Language)this.LanguagesComboBox.SelectedItem;

            await api.UpdateUser(EntrancePage.SignedInUser);
        }
    }
}
