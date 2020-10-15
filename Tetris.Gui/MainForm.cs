using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Tetris.Game;
using Timer = System.Windows.Forms.Timer;

namespace Tetris.Gui
{
    public partial class MainFrom : Form
    {

        #region Private Constants

        private const int deckWidth = 10;
        private const int deckHeight = 20;
        private const int tetrominoWidthHeightBlocks = 4;
        private const int spaceBetweenBlocks = 2;
        private const int spaceBetweenBlockBorderAndInnerRectangle = 1;
        private const int deckBorderWidth = 5;
        private const int vanishDelayInterval = 200;
        private const int verticalSpaceBetweenUiElements = 20;
        private const int horizontalSpaceBetweenUiElements = 50;
        private const int moveDownDelayInterval = 50;
        private const int moveLeftRightDelayInterval = 120;

        #endregion

        #region Private Variables

        private int blockWidthHeight = 30;

        private readonly Color vanishColor = Color.Orange;
        private readonly Color borderColor = Color.Black;
        private readonly Color hiddenColor = Color.White;
        private bool[,] deck;

        private Game.Tetris tetrisGame;

        private Bitmap gameDeckBitmap;
        private Bitmap nextTetrominoBitmap;
        private Bitmap holdTetrominoBitmap;
        private Bitmap headerBitmap;

        ManualResetEvent mre = new ManualResetEvent(false);
        private bool moveDown;
        private bool moveLeft;
        private bool moveRight;

        #endregion

        #region Ctor

        public MainFrom()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Methods

        #region Tetris Events

