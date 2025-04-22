using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BoardComponents : Base
    {
        public Player player { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public BrickType brickType { get; set; }
    }
}
