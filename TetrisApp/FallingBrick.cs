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

            L = new FallingBrick();

            L.rotateRight = RotateRight(L);
        }

        private static FallingBrick RotateRight(FallingBrick brick)
        {
            //creates a new, blank grid
            int[,] board = new int[brick.grid.GetLength(0), brick.grid.GetLength(1)];
            int length = brick.grid.GetLength(0);
            int temp;
            ////copies the grid
            //for (int col = 0; col < brick.grid.GetLength(0); col++)
            //{
            //    for (int row = 0; row < brick.grid.GetLength(1); row++)
            //    {
            //        board[col, row] = brick.grid[col, row];
            //    }
            //}

            // rotates
            for (int layer = 0; layer < length / 2; layer++)
            {
                int[] RowOrColumnBeingReplaced = new int[length - layer * 2];

                // fills in the array
                for (int i = 0; i < length - layer * 2; i++)
                {
                    RowOrColumnBeingReplaced[i] = brick.grid[length - layer, layer + i];
                }
                // overrides the grid 
                for (int i = 0; i < length - layer * 2; i++)
                {
                    board[length - layer, layer + i] = brick.grid[layer + i, layer];
                }


                // makes use of the array
                for (int i = 0; i < length - layer * 2; i++)
                {
                    temp = brick.grid[layer + i, length - layer];
                    //board[layer + i, length - layer] = RowOrColumnBeingReplaced[];
                }
                
            }
            

            FallingBrick ans = new FallingBrick() { grid = board, rotateRight = null, rotateLeft = brick };
            brick.rotateRight = ans;
            return null;
        }
    }
}
