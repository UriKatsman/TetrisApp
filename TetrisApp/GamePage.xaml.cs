using Model;
using MyService;
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
using System.Windows.Threading;

namespace TetrisApp
{        
    public partial class GamePage : Page
    {        
        private int[,] DrawnBoard;
        private int[,] BackBoard;

        private Page previous;
        public static Player currentPlayer;
        public GamePage(Page previous)
        {
            InitializeComponent();
            this.previous = previous;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(0.1);
                      
            timer.Start();                        

        }              

        private async void GetBoard()
        {
            Apiservice api = new Apiservice();

            List<BoardComponents> allBricks = await api.GetAllBoardComponents();
            foreach (BoardComponents brick in allBricks)
            {
                if (brick.player.Id == currentPlayer.Id)
                    DrawnBoard[brick.Col, brick.Row] = brick.brickType.Id;
            }
        }
        //---------------                

        private double tick = 0;

        //    <----The Game Brick---->
        private GameBrick Brick;
        //    <----The Game Brick---->

        private void Timer_Tick(object? sender, EventArgs e)
        {
            this.tick += 1;

            if (tick == 1)
            {
                // finds the correct dimentions of the canvas/arrays
                double CanvasWidth = (double)this.MainCanvas.ActualWidth;
                double CanvasHeight = (double)this.MainCanvas.ActualHeight;                
                double ratio = CanvasHeight / CanvasWidth;
                // width of the arrays
                int size = 20;
                // creation of the arrays
                this.DrawnBoard = new int[size, (int)(size * ratio + 0.5)];
                this.BackBoard = new int[size, (int)(size * ratio + 0.5)];
                // withdrawing the stored information about the board from the last game
                GetBoard();

                // testing -> creates a random brick every game tick
                this.Brick = new GameBrick();
                this.Brick.posX = BricksGets.rng.Next(0, DrawnBoard.GetLength(0) - Brick.grid.GetLength(0));
                this.Brick.posY = DrawnBoard.GetLength(1) - Brick.grid.GetLength(1);
            }
            
                             
            // updates the array responsible for what the player sees
            BrickBackgroundGridsMerge();
            
            // makes sure to not update the screen while the screen is moved
            if (!MainWindow.IsMoved)
                // updates the screen's display
                DrawBoard(this.MainCanvas, this, this.DrawnBoard);
            MainWindow.IsMoved = false;
            //*/
        }
        private void BrickBackgroundGridsMerge()
        {// updates the array responsible for what the player sees
            for (int i = 0; i < BackBoard.GetLength(0); i++)
            {
                for (int k = 0; k < BackBoard.GetLength(1); k++)
                {
                    DrawnBoard[i, k] = BackBoard[i, k];
                }
            }
            for (int i = 0; i < Brick.grid.GetLength(0); i++)
            {
                for (int k = 0; k < Brick.grid.GetLength(1); k++)
                {
                    DrawnBoard[i + Brick.posX, k + Brick.posY] = Brick.grid[i, k];
                }
            }
        }
        private void MoveBrickToBackground()
        {
            for (int i = 0; i < Brick.grid.GetLength(0); i++)
            {
                for (int k = 0; k < Brick.grid.GetLength(1); k++)
                {
                    BackBoard[i + Brick.posX, k + Brick.posY] = Brick.grid[i, k];
                }
            }
        }
        private static void DrawBoard(Canvas painting, Page p, int[,] board)
        {
            painting.Children.Clear();
            
            double BoardWidth = (double)painting.ActualWidth;
            double BoardHeight = (double)painting.ActualHeight;

            double tileWidth = BoardWidth / board.GetLength(0);
            double tileHeight = BoardHeight / board.GetLength(1);

            for (int row = board.GetLength(1) - 1; row >= 0; row--)
            {
                for (int col = board.GetLength(0) - 1; col >= 0; col--) 
                {
                    Canvas TileToDraw = new Canvas();
                    TileToDraw.Background = intToBrush(board[col, row]);
                    TileToDraw.Height = tileHeight;
                    TileToDraw.Width = tileWidth;

                    Canvas.SetTop(TileToDraw, BoardHeight - tileHeight - row * tileHeight);
                    Canvas.SetLeft(TileToDraw, col * tileWidth);

                    painting.Children.Add(TileToDraw);
                }
            }
        }
        private static Brush intToBrush(int colorId)
        {
            switch (colorId)
            {
                case 24: return Brushes.Black;
                case 25: return Brushes.Red;
                case 26: return Brushes.Yellow;
                case 27: return Brushes.Green;
                case 28: return Brushes.Purple;
                case 29: return Brushes.Blue;
                case 30: return Brushes.Cyan;
                case 31: return Brushes.Pink;
                default: return Brushes.White;
            }
        }

        private void LeftBtn(object sender, MouseButtonEventArgs e)
        {
            if (this.Brick.posX > 0 - this.Brick.leftBorderDistance) 
                this.Brick.posX--;
        }

        private void RightBtn(object sender, MouseButtonEventArgs e)
        {
            if (this.Brick.posX < this.DrawnBoard.GetLength(0) - this.Brick.grid.GetLength(0) + this.Brick.rightBorderDistance) 
                this.Brick.posX++;
        }

        private void LRBtn(object sender, MouseButtonEventArgs e)
        {
            this.Brick.RotateLeft();
        }

        private void RRBtn(object sender, MouseButtonEventArgs e)
        {
            this.Brick.RotateRight();
        }
    }
}
