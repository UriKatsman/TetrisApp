using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Friendship : Base
    {
        public Player player1 { get; set; }
        public Player player2 { get; set; }
        public bool isAccepted { get; set; }

        public override string ToString()
        {
            return this.player1 + " + " + this.player2 + " "+ this.isAccepted;
        }
    }
}
