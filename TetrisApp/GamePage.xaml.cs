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
        private int[,] board;
        
        private Page previous;
        public static Player currentPlayer;
        public static bool isGaming = false;
        public GamePage(Page previous)
        {
            InitializeComponent();
            isGaming = true;
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
                    board[brick.Col, brick.Row] = brick.brickType.Id;
            }
        }
        //---------------                

        private double tick = 0;
        private void Timer_Tick(object? sender, EventArgs e)
        {
            this.tick += 1;

            if (tick == 1)
            {
                double CanvasWidth = (double)this.MainCanvas.ActualWidth;
                double CanvasHeight = (double)this.MainCanvas.ActualHeight;
                //double CanvasWidth = 266+2/3;
                //double CanvasHeight = 300;
                double ratio = CanvasHeight / CanvasWidth;
                int size = 20;
                this.board = new int[size, (int)(size * ratio + 0.5)];
                GetBoard();

                for (int i = 0; i < Bricks.L.grid.GetLength(0); i++)
                {
                    for (int k = 0; i < Bricks.L.grid.GetLength(1); i++)
                    {
                        board[i, k] = Bricks.L.grid[i, k];
                    }
                }

                for (int i = 0; i < Bricks.L.grid.GetLength(0); i++)
                {
                    for (int k = 0; i < Bricks.L.grid.GetLength(1); i++)
                    {
                        board[i + 3, k] = Bricks.L.rotateRight.grid[i, k];
                    }
                }
                //*/
            }

//            this.board[LeftPos,1] = (new Random()).Next(24,32);

            if (!MainWindow.IsMoved)
                DrawBoard(this.MainCanvas, this, this.board);
            MainWindow.IsMoved = false;
            //*/
        }
        private static void DrawBoard(Canvas painting, Page p, int[,] board)
        {
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

        public static int LeftPos = 0;
        private void LeftBtn(object sender, MouseButtonEventArgs e)
        {
            this.board[LeftPos, 1] = 0;
            if (LeftPos > 0)
                LeftPos--;
        }

        private void RightBtn(object sender, MouseButtonEventArgs e)
        {
            this.board[LeftPos, 1] = 0;
            if (LeftPos < this.board.GetLength(0)-1)
                LeftPos++;
        }
    }
}
