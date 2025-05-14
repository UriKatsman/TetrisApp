using Model;
using MyService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for FriendsPage.xaml
    /// </summary>
    public partial class FriendsPage : Page
    {
        private bool isShowingFriends = true;
        private List<FriendUserControl> FriendControls;
        private List<FriendRequestUserControl> PendingRequestControls;

        private Page previous;

        private Player currentPlayer;

        public FriendsPage(Page previous)
        {
            InitializeComponent();
            this.FriendControls = new();
            this.PendingRequestControls = new();
            GetLists();
            this.previous = previous;
            this.Loaded += FriendsPage_Loaded;
        }

        private void FriendsPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (EntrancePage.SignedInUser != null)
                TranslatePage(EntrancePage.SignedInUser.language);
            else
                TranslatePage(new Language() { LanguageName = "Hebrew" });
        }
        private string Friends;
        private string FriendRequests;
        private string FriendRequestIsAlreadyPending;
        private string YouAreAlreadyFriendsWithThisPerson;
        private string CantSendYourselfARequest;
        private void TranslatePage(Language To)
        {
            if (To == null)
                return;
            switch (To.LanguageName)
            {
                case "English":
                    this.ModeTXT.Text = "Friends";
                    this.FriendRequests = "Friend Requests";
                    this.Friends = "Friends";
                    this.FriendRequestIsAlreadyPending = "Friend request is already pending";
                    this.YouAreAlreadyFriendsWithThisPerson = "You are friends already";
                    this.CantSendYourselfARequest = "Can't send yourself a friend request";
                    this.AddButtonTXT.Text = "Add Friend";
                    this.SwitchBtn.Content = "Switch";
                    this.SwitchBtn.FontSize = 10;
                    break;
                case "Hebrew":
                    this.ModeTXT.Text = "חברים";
                    this.FriendRequests = "בקשות חברות";
                    this.Friends = "חברים";
                    this.FriendRequestIsAlreadyPending = "בקשת החברות כבר ממתינה";
                    this.YouAreAlreadyFriendsWithThisPerson = "אתם כבר חברים";
                    this.CantSendYourselfARequest = "לא ניתן לשלוח לעצמך בקשת חברות";
                    this.AddButtonTXT.Text = "הוסף חבר";
                    this.SwitchBtn.Content = "החלף";
                    this.SwitchBtn.FontSize = 10;
                    break;
                case "German":
                    this.ModeTXT.Text = "Freunde";
                    this.FriendRequests = "Freundschaftsanfrage";
                    this.Friends = "Freunde";
                    this.FriendRequestIsAlreadyPending = "Freundschaftsanfrage steht bereit aus";
                    this.YouAreAlreadyFriendsWithThisPerson = "Ihr seid bereits befreundet";
                    this.CantSendYourselfARequest = "Du kannst dir selbst keine Freundschaftsanfrage " +
                        "schicken";
                    this.AddButtonTXT.Text = "Freund hinzufügen";
                    this.SwitchBtn.Content = "Wechseln";
                    this.SwitchBtn.FontSize = 7;
                    break;
            }
        }

        private async void GetLists()
        {            
            Apiservice api = new();
            LoadingSign ls = new();
            ls.Width = 100;
            ls.Height = 100;
            this.FriendsList.ItemsSource = new List<LoadingSign>() { ls };
            if (currentPlayer == null)
                currentPlayer = (await api.GetAllPlayers()).Find(x=> x.Id ==
                EntrancePage.SignedInUser.Id);
            List <Friendship> Friendships = await api.GetAllFriendships();

            double ListWidth = this.FriendsList.Width;
            if (this.isShowingFriends)
            {
                List<Friendship> Friends = Friendships.FindAll(x => (x.player1.Id == currentPlayer.Id 
                || x.player2.Id == currentPlayer.Id) && x.isAccepted == true);
                this.FriendControls = new();
                foreach (Friendship f in Friends)
                {
                    if (f.player1.Id == currentPlayer.Id)
                        this.FriendControls.Add(new(f.player2, f, this));
                    else
                        this.FriendControls.Add(new(f.player1, f, this));

                    this.FriendControls.Last().Width = ListWidth;
                }
                this.FriendsList.ItemsSource = null;
                this.FriendsList.ItemsSource = this.FriendControls;
            }
            else
            {
                List<Friendship> Friends = Friendships.FindAll(x => (x.player1.Id == currentPlayer.Id
                || x.player2.Id == currentPlayer.Id) && x.isAccepted == false);
                this.PendingRequestControls = new();
                foreach (Friendship f in Friends)
                {
                    if (f.player1.Id == currentPlayer.Id)
                        this.PendingRequestControls.Add(new(f.player2, f, this));
                    else
                        this.PendingRequestControls.Add(new(f.player1, f, this));
                    this.PendingRequestControls.Last().Width = ListWidth;
                }
                this.FriendsList.ItemsSource = null;
                this.FriendsList.ItemsSource = this.PendingRequestControls;
            }
            
        }
        private void GoBack(object sender, MouseButtonEventArgs e)
        {
            NavigationService nv = NavigationService.GetNavigationService(this);
            nv.Navigate(this.previous);
        }

        public void Deny_requst(FriendRequestUserControl sender)
        {
            Player friend = sender.p;

            Del_Requst(sender.friendship);
            this.PendingRequestControls.Remove(this.PendingRequestControls.Find(x => x.p.Id 
            == friend.Id));
            this.FriendsList.ItemsSource = null;
            this.FriendsList.ItemsSource = this.PendingRequestControls;
        }
        private async void Del_Requst(Friendship f)
        {
            Apiservice api = new();
            await api.DeleteFriendship(f.Id);            
        }

        public void Accpet_requst(FriendRequestUserControl sender)
        {
            Player friend = sender.p;

            Update_Requst(sender.friendship);
            this.PendingRequestControls.Remove(this.PendingRequestControls.Find(x => x.p.Id
            == friend.Id));            
            this.FriendsList.ItemsSource = null;
            this.FriendsList.ItemsSource = this.PendingRequestControls;
        }
        private async void Update_Requst(Friendship f)
        {
            Apiservice api = new();
            f.isAccepted = true;
            await api.UpdateFriendship(f);
        }






        public void Del_Friend(FriendUserControl sender)
        {
            // True = the friend is player 1
            bool FriendIsOne = true;
            Player friend;
            if (sender.friendship.player1.Id == this.currentPlayer.Id)
                FriendIsOne = false;
            
            if (FriendIsOne)
                friend = sender.friendship.player1;
            else
                friend = sender.friendship.player2;

            removeFriend(sender.friendship);
            if (FriendIsOne)
                this.FriendControls.Remove(this.FriendControls.Find(x => x.friendship.player1
                == friend));
            else
                this.FriendControls.Remove(this.FriendControls.Find(x => x.friendship.player2 
                == friend));
            this.FriendsList.ItemsSource = null;
            this.FriendsList.ItemsSource = this.FriendControls;
        }
        private async void removeFriend(Friendship f)
        {
            Apiservice api = new();
            api.DeleteFriendship(f.Id);          
        }        

        private void SendRequest(object sender, RoutedEventArgs e)
        {
            SendRequestAsync();
        }

        private async void SendRequestAsync()
        {
            string FriendName = AddFriendTextBox.Text;
            Player PotentionalFriend = await DoesFriendExist(FriendName);
            if (PotentionalFriend != null)
            {
                Friendship f = new Friendship() { player1 = currentPlayer, player2 
                    = PotentionalFriend, isAccepted = false };
                Apiservice api = new();
                await api.InsertFriendship(f);
            }
        }


        private async Task<Player> DoesFriendExist(string username)
        {
            // makes sure the player is not sending himself a friend request
            if (username == this.currentPlayer.UserName)
            {
                AddFriendTextBox.Text = this.CantSendYourselfARequest;
                return null;
            }
            
            Apiservice api = new();

            // makes sure the player is not sending a friend request to someone who already
            // is a friend or has a request            
            List<Friendship> AllFriendships = await api.GetAllFriendships();
            foreach (Friendship f in AllFriendships)
            {
                if (f.player1.UserName == username || f.player2.UserName == username)
                {
                    if (f.player1.UserName == EntrancePage.SignedInUser.UserName || 
                        f.player2.UserName == EntrancePage.SignedInUser.UserName)
                    {
                        if (f.isAccepted == false)
                            AddFriendTextBox.Text = this.FriendRequestIsAlreadyPending;
                        else
                            AddFriendTextBox.Text = this.YouAreAlreadyFriendsWithThisPerson;
                        return null;
                    }
                }
            }





            List<Player> players = new();
            players = await api.GetAllPlayers();



            Player person = players.Find(x=> x.UserName == username);
            return person;
        }

        private void ChangeView(object sender, RoutedEventArgs e)
        {
            this.isShowingFriends = !this.isShowingFriends;
            if (this.isShowingFriends)
                this.ModeTXT.Text = this.Friends;
            else
                this.ModeTXT.Text = this.FriendRequests;
            GetLists();
        }

        private void GoToSettings(object sender, MouseButtonEventArgs e)
        {
            NavigationService nv = NavigationService.GetNavigationService(this);
            nv.Navigate(new SettingsPage(this));
        }
    }
}