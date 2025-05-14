using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Model;
using MyService;

namespace TetrisApp
{
    public class FallingBrick
    {
        public int[,] grid;
        public FallingBrick rotateRight;
        public FallingBrick rotateLeft;
        public int rotationAmount;

        public FallingBrick(int rotationAmount)
        {
            this.rotationAmount = rotationAmount;
        }
    }
    public class GameBrick
    {
        public FallingBrick brick;
        public int posX;
        public int posY;
        public int[,] grid;
        public int ColorID;
        public int rightBorderDistance;
        public int leftBorderDistance;

        public GameBrick(int x = 0, int y = 0, FallingBrick shape = null, int ChoosenColor = 0)
        {
            if (ChoosenColor == 0)
                this.PicksColorInRandom();
            else
                this.ColorID = ChoosenColor;
            this.posX = x;
            this.posY = y;
            if (shape == null)
                this.brick = BricksGets.allBricks[BricksGets.rng.Next(0, BricksGets.allBricks.Count)];
            else
                this.brick = shape;
            this.grid = this.brick.grid;            
        }
                
        public void RotateRight(int[,] GridToFill, int ColorID)
        {
            this.brick = this.brick.rotateRight;
            ReplaceGrid(GridToFill, ColorID);
        }
        public void RotateLeft(int[,] GridToFill, int ColorID)
        {
            this.brick = this.brick.rotateLeft;
            ReplaceGrid(GridToFill, ColorID);
        }
        private void ReplaceGrid(int[,] GridToFill, int ColorID)
        {
            for (int i = 0; i < this.grid.GetLength(0); i++)
            {
                for (int k = 0; k < this.grid.GetLength(1); k++)
                    if (this.brick.grid[i, k] != 0)
                        GridToFill[i, k] = ColorID;
                    else
                        GridToFill[i, k] = 0;
            }
        }

        private void PicksColorInRandom()
        {
            int ColorVariety = BricksGets.ColorIDs.Count;
            BrickType r = BricksGets.ColorIDs[BricksGets.rng.Next(0, ColorVariety)];
            this.ColorID = r.Id;
        }

    }
    public class BricksGets
    {
        private static bool Singleton = false;

        public static List<FallingBrick> allBricks;
        public static FallingBrick L;
        public static FallingBrick square;
        public static FallingBrick line;
        public static FallingBrick T;
        public static FallingBrick Z;
        public static FallingBrick backZ;
        public static FallingBrick spoon;
        public static FallingBrick backSpoon;
        public static FallingBrick plus;
        public static FallingBrick littleCube;

        //        <---Color IDs--->
        public static List<BrickType> ColorIDs;
        //        <---Color IDs--->
        //        <---Random Number Generator--->
        public static Random rng;
        //        <---Random Number Generator--->

        public BricksGets()
        {
            if (Singleton == true)
                return;
            Singleton = true;

            this.ColorPalletePicker();            
            allBricks = new();
            //                                               Left        middle       right
            //                                       top center bottom
            L = new FallingBrick(4) { grid = new int[,] { { 1, 0, 0 }, { 1, 0, 0 }, { 1, 1, 1 } } };
            FinalizeBrick(L);
            //square = new FallingBrick(0) { grid = new int[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } } };
            //FinalizeBrick(square);
            line = new FallingBrick(1) { grid = new int[,] { { 0,1,0 }, { 0, 1, 0 }, { 0,1,0 } } };
            FinalizeBrick(line);
            T = new FallingBrick(4) { grid = new int[,] { { 0, 0, 1 }, { 0, 1, 1 }, { 0, 0, 1 } } };
            FinalizeBrick(T);
            Z = new FallingBrick(2) { grid = new int[,] { { 0, 0, 1 }, { 0, 1, 1 }, { 0, 1, 0 } } };
            FinalizeBrick(Z);
            backZ = new FallingBrick(2) { grid = new int[,] { { 0, 1, 0 }, { 0, 1, 1 }, { 0, 0, 1 } } };
            FinalizeBrick(backZ);
            spoon = new FallingBrick(4) { grid = new int[,] { { 0, 1, 0 }, { 0, 1, 0 }, { 0, 1, 1 } } };
            FinalizeBrick(spoon);
            backSpoon = new FallingBrick(4) { grid = new int[,] { { 0, 1, 1 }, { 0, 1, 0 }, { 0, 1, 0 } } };
            FinalizeBrick(backSpoon);
            //plus = new FallingBrick(0) { grid = new int[,] { { 0, 1, 0 }, { 1, 1, 1 }, { 0, 1, 0 } } };
            //FinalizeBrick(plus);
            littleCube = new FallingBrick(0) { grid = new int[,] { { 1, 1 }, { 1, 1 }} };
            FinalizeBrick(littleCube);
        }
        private void FinalizeBrick(FallingBrick b)
        {
            b = CreateRotation(b);
            allBricks.Add(b);
        }        
        private async void ColorPalletePicker()
        {
            Apiservice api = new();
            ColorIDs = await api.GetAllBrickTypes();            
        }

        private static FallingBrick CreateRotation(FallingBrick This)
        {
            //                   Left        middle       right
            //            top center bottom            
            FallingBrick current = This;
            if (current.rotationAmount > 0)
            {                
                CreateRotateRight(current);
            }
            else
            {
                current.rotateRight = current;
                current.rotateLeft = current;
            }

            for (int i = 0; i < current.rotationAmount - 2; i++)
            {
                current = current.rotateRight;               
                CreateRotateRight(current);
            }

            if (current.rotationAmount > 0)
            {
                current = current.rotateRight;
                current.rotateRight = This;
                This.rotateLeft = current;
            }
            return This;
        }
        private static FallingBrick CreateRotateRight(FallingBrick brick)
        {
            //creates a new, blank grid
            int[,] board = new int[brick.grid.GetLength(0), brick.grid.GetLength(1)];
            int length = brick.grid.GetLength(0) - 1;
                        
            int[,] origin = brick.grid;
            ////copies the grid            

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
            if (length % 2 == 0)
            {
                board[length / 2, length / 2] = origin[length / 2, length / 2];
            }

            FallingBrick ans = new FallingBrick(brick.rotationAmount) { grid = board, rotateRight = null, rotateLeft = brick };
            brick.rotateRight = ans;
            return ans;
        }
    }
}
