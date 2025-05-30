﻿using Model;
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
        public Friendship friendship;
        private Page page;
        public FriendUserControl(Player p, Friendship f, Page page)
        {
            InitializeComponent();
            this.page = page;
            this.FriendNameTXT.Text = p.UserName;
            this.FriendScoreTXT.Text = p.TetrisHighScore.ToString();
            this.friendship = f;
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
