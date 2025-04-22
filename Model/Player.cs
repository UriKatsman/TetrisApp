using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Player : User
    {
        public int TetrisHighScore { get; set; }
        public int TetrisCurrentScore { get; set; }


        public override string ToString()
        {
            return $"id: {this.Id}, highscore: {this.TetrisHighScore}, current score: {this.TetrisCurrentScore}";
        }
    }
}
