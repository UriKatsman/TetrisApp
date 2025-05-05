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
    /// Interaction logic for FriendsPage.xaml
    /// </summary>
    public partial class FriendsPage : Page
    {
        private List<FriendUserControl> FriendControls;
        //private List<FriendUserControl> PendingRequestControls;

        private Player currentPlayer;

        public FriendsPage()
        {
            InitializeComponent();

            GetLists();
        }
        private async void GetLists()
        {            
            Apiservice api = new();
            currentPlayer = (await api.GetAllPlayers()).Find(x=> x.Id == EntrancePage.SignedInUser.Id);
            List <Friendship> Friendships = await api.GetAllFriendships();
            List<Friendship> Friends = Friendships.FindAll(x => (x.player1.Id == currentPlayer.Id || x.player2.Id == currentPlayer.Id) && x.isAccepted == true);
            this.FriendControls = new();
            foreach (Friendship f in Friends)
            {
                if (f.player1.Id == currentPlayer.Id)
                    this.FriendControls.Add(new(f.player2,f.player2.UserName,f.player2.TetrisHighScore,this));
                else
                    this.FriendControls.Add(new(f.player1, f.player1.UserName, f.player1.TetrisHighScore,this));
                //this.FriendControls.Last().Width = this.FriendsList.Width;                
            }
            this.FriendsList.ItemsSource = null;
            this.FriendsList.ItemsSource = this.FriendControls;
        }
        private void GoBack(object sender, MouseButtonEventArgs e)
        {

        }
        public void Del_Friend(FriendUserControl sender)
        {
            Player friend = (Player)sender.DataContext;
            
            removeFriend(friend);
            this.FriendControls.Remove(this.FriendControls.Find(x => x.p == friend));
            this.FriendsList.ItemsSource = null;
            this.FriendsList.ItemsSource = this.FriendControls;
        }
        private async void removeFriend(Player friend)
        {
            Apiservice api = new();

            List<Friendship> friendships = await api.GetAllFriendships();
            Friendship f = friendships.Find(x => 
            (x.player1.Id == friend.Id && x.player2.Id == currentPlayer.Id) || 
            (x.player2.Id == friend.Id && x.player1.Id == currentPlayer.Id));

            api.DeleteFriendship(f.Id);
          
        }        

        private void SendRequest(object sender, RoutedEventArgs e)
        {
            string FriendName = AddFriendTextBox.Text;
            Player PotentionalFriend = DoesFriendExist(FriendName).Result;
            if (PotentionalFriend != null)
            {                
                SendFriendRequest(PotentionalFriend);
            }
        }
        private async void SendFriendRequest(Player friend)
        {
            Friendship f = new Friendship() { player1 = currentPlayer, player2 = friend, isAccepted = false };
            Apiservice api = new();
            api.InsertFriendship(f);
        }


        private async Task<Player> DoesFriendExist(string username)
        {
            if (username == this.currentPlayer.UserName)
            {
                AddFriendTextBox.Text = "Can't send yourself a friend request";
                return null;
            }
            Apiservice api = new();
            List<Player> players = await api.GetAllPlayers();
            Player person = players.Find(x=> x.UserName == username);
            return person;
        }
        
    }
}
