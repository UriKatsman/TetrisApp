using Model;
using MyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TetrisApp
{
    public class AdminListItem
    {// class that represents a single row in the listview in the administrator screen
        private CheckBox checkbox;
        private User user;

        public AdminListItem(User u)
        {// constructor for the class
            this.checkbox = new CheckBox();
            this.user = u;
            CheckCheckbox();
        }
        
        private async void CheckCheckbox()
        { // checks the checkbox if needed
            Apiservice api = new();
            List<Admin> admins = await api.GetAllAdmins();

            bool IsAdmin = admins.Find(x => x.Id == this.user.Id) != null;

            this.checkbox.IsChecked = IsAdmin;
        }

        public CheckBox CheckBox { get; }
        public User User { get; }
    }
}
