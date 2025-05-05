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
    /// Interaction logic for FriendUserControl.xaml
    /// </summary>
    public partial class FriendUserControl : UserControl
    {
        public Player p;
        private Page page;
        public FriendUserControl(Player p, string username, int HighScore, Page page)
        {
            InitializeComponent();
            this.p = p;
            this.page = page;
            this.FriendNameTXT.Text = username;
            this.FriendScoreTXT.Text = HighScore.ToString();
            this.DataContext = p;            
            this.Xbtn.MouseDown += Xbtn_MouseDown;
        }

        private void Xbtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (page is FriendsPage)
            {
                FriendsPage fPage = page as FriendsPage;
                fPage.Del_Friend(this);
            }
        }
    }
}
