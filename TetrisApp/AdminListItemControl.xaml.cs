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
using Model;

namespace TetrisApp
{
    /// <summary>
    /// Interaction logic for AdminListItemControl.xaml
    /// </summary>
    public partial class AdminListItemControl : UserControl
    {
        public AdminListItemControl()
        {
            InitializeComponent();
        }
        public AdminListItemControl(AdminListItem ALI)
        {
            InitializeComponent();
            this.IsAdminCheckBox = ALI.CheckBox;
            this.userUserName.Text = ALI.User.UserName;
            this.userPassword.Text = ALI.User.Password;
        }
        public AdminListItemControl(User u)
        {
            InitializeComponent();
            AdminListItem ALI = new AdminListItem(u);
            this.IsAdminCheckBox = ALI.CheckBox;
            this.userUserName.Text = ALI.User.UserName;
            this.userPassword.Text = ALI.User.Password;
        }
    }
}
