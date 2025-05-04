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
        public FriendUserControl(Player p, string username, int HighScore)
        {
            InitializeComponent();
            this.p = p;
            this.FriendNameTXT.Text = username;
            this.FriendScoreTXT.Text = HighScore.ToString();
        }

        private void RemoveFriend(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