        private void TetrisGame_Score(object sender, ScoreEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    UpdateScores(e);
                }));
            }
            else
            {
                UpdateScores(e);
            }
        }

        private void TetrisGame_RowVanish(object sender, BlockEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    DrawVanishRows(e);
                }));
            }
            else
            {
                DrawVanishRows(e);
            }
        }

        private void TetrisGame_NextTetromino(object sender, NextTetrominoesEventArg e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    DrawNextTetromino(e);
                }));
            }
            else
            {
                DrawNextTetromino(e);
            }
        }

        private void TetrisGame_ChangedBlocks(object sender, BlockEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    DrawChangedBlocks(e);
                }));
            }
            else
            {
                DrawChangedBlocks(e);
            }
        }

        private void TetrisGame_GhostBlocks(object sender, BlockEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    DrawGhostBlocks(e);
                }));
            }
            else
            {
                DrawGhostBlocks(e);
            }
        }

        private void TetrisGame_GameOver(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    GameOver();
                }));
            }
            else
            {
                GameOver();
            }
        }

        private void TetrisGame_HoldTetromino(object sender, BlockEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    DrawHoldTetromino(e);
                }));
            }
            else
            {
                DrawHoldTetromino(e);
            }
        }

        #endregion

        #region Form Events

        private void MainFrom_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    moveDown = false;
                    break;

                case Keys.Left:
                    moveLeft = false;
                    break;

                case Keys.Right:
                    moveRight = false;
                    break;
            }
        }

        private void MainFrom_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    tetrisGame.Rotate();
                    return;

                case Keys.Down:
                    moveDown = true;
                    mre.Set();
                    break;

                case Keys.Left:
                    moveLeft = true;
                    mre.Set();
                    break;

                case Keys.Right:
                    moveRight = true;
                    mre.Set();
                    break;

                case Keys.P:
                    tetrisGame.Pause();
                    break;

                case Keys.R:
                    tetrisGame.Resume();
                    break;

                case Keys.Enter:
                    tetrisGame.Restart();
                    break;

                case Keys.Space:
                    tetrisGame.HardDrop();
                    break;

                case Keys.Escape:
                    Application.Exit();
                    break;

                case Keys.G:
                    tetrisGame.GhostBlocksVisible = !tetrisGame.GhostBlocksVisible;
                    break;

                case Keys.H:
                    tetrisGame.Hold();
                    break;

            }
        }

        private void MainFrom_Load(object sender, EventArgs e)
        {
            InitializeDrawingBitmaps();
            DecorateFormControls();
            InitializeTetrisGame();            
        }

        private void MainFrom_Deactivate(object sender, EventArgs e)
        {
            tetrisGame.Pause();
        }

        private void MainFrom_Activated(object sender, EventArgs e)
        {
            if (tetrisGame != null)
                tetrisGame.Resume();
        }

        private void MainFrom_FormClosing(object sender, FormClosingEventArgs e)
        {
            tetrisGame.Pause();
            if (MessageBox.Show(this, "Are you sure to quit game?", "Quit Game", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
                tetrisGame.Resume();
            }
        }

        private void MainFrom_Resize(object sender, EventArgs e)
        {
            DecorateFormControls();
        }

        #endregion

        #region Drawing Methods

        private void DrawNextTetromino(NextTetrominoesEventArg e)
        {
            using (var graphics = Graphics.FromImage(nextTetrominoBitmap))
            {
                graphics.Clear(BackColor);
                var y = 0;
                foreach (var tetro in e.NextTetrominoes)
                {
                    foreach (var block in tetro)
                    {
                        DrawSingleBlock(graphics, block.X, block.Y + y, GetColor(block.Status, BackColor), block.Status != BlockStatus.Hidden ? borderColor : hiddenColor);
                    }
                    y += tetrominoWidthHeightBlocks;
                }
            }
            nextTetrominoPicBox.Refresh();
        }

        private void DrawVanishRows(BlockEventArgs e)
        {
            using (var graphics = Graphics.FromImage(gameDeckBitmap))
            {
                foreach (var block in e.Blocks)
                {
                    DrawSingleBlock(graphics, block.X, block.Y, vanishColor, vanishColor);
                }
                gameDeckPicBox.Refresh();

                Thread.Sleep(vanishDelayInterval);

                foreach (var block in e.Blocks)
                {
                    DrawSingleBlock(graphics, block.X, block.Y, GetColor(block.Status, hiddenColor), borderColor);
                }
            }
            gameDeckPicBox.Refresh();
        }

        private void DrawGhostBlocks(BlockEventArgs e)
        {
            using (var graphics = Graphics.FromImage(gameDeckBitmap))
            {
                foreach (var block in e.Blocks.Where(s => s.Y >= 0))
                {
                    if (block.Status == BlockStatus.Hidden)
                    {
                        DrawGhostBlock(graphics, block.X, block.Y, BackColor);
                    }
                    else if (!deck[block.X, block.Y])
                    {
                        DrawGhostBlock(graphics, block.X, block.Y, borderColor);
                    }
                }
            }
            gameDeckPicBox.Refresh();
        }

        private void DrawChangedBlocks(BlockEventArgs e)
        {
            using (var graphics = Graphics.FromImage(gameDeckBitmap))
            {
                foreach (var block in e.Blocks.Where(s => s.Y >= 0))
                {
                    DrawSingleBlock(graphics, block.X, block.Y, GetColor(block.Status, hiddenColor), block.Status != BlockStatus.Hidden ? borderColor : hiddenColor);
                    deck[block.X, block.Y] = block.Status != BlockStatus.Hidden;
                }
            }
            gameDeckPicBox.Refresh();

            if (e.Blocks.All(s => s.Y >= 0)) return;

            using (var graphics = Graphics.FromImage(headerBitmap))
            {
                foreach (var block in e.Blocks.Where(s => s.Y < 0))
                {

                    DrawSingleBlock(graphics, block.X, tetrominoWidthHeightBlocks + block.Y, GetColor(block.Status, BackColor), block.Status != BlockStatus.Hidden ? borderColor : BackColor);
                }
            }
            headerPicBox.Refresh();
        }

        private void DrawBoarder()
        {
            using (var graphic = Graphics.FromImage(gameDeckBitmap))
            {
                graphic.DrawRectangle(new Pen(Color.Black, deckBorderWidth), new Rectangle(0, 0, gameDeckPicBox.Width, gameDeckPicBox.Height));
                graphic.DrawRectangle(new Pen(BackColor, deckBorderWidth), new Rectangle(deckBorderWidth, 0, gameDeckPicBox.Width - deckBorderWidth * 2, deckBorderWidth));
            }
        }

        private void DrawSingleBlock(Graphics graphics, int x, int y, Color color, Color borderColor)
        {

            //draw rectangle
            var currentX = x * (spaceBetweenBlocks + blockWidthHeight) + spaceBetweenBlocks + deckBorderWidth;
            var currentY = y * (spaceBetweenBlocks + blockWidthHeight) + spaceBetweenBlocks + deckBorderWidth;
            var blockRectangle = new Rectangle(currentX, currentY, blockWidthHeight, blockWidthHeight);
            graphics.DrawRectangle(new Pen(borderColor, 1), blockRectangle);

            // fill inner rectangle
            blockRectangle.X += spaceBetweenBlockBorderAndInnerRectangle * 2;
            blockRectangle.Y += spaceBetweenBlockBorderAndInnerRectangle * 2;
            blockRectangle.Width -= spaceBetweenBlockBorderAndInnerRectangle * 2 * 2;
            blockRectangle.Height -= spaceBetweenBlockBorderAndInnerRectangle * 2 * 2;
            blockRectangle.Width++;
            blockRectangle.Height++;
            graphics.FillRectangle(new SolidBrush(color), blockRectangle);

        }

        private void DrawGhostBlock(Graphics graphics, int x, int y, Color color)
        {
            var currentX = x * (spaceBetweenBlocks + blockWidthHeight) + spaceBetweenBlocks + deckBorderWidth + spaceBetweenBlockBorderAndInnerRectangle;
            var currentY = y * (spaceBetweenBlocks + blockWidthHeight) + spaceBetweenBlocks + deckBorderWidth + spaceBetweenBlockBorderAndInnerRectangle;
            var blockRectangle = new Rectangle(currentX, currentY, blockWidthHeight - spaceBetweenBlockBorderAndInnerRectangle * 2, blockWidthHeight - spaceBetweenBlockBorderAndInnerRectangle * 2);
            graphics.DrawRectangle(new Pen(color, 1), blockRectangle);
        }

        private void DrawHoldTetromino(BlockEventArgs e)
        {
            using (var graphics = Graphics.FromImage(holdTetrominoBitmap))
            {
                graphics.Clear(BackColor);
                foreach (var block in e.Blocks)
                {
                    DrawSingleBlock(graphics, block.X, block.Y, GetColor(block.Status, BackColor), block.Status != BlockStatus.Hidden ? borderColor : hiddenColor);
                }
            }
            holdTetrominoPicBox.Refresh();
        }

        #endregion

        #region Other Methods

        private Color GetColor(BlockStatus status, Color hiddenColor)
        {
            switch (status)
            {
                case BlockStatus.Hidden:

                    return hiddenColor;
                case BlockStatus.LightBlue:

                    return Color.LightBlue;

                case BlockStatus.DarkBlue:

                    return Color.DarkBlue;

                case BlockStatus.Orange:
                    return Color.Orange;

                case BlockStatus.Yellow:
                    return Color.Yellow;

                case BlockStatus.Green:
                    return Color.Green;

                case BlockStatus.Red:
                    return Color.Red;

                default:
                    return Color.Magenta;
            }
        }

        private void DecorateFormControls()
        {
            gameDeckPicBox.Left = (Width - gameDeckPicBox.Width - scoreGrp.Width - nextTetrominoPicBox.Width - horizontalSpaceBetweenUiElements*2 - holdTetrominoPicBox.Width) / 2;
            gameDeckPicBox.Top = Height / 2 - gameDeckPicBox.Height / 2;

            headerPicBox.Left = gameDeckPicBox.Left;
            headerPicBox.Top = gameDeckPicBox.Top - headerPicBox.Height + deckBorderWidth + 1;
            headerPicBox.BringToFront();

            nextLbl.Top = gameDeckPicBox.Top;
            nextLbl.Left = gameDeckPicBox.Left + gameDeckPicBox.Width + horizontalSpaceBetweenUiElements;

            nextTetrominoPicBox.Top = gameDeckPicBox.Top + verticalSpaceBetweenUiElements;
            nextTetrominoPicBox.Left = gameDeckPicBox.Left + gameDeckPicBox.Width + horizontalSpaceBetweenUiElements;

            scoreGrp.Top = gameDeckPicBox.Top;
            scoreGrp.Left = nextTetrominoPicBox.Left + nextTetrominoPicBox.Width + horizontalSpaceBetweenUiElements;

            shortcutsGrp.Left = scoreGrp.Left;
            shortcutsGrp.Top = scoreGrp.Top + scoreGrp.Height + verticalSpaceBetweenUiElements;

            holdTetrominoPicBox.Top = gameDeckPicBox.Top+ verticalSpaceBetweenUiElements;
            holdTetrominoPicBox.Left = gameDeckPicBox.Left - holdTetrominoPicBox.Width - horizontalSpaceBetweenUiElements;
            holdLbl.Top = gameDeckPicBox.Top;
            holdLbl.Left = holdTetrominoPicBox.Left;
        }

        private void InitializeDrawingBitmaps()
        {
            var gameDeckwidth = deckWidth * (blockWidthHeight + spaceBetweenBlocks) + deckBorderWidth * 2;
            var gameDeckHeight = deckHeight * (blockWidthHeight + spaceBetweenBlocks) + deckBorderWidth * 2;

            if (gameDeckHeight > Height || gameDeckwidth > Width)
            {
                blockWidthHeight /= 2;
                gameDeckwidth = deckWidth * (blockWidthHeight + spaceBetweenBlocks) + deckBorderWidth * 2;
                gameDeckHeight = deckHeight * (blockWidthHeight + spaceBetweenBlocks) + deckBorderWidth * 2;
            }

            gameDeckBitmap = new Bitmap(gameDeckwidth, gameDeckHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            nextTetrominoBitmap = new Bitmap(tetrominoWidthHeightBlocks * (blockWidthHeight + spaceBetweenBlocks) + 10, tetrominoWidthHeightBlocks * (blockWidthHeight + spaceBetweenBlocks) *5 + 10, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            headerBitmap = new Bitmap(gameDeckwidth, tetrominoWidthHeightBlocks * (blockWidthHeight + spaceBetweenBlocks) + deckBorderWidth + 1);
            holdTetrominoBitmap = new Bitmap(tetrominoWidthHeightBlocks * (blockWidthHeight + spaceBetweenBlocks) + 10, tetrominoWidthHeightBlocks * (blockWidthHeight + spaceBetweenBlocks)  + 10, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            gameDeckPicBox.Image = gameDeckBitmap;
            nextTetrominoPicBox.Image = nextTetrominoBitmap;
            headerPicBox.Image = headerBitmap;
            holdTetrominoPicBox.Image = holdTetrominoBitmap;
        }

        private void InitializeTetrisGame()
        {
            tetrisGame = new Game.Tetris(deckWidth, deckHeight);
            tetrisGame.ChangedBlocks += TetrisGame_ChangedBlocks;
            tetrisGame.GameOver += TetrisGame_GameOver;
            tetrisGame.NextTetrominoes += TetrisGame_NextTetromino;
            tetrisGame.RowVanish += TetrisGame_RowVanish;
            tetrisGame.Score += TetrisGame_Score;
            tetrisGame.GhostBlocks += TetrisGame_GhostBlocks;
            tetrisGame.HoldTetromino += TetrisGame_HoldTetromino;
            deck = new bool[deckWidth, deckHeight];
            DrawBoarder();
            new Thread(DoMovement) { IsBackground = true }.Start();
            tetrisGame.Start( );
        }

        private void UpdateScores(ScoreEventArgs scoreEventArgs)
        {
            linesLbl.Text = scoreEventArgs.Lines.ToString();
            scoreLbl.Text = scoreEventArgs.Score.ToString();
            levelLbl.Text = scoreEventArgs.Level.ToString();
        }

        private void GameOver()
        {
            MessageBox.Show(this, "Game Over");
        }

        private void DoMovement()
        {
            while (true)
            {
                mre.WaitOne();
                do
                {
                    if (moveRight)
                    {
                        tetrisGame.MoveRight();
                        Thread.Sleep(moveLeftRightDelayInterval);
                    }
                    else if (moveLeft)
                    {
                        tetrisGame.MoveLeft();
                        Thread.Sleep(moveLeftRightDelayInterval);
                    }
                    else if (moveDown)
                    {
                        tetrisGame.MoveDown();
                        Thread.Sleep(moveDownDelayInterval);
                    }
                    else
                    {
                        break;
                    }

                } while (true);
                mre.Reset();
            }
        }

        #endregion

        #endregion

    }
}
