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
        public GamePage(Page previous)
        {
            InitializeComponent();
            this.previous = previous;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Start();

            double CanvasWidth = 266+2/3;
            double CanvasHeight = 300;
            double ratio = CanvasHeight / CanvasWidth;
            int size = 20;
            this.board = new int[size, (int)(size * ratio)];

            for (int row = board.GetLength(1) - 1; row >= 0; row--)
            {
                for (int col = board.GetLength(0) - 1; col >= 0; col--)
                {
                    board[col, row] = 8;
                }
            }
            board[0, 0] = 0;
            board[1, 0] = 1;
            board[0, 1] = 2;

            //this.PreviewKeyDown += GamePage_PreviewKeyDown;
            
        }

        
        //---------------
        public static int Left = 0;
        private void KeyPress(object sender, KeyEventArgs e)
        {
            MessageBox.Show(e.Key.ToString());
            switch (e.Key)
            {
                case Key.A: Left++; break;
                case Key.D: Left--; break;
            }
            Left %= this.board.GetLength(0);
        }

        private double tick = 0;
        private void Timer_Tick(object? sender, EventArgs e)
        {
            this.tick += 1;
            
            this.board[Left,2] = (new Random()).Next(0, 8);
            if (!MainWindow.IsMoved)
                DrawBoard(this.MainCanvas, this, this.board);
            MainWindow.IsMoved = false;
            //*/
        }
        private static void DrawBoard(Canvas painting, Page p, int[,] board)
        {
            int BoardWidth = (int)painting.ActualWidth;
            int BoardHeight = (int)painting.ActualHeight;

            int tileWidth = BoardWidth / board.GetLength(0);
            int tileHeight = BoardHeight / board.GetLength(1);

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
                case 0: return Brushes.Blue;
                case 1: return Brushes.Red;
                case 2: return Brushes.Green;
                case 3: return Brushes.Yellow;
                case 4: return Brushes.Orange;
                case 5: return Brushes.Pink;
                case 6: return Brushes.Purple;
                case 7: return Brushes.Cyan;
                default: return Brushes.Black;
            }
        }
        
    }
}
