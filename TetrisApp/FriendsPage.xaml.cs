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

        private Player currentPlayer;

        public FriendsPage()
        {
            InitializeComponent();
            this.FriendControls = new();
            this.PendingRequestControls = new();
            GetLists();
        }
        private async void GetLists()
        {            
            Apiservice api = new();
            LoadingSign ls = new();
            ls.Width = 100;
            ls.Height = 100;
            this.FriendsList.ItemsSource = new List<LoadingSign>() { ls };
            if (currentPlayer == null)
                currentPlayer = (await api.GetAllPlayers()).Find(x=> x.Id == EntrancePage.SignedInUser.Id);
            List <Friendship> Friendships = await api.GetAllFriendships();

            double ListWidth = this.FriendsList.Width;
            if (this.isShowingFriends)
            {
                List<Friendship> Friends = Friendships.FindAll(x => (x.player1.Id == currentPlayer.Id || x.player2.Id == currentPlayer.Id) && x.isAccepted == true);
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
                List<Friendship> Friends = Friendships.FindAll(x => (x.player1.Id == currentPlayer.Id || x.player2.Id == currentPlayer.Id) && x.isAccepted == false);
                this.PendingRequestControls = new();
                foreach (Friendship f in Friends)
                {
                    if (f.player2.Id == currentPlayer.Id)
                        this.PendingRequestControls.Add(new(f.player2,f, this));

                    this.FriendControls.Last().Width = ListWidth;
                }
                this.FriendsList.ItemsSource = null;
                this.FriendsList.ItemsSource = this.PendingRequestControls;
            }
            
        }
        private void GoBack(object sender, MouseButtonEventArgs e)
        {

        }

        public void Deny_requst(FriendRequestUserControl sender)
        {
            Player friend = sender.p;

            Del_Requst(sender.friendship);
            this.PendingRequestControls.Remove(this.PendingRequestControls.Find(x => x.p.Id == friend.Id));
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
            this.PendingRequestControls.Remove(this.PendingRequestControls.Find(x => x.p.Id == friend.Id));            
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
                this.FriendControls.Remove(this.FriendControls.Find(x => x.friendship.player1 == friend));
            else
                this.FriendControls.Remove(this.FriendControls.Find(x => x.friendship.player2 == friend));
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
                Friendship f = new Friendship() { player1 = currentPlayer, player2 = PotentionalFriend, isAccepted = false };
                Apiservice api = new();
                await api.InsertFriendship(f);
            }
        }


        private async Task<Player> DoesFriendExist(string username)
        {

            // makes sure the player is not sending himself a friend request
            if (username == this.currentPlayer.UserName)
            {
                AddFriendTextBox.Text = "Can't send yourself a friend request";
                return null;
            }


            
            Apiservice api = new();


            // makes sure the player is not sending a friend request to someone who already is a friend or has a request            
            List<Friendship> AllFriendships = await api.GetAllFriendships();
            foreach (Friendship f in AllFriendships)
            {
                if (f.player1.UserName == username || f.player2.UserName == username)
                {
                    if (f.isAccepted == false)
                        AddFriendTextBox.Text = "Friend request is already pending";
                    else
                        AddFriendTextBox.Text = "You are already friended with this person";
                    return null;
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
                this.ModeTXT.Text = "Friends";
            else
                this.ModeTXT.Text = "Friend Requests";
            GetLists();
        }
    }
}
