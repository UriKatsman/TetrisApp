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
            
            this.PreviousPage = PreviousPage;            
            SetContentForLangugesComboBox();
        }
        private async void SetContentForLangugesComboBox()
        {
            Apiservice api = new();    
            List<Language> languages = await api.GetAllLanguages();
            this.LanguagesComboBox.ItemsSource =languages;


            int index = 0;
            if (EntrancePage.SignedInUser == null)
            {
                index = 0;
            }
            else
                for (int i = this.LanguagesComboBox.Items.Count - 1; i >= 0; i--)
                {
                    if (EntrancePage.SignedInUser.language.Id == ((Language)this.LanguagesComboBox.Items[i]).Id)
                        index = i;
                }
            
            this.LanguagesComboBox.SelectedIndex = index;
        }

        private void GoBack(object sender, MouseButtonEventArgs e)
        {
            if (EntrancePage.SignedInUser != null)
                SaveChanges();
            NavigationService nv = NavigationService.GetNavigationService(this);

            if (this.PreviousPage is AdminViewListPage)
                ((AdminViewListPage)this.PreviousPage).UpdateTheListView();
            //nv.Navigate(new AdminViewListPage(this.PreviousPage));

            nv.Navigate(this.PreviousPage);
        }
        private async void SaveChanges()
        {
            Apiservice api = new();
            List<Language> l= await api.GetAllLanguages();
            if ((Language)this.LanguagesComboBox.SelectedItem != null)
                EntrancePage.SignedInUser.language = (Language)this.LanguagesComboBox.SelectedItem;

            await api.UpdateUser(EntrancePage.SignedInUser);
        }
    }
}
