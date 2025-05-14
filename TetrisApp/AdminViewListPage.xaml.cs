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
        public async void UpdateTheListView()
        {// מציב נתונים בListView
            
            Apiservice api = new();

            List<User> users = await api.GetAllUsers();
            List<Admin> admins = await api.GetAllAdmins();
            bool IsAdmin;
            List<AdminListItemControl> items = new List<AdminListItemControl>();

            foreach (User u in users)
            {                
                items.Add(new AdminListItemControl(u));

                items.Last().Width = this.UsersListBox.Width - 15;
                items.Last().HorizontalAlignment = HorizontalAlignment.Center;                

                IsAdmin = admins.Find(x => x.Id == items.Last().user.Id) != null;
                items.Last().AdminCheckBox.IsChecked = IsAdmin;                
            }

            this.UsersListBox.ItemsSource = items;                       
        }        

        public AdminViewListPage()
        {
            this.PreviousPage = new EntrancePage();
            InitializeComponent();            
            UpdateTheListView();
            this.Loaded += AdminViewListPage_Loaded;
        }
        private void TranslatePage(Language To)
        {
            if (To == null)
                return;
            switch (To.LanguageName)
            {
                case "English":
                    this.DeleteHeader.Header = "Ban";
                    this.UpdateHeader.Header = "Change";    
                    this.ApplyBtn.Content = "Apply";
                    break;
                case "Hebrew":
                    this.DeleteHeader.Header = "אסור";
                    this.UpdateHeader.Header = "עדכן";
                    this.ApplyBtn.Content = "שמור";
                    break;
                case "German":
                    this.DeleteHeader.Header = "verbannen";
                    this.UpdateHeader.Header = "ändern";
                    this.ApplyBtn.Content = "übertragen";
                    break;
            }
        }
        public AdminViewListPage(Page PreviousPage)
        {
            this.PreviousPage = PreviousPage;
            InitializeComponent();
            UpdateTheListView();
            this.Loaded += AdminViewListPage_Loaded;
        }

        private void AdminViewListPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.TranslatePage(EntrancePage.SignedInUser.language);
        }

        private void GoToSettings(object sender, MouseButtonEventArgs e)
        {
            NavigationService nv = NavigationService.GetNavigationService(this);
            nv.Navigate(new SettingsPage(this));
        }

        private void GoBack(object sender, MouseButtonEventArgs e)
        {
            NavigationService nv = NavigationService.GetNavigationService(this);
            nv.Navigate(this.PreviousPage);
        }        

        private async void ListViewDelete(object sender, RoutedEventArgs e)
        {
            User x = ((AdminListItemControl)UsersListBox.SelectedItem).user;            

            Apiservice api = new();

            await api.DeleteUser(x.Id);

            UsersListBox.ItemsSource = null;
            UpdateTheListView();
        }
        private User EditedUser;
        private void ListViewUpdate(object sender, RoutedEventArgs e)
        {
            this.EditedUser = ((AdminListItemControl)UsersListBox.SelectedItem).user;

            this.UserEditPanel.Visibility = Visibility.Visible;
            this.UsernameBox.Text = this.EditedUser.UserName;
            this.PasswordBox.Text = this.EditedUser.Password;
        }

        private async void ApplyClick(object sender, RoutedEventArgs e)
        {
            Apiservice api = new();

            User x = this.EditedUser;
            
            User NewUser = new User() { language = x.language, Password = this.PasswordBox.Text, Id = x.Id, UserName = this.UsernameBox.Text };

            await api.UpdateUser(NewUser);
            UpdateTheListView();

            this.UserEditPanel.Visibility = Visibility.Collapsed;
        }
    }
}
