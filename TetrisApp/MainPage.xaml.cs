﻿using MyService;
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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private Page PreviousPage;
        public MainPage()
        {
            InitializeComponent();
            SetPlayer();
        }
        public MainPage(Page prev)
        {
            InitializeComponent();
            this.PreviousPage = prev;
            SetPlayer();
        }
        private async void SetPlayer()
        {
            Apiservice api = new Apiservice();
            GamePage.currentPlayer = (await api.GetAllPlayers()).Find(x => x.Id == EntrancePage.SignedInUser.Id);
        }

        private void GoBack(object sender, MouseButtonEventArgs e)
        {
            NavigationService nv = NavigationService.GetNavigationService(this);
            nv.Navigate(this.PreviousPage);
        }

        private void GoToSettings(object sender, MouseButtonEventArgs e)
        {
            NavigationService nv = NavigationService.GetNavigationService(this);
            nv.Navigate(new SettingsPage(this));
        }

        private void PlayBtn(object sender, RoutedEventArgs e)
        {
            NavigationService nv = NavigationService.GetNavigationService(this);
            nv.Navigate(new GamePage(this));
        }
    }
}
