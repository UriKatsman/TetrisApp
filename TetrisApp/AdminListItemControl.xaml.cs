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
        public User user { get; }
        public CheckBox AdminCheckBox { get; }
        public AdminListItemControl()
        {
            InitializeComponent();
        }        
        public AdminListItemControl(User u)
        {
            InitializeComponent();
            this.user = u;
            AdminListItem ALI = new AdminListItem(u);            
            this.DataContext = ALI;
            this.AdminCheckBox = this.IsAdminCheckBox;
            this.userUserName.Text = ALI.user.UserName;
            this.userPassword.Text = ALI.user.Password;
            this.userLanguage.Text = ALI.user.language.LanguageName;
        }
    }
}
