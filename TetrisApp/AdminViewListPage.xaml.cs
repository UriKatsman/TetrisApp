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
    /// Interaction logic for AdminViewListPage.xaml
    /// </summary>
    public partial class AdminViewListPage : Page
    {
        private Page PreviousPage;
        private async void UpdateTheListView()
        {// מציב נתונים בListView

            Apiservice api = new();

            List<User> users = await api.GetAllUsers();
            List<Admin> admins = await api.GetAllAdmins();
            bool IsAdmin;
            List<AdminListItemControl> items = new List<AdminListItemControl>();

            foreach (User u in users)
            {                
                items.Add(new AdminListItemControl(u));

                items.Last().Width = 690;
                items.Last().HorizontalAlignment = HorizontalAlignment.Center;

                IsAdmin = admins.Find(x => x.Id == items.Last().user.Id) != null;
                items.Last().AdminCheckBox.IsChecked = IsAdmin;

                if (items.Last().AdminCheckBox.IsChecked == true) 
                    ;
            }            
            
            this.UsersListBox.ItemsSource = items;
            
            
            foreach (AdminListItemControl u in this.UsersListBox.ItemsSource)
            {                
                if (u.AdminCheckBox.IsChecked == true)
                    ;
            }


            //*/
        }        

        public AdminViewListPage()
        {
            this.PreviousPage = new EntrancePage();
            InitializeComponent();
            
            UpdateTheListView();
        }
        public AdminViewListPage(Page PreviousPage)
        {
            this.PreviousPage = PreviousPage;
            InitializeComponent();
            UpdateTheListView();
        }

        private void GoToSettings(object sender, MouseButtonEventArgs e)
        {

        }

        private void GoBack(object sender, MouseButtonEventArgs e)
        {
            NavigationService nv = NavigationService.GetNavigationService(this);
            nv.Navigate(this.PreviousPage);
        }        

        private void ListViewDelete(object sender, RoutedEventArgs e)
        {
            
            User x = (User)UsersListBox.SelectedItem;

            Apiservice api = new();

            api.DeleteUser(x.Id);

            UsersListBox.ItemsSource = null;
            UpdateTheListView();
        }

        private void ListViewUpdate(object sender, RoutedEventArgs e)
        {

        }
    }
}
