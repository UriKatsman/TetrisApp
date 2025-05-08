using Model;
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
    /// Interaction logic for FriendRequestUserControl.xaml
    /// </summary>
    public partial class FriendRequestUserControl : UserControl
    {
        public Friendship friendship;
        private Page page;
        public Player p;
        public FriendRequestUserControl(Player p, Friendship f, Page page)
        {
            InitializeComponent();
            this.p = p;
            this.friendship = f;
            this.PlayerName.Text = p.UserName;
            this.page = page;
        }

        
        private void DeclineClick(object sender, MouseButtonEventArgs e)
        {
            if (page is FriendsPage)
            {
                FriendsPage fPage = page as FriendsPage;
                fPage.Deny_requst(this);
            }
        }

        private void AcceptClick(object sender, MouseButtonEventArgs e)
        {
            if (page is FriendsPage)
            {
                FriendsPage fPage = page as FriendsPage;
                fPage.Accpet_requst(this);
            }
        }
    }
}
