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
        public CheckBox checkbox { get; }
        public User user { get; }

        public AdminListItem(User u)
        {// constructor for the class
            this.checkbox = new CheckBox();
            this.user = u;            
        }
        
        

        public CheckBox CheckBox { get; }        
    }
}
