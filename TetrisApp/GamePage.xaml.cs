using Model;
using MyService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        //stores the bricks in use
        private List<BrickType> AllColors;

        private Apiservice api;
        private Page previous;
        public static Player currentPlayer;


        private int[,] DrawnBoard;
        private int[,] BackBoard;
        private int[,] BrickGrid;

        private double tick = 0;
        private bool GameOver;

        private GameBrick Brick;
        private int amountOfTicksPerIteration = 10;
        private bool isInitiated = false;

        public GamePage(Page previous)
        {
            InitializeComponent();
            this.api = new Apiservice();            
            this.previous = previous;
            this.GameOverScreen.Visibility = Visibility.Collapsed;
            this.GoBackBtn.IsEnabled = false;

            this.Loaded += GamePage_Loaded;
        }

        private void GamePage_Loaded(object sender, RoutedEventArgs e)
        {
            this.GameOver = false;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Start();
            TranslatePage(EntrancePage.SignedInUser.language);
        }
        public void TranslatePage(Language To)
        {
            if (To == null)
                return;
            switch (To.LanguageName)
            {
                case "English":
                    this.GameOverTXT.Text = "Game Over!";
                    this.FinalScore.Text = "Score: ";
                    this.GameOverBtnTxt.Text = "Back to Main Page";
                    this.ScoreTXT.Text = "Score: ";                    
                    break;
                case "Hebrew":
                    this.GameOverTXT.Text = "המשחק הסתיים!";
                    this.FinalScore.Text = "ניקוד: ";
                    this.GameOverBtnTxt.Text = "חזור לעמוד ראשי";
                    this.ScoreTXT.Text = "ניקוד: ";
                    break;
                case "German":
                    this.GameOverTXT.Text = "Spiel Endet";
                    this.FinalScore.Text = "Punktzahl: ";
                    this.GameOverBtnTxt.Text = "Zurück zur Startseite";
                    this.ScoreTXT.Text = "Punktzahl: ";
                    break;
            }
        }
        private async void initiateGame()
        {
            if (this.isInitiated)
                return;
            // finds the correct dimentions of the canvas/arrays
            double CanvasWidth = (double)this.MainCanvas.ActualWidth;
            double CanvasHeight = (double)this.MainCanvas.ActualHeight;
            double ratio = CanvasHeight / CanvasWidth;
            // width of the arrays
            int size = 10;
            // creation of the arrays
            this.DrawnBoard = new int[size, (int)(size * ratio + 0.5)];
            this.BackBoard = new int[size, (int)(size * ratio + 0.5)];
            // withdrawing the stored information about the board from the last game            
            this.ScoreTXT.Text += currentPlayer.TetrisCurrentScore.ToString();
            
            this.Brick = new GameBrick();
            CopyBrickGrid();
            this.Brick.posX = BricksGets.rng.Next(0, DrawnBoard.GetLength(0) - Brick.grid.GetLength(0));
            this.Brick.posY = DrawnBoard.GetLength(1) - Brick.grid.GetLength(1);
            amountOfTicksPerIteration = 3;

            this.GameOver = false;
            
            this.GoBackBtn.IsEnabled=false;
            UpdateScreen();
            this.isInitiated = true;
        }        

         
        private void CollapseRows()
        {
            int RowCount = 0;
            // top to bottom
            for (int i = this.BackBoard.GetLength(1) - 1; i >= 0; i--) 
            {
                // left to right
                for (int k = 0; k < this.BackBoard.GetLength(0); k++)
                {
                    if (this.BackBoard[k, i] == 0)
                        break;
                                       
                    if (k == this.BackBoard.GetLength(0) - 1)
                    {                        
                        RowCount++;
                        // bottom to top
                        for (int j = i; j < this.BackBoard.GetLength(1) - 1; j++)
                        {
                            // left to right
                            for (int l = 0; l < this.BackBoard.GetLength(0); l++)
                            {                                
                                this.BackBoard[l, j] = this.BackBoard[l, j + 1];                                
                            }
                        }
                    }
                }                
            }
            if (RowCount > 0)
            {
                currentPlayer.TetrisCurrentScore += RowsToScore(RowCount);
            }
        }
        private int RowsToScore(int rows)
        {
            return (10+2*(rows-1)) * rows;
        }
        private async void LossDetection()
        {            
            for (int i = 0; i < this.BackBoard.GetLength(0); i++)
            {
                if (this.BackBoard[i, this.BackBoard.GetLength(1) - 1] != 0)
                {
                    currentPlayer.TetrisHighScore = Math.Max(currentPlayer.TetrisHighScore, currentPlayer.TetrisCurrentScore);
                    this.FinalScore.Text += currentPlayer.TetrisCurrentScore;
                    currentPlayer.TetrisCurrentScore = 0;
                    await api.UpdatePlayer(currentPlayer);
                    this.GameOver = true;
                    
                    this.GameOverScreen.Visibility = Visibility.Visible;
                    this.GoBackBtn.IsEnabled = true;

                    return;
                }
            }            
        }
        private void CopyBrickGrid()
        {
            this.BrickGrid = new int[this.Brick.grid.GetLength(0), this.Brick.grid.GetLength(1)];
            for (int i = 0; i < this.Brick.grid.GetLength(0); i++)
            {
                for (int k = 0; k < this.Brick.grid.GetLength(1); k++)
                {
                    if (this.Brick.grid[i, k] != 0)
                        this.BrickGrid[i, k] = this.Brick.ColorID;
                }
            }
        }
        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (this.GameOver)
                return;

            if (MainWindow.IsMoved == false)
            {
                this.tick += 1;
            }
            initiateGame();


            if (this.tick % amountOfTicksPerIteration == 0)
            {                                               
                if (detectCollision())
                { 
                    MoveBrickToBackground();
                    LossDetection();
                    CollapseRows();

                    this.Brick = new GameBrick();
                    CopyBrickGrid();
                    this.Brick.posX = BricksGets.rng.Next(0, DrawnBoard.GetLength(0) - Brick.grid.GetLength(0));
                    this.Brick.posY = DrawnBoard.GetLength(1) - Brick.grid.GetLength(1);
                }
                else
                    this.Brick.posY--;
                this.tick = 0;
            }
            
            if (MainWindow.IsMoved == false)
            {
                UpdateScreen();
            }
            MainWindow.IsMoved = false;
        }
        
        private void UpdateScreen()
        {
            BrickBackgroundGridsMerge();
            DrawBoard(this.MainCanvas, this, this.DrawnBoard);
            if(DownButton.IsMouseOver == true)
            {
                int y;
                for (y = this.Brick.posY; y >= -2 && !CheckForMoveCollision(this.Brick.brick, this.Brick.posX, y); y--) ;
                if (y != this.Brick.posY)
                    y++;    

                double BoardWidth = (double)this.MainCanvas.ActualWidth;
                double BoardHeight = (double)this.MainCanvas.ActualHeight;

                double tileWidth = BoardWidth / this.DrawnBoard.GetLength(0);
                double tileHeight = BoardHeight / this.DrawnBoard.GetLength(1);
                for (int row = this.Brick.grid.GetLength(1) - 1 + y; row >= y; row--) 
                {
                    for (int col = this.BrickGrid.GetLength(0) - 1 + this.Brick.posX; col >= this.Brick.posX; col--) 
                    {
                        if (this.BrickGrid[col - this.Brick.posX, row - y] != 0)
                        {
                            if (this.DrawnBoard[col, row] == 0)
                            {
                                Canvas TileToDraw = new Canvas();
                                TileToDraw.Background = ColorToBright(intToBrush(this.Brick.ColorID));
                                TileToDraw.Height = tileHeight;
                                TileToDraw.Width = tileWidth;

                                Canvas.SetTop(TileToDraw, BoardHeight - tileHeight - row * tileHeight);
                                Canvas.SetLeft(TileToDraw, col * tileWidth);

                                this.MainCanvas.Children.Add(TileToDraw);
                            }
                        }                    
                    }
                }
            }            
        }
        private bool detectCollision()
        {
            for (int i = 0; i < this.BrickGrid.GetLength(0); i++)
            {
                for (int k = 0; k < this.BrickGrid.GetLength(1); k++)
                {
                    if (this.BrickGrid[i, k] != 0)
                    {
                        if (Brick.posY + k == 0)
                            return true;
                        if (BackBoard[i + Brick.posX, k + Brick.posY - 1] != 0)
                            return true;                        
                    }
                }
            }

            return false;
        }
        private bool CheckForMoveCollision(FallingBrick Brick, int NewX, int NewY)
        {
            for (int i = 0; i < Brick.grid.GetLength(0); i++)
            {
                for (int k = 0; k < Brick.grid.GetLength(1); k++)
                {
                    if (Brick.grid[i, k] != 0)
                    {
                        // bottom of the screen
                        if (NewY + k < 0)
                            return true;
                        // right of the screen
                        if (i + NewX >= this.BackBoard.GetLength(0))
                            return true;
                        // left of the screen
                        if (i + NewX < 0)
                            return true;
                        // object
                        if (BackBoard[i + NewX, k + NewY] != 0)
                            return true;
                    }
                }
            }

            return false;
        }
        private void BrickBackgroundGridsMerge()
        {
            // updates the array responsible for what the player sees
            for (int i = 0; i < BackBoard.GetLength(0); i++)
            {
                for (int k = 0; k < BackBoard.GetLength(1); k++)
                {
                    DrawnBoard[i, k] = BackBoard[i, k];
                }
            }
            for (int i = 0; i < Brick.grid.GetLength(0); i++)
            {
                for (int k = 0; k < Brick.grid.GetLength(1) && i + Brick.posX < DrawnBoard.GetLength(0) && k + Brick.posY < DrawnBoard.GetLength(1); k++)
                {
                    if (this.BrickGrid[i, k] != 0)
                        DrawnBoard[i + Brick.posX, k + Brick.posY] = this.BrickGrid[i, k];
                }
            }
        }
        private void MoveBrickToBackground()
        {
            for (int i = 0; i < BrickGrid.GetLength(0); i++)
            {
                for (int k = 0; k < BrickGrid.GetLength(1); k++)
                {
                    if (this.BrickGrid[i, k] != 0)
                    {
                        BackBoard[i + Brick.posX, k + Brick.posY] = this.Brick.ColorID;                                            
                    }
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
                case 1: return Brushes.DarkBlue;//to be removed
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
            if (!CheckForMoveCollision(this.Brick.brick, this.Brick.posX - 1, this.Brick.posY))
                this.Brick.posX--;
        }

        private void RightBtn(object sender, MouseButtonEventArgs e)
        {
            if (!CheckForMoveCollision(this.Brick.brick, this.Brick.posX + 1, this.Brick.posY))
                this.Brick.posX++;
        }

        private void LRBtn(object sender, MouseButtonEventArgs e)
        {
            if (CheckForMoveCollision(this.Brick.brick.rotateLeft,this.Brick.posX,this.Brick.posY) == false)
                this.Brick.RotateLeft(this.BrickGrid, this.Brick.ColorID);            
        }

        private void RRBtn(object sender, MouseButtonEventArgs e)
        {
            if (CheckForMoveCollision(this.Brick.brick.rotateRight, this.Brick.posX, this.Brick.posY) == false)
                this.Brick.RotateRight(this.BrickGrid, this.Brick.ColorID);            
        }

        private void DownBtn(object sender, MouseButtonEventArgs e)
        {
            int y;
            for (y = this.Brick.posY; y >= -2 && !CheckForMoveCollision(this.Brick.brick, this.Brick.posX, y); y--) ;
            if (y != this.Brick.posY)
                y++;

            this.Brick.posY = y;
        }
        private static Brush ColorToBright(Brush Origin)
        {
            Color color = (Color)Origin.GetValue(SolidColorBrush.ColorProperty);
            color.R = (byte)((255 - color.R) / 2);
            color.G = (byte)((255 - color.G) / 2);
            color.B = (byte)((255 - color.B) / 2);
            color.R = (byte)(255 - color.R);
            color.G = (byte)(255 - color.G);
            color.B = (byte)(255 - color.B);
            return new SolidColorBrush(color);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.GameOverScreen.Opacity = 0;
            NavigationService nv = NavigationService.GetNavigationService(this);
            nv.Navigate(this.previous);
        }
    }
}
