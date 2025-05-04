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
        public FriendsPage()
        {
            InitializeComponent();

            GetLists();
        }
        private async void GetLists()
        {
            Player p = EntrancePage.SignedInUser as Player;
            Apiservice api = new();
            List<Friendship> Friendships = await api.GetAllFriendships();
            List<Friendship> Friends = Friendships.FindAll(x => (x.player1.Id == p.Id || x.player2.Id == p.Id) && x.isAccepted == true);
            this.FriendControls = new();
            foreach (Friendship f in Friends)
            {
                if (f.player1.Id == p.Id)
                    this.FriendControls.Add(new(f.player2,f.player2.UserName,f.player2.TetrisHighScore));
                else
                    this.FriendControls.Add(new(f.player1, f.player1.UserName, f.player1.TetrisHighScore));
            }
            this.FriendsList.ItemsSource = null;
            this.FriendsList.ItemsSource = this.FriendControls;
        }
        private void GoBack(object sender, MouseButtonEventArgs e)
        {

        }


    }
}
