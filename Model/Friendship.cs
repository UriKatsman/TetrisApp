using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Friendship : Base
    {
        public Player player1;
        public Player player2;

        public override string ToString()
        {
            return this.player1 + " + " + this.player2;
        }
    }
}
