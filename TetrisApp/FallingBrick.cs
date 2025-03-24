using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TetrisApp
{
    public class FallingBrick
    {
        public int[,] grid;
        public FallingBrick rotateRight;
        public FallingBrick rotateLeft;        
    }
    public class Bricks
    {
        private static bool Singleton = false;

        public static FallingBrick L ;
        public static FallingBrick square = new FallingBrick();


       
        public Bricks()
        {
            if (Singleton == true)
                return;
            Singleton = true;

            L = new FallingBrick() {grid = new int[,] { {1,2,3}, {8,9,4}, {7,6,5} } };


            L.rotateRight = RotateRight(L);
        }

        private static FallingBrick RotateRight(FallingBrick brick)
        {
            //creates a new, blank grid
            int[,] board = new int[brick.grid.GetLength(0), brick.grid.GetLength(1)];
            int length = brick.grid.GetLength(0) - 1;
            int[,] origin = brick.grid;
            ////copies the grid
            //for (int col = 0; col < brick.grid.GetLength(0); col++)
            //{
            //    for (int row = 0; row < brick.grid.GetLength(1); row++)
            //    {
            //        board[col, row] = brick.grid[col, row];
            //    }
            //}


            // rotates
            for (int layer = 0; layer < (length + 2)/ 2; layer++)
            {
                // top to right
                for (int i = layer; i < (length + 1) - layer; i++)
                {
                    board[length - i - layer, length - layer] = origin[layer, i + layer ];
                }
                // right to bottom
                for (int i = layer; i < (length + 1) - layer; i++)
                {
                    board[length - layer, length - i - layer] = origin[i + layer,length - layer];
                }
                // bottom to left
                for (int i = layer; i < (length + 1) - layer; i++)
                {
                    board[length - i - layer, layer] = origin[length - layer, length - i - layer];
                }
                // left to top
                for (int i = layer; i < (length + 1) - layer; i++)
                {
                    board[layer, layer + i] = origin[length - i - layer, layer];
                }
            }
            if (length % 2 == 1)
            {
                board[length / 2 + 1, length / 2 + 1] = origin[length / 2 + 1, length / 2 + 1];
            }

            FallingBrick ans = new FallingBrick() { grid = board, rotateRight = null, rotateLeft = brick };
            brick.rotateRight = ans;
            return ans;
        }
    }
}
