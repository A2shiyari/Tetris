using System;
using System.Drawing;
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
        private const int spaceBetweenBlocks = 0;
        private const int spaceBetweenBlockBorderAndInnerRectangle = 2;
        private const int deckBorderWidth = 5;
        private const int vanishDelayInterval = 200;
        private const int moveRightLeftTimerInterval = 65;

        #endregion

        #region Private Variables

        private int blockWidthHeight = 30;

        private readonly Color vanishColor = Color.DarkOrange;
        private readonly Color visibleColor = Color.Black;
        private readonly Color hiddenColor = Color.White;
        private readonly Color ghostColor = Color.LightGray;

        private Game.Tetris tetrisGame;

        private bool moveDown;
        private bool moveLeft;
        private bool moveRight;
        private readonly Timer movementTimer = new Timer { Interval = 5 };

        private Bitmap gameDeckBitmap;
        private Bitmap nextTetrominoBitmap;

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

        private void TetrisGame_NextTetromino(object sender, BlockEventArgs e)
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

            if (!(moveDown || moveLeft || moveRight))
            {
                movementTimer.Stop();
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
                    movementTimer.Interval = 25;
                    moveDown = true;
                    movementTimer.Start();
                    break;

                case Keys.Left:
                    tetrisGame.MoveLeft();
                    Thread.Sleep(moveRightLeftTimerInterval);
                    movementTimer.Interval = moveRightLeftTimerInterval;
                    moveLeft = true;
                    movementTimer.Start();
                    break;

                case Keys.Right:
                    tetrisGame.MoveRight();
                    Thread.Sleep(moveRightLeftTimerInterval);
                    movementTimer.Interval = moveRightLeftTimerInterval;
                    moveRight = true;
                    movementTimer.Start();
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

            }
        }

        private void MainFrom_Load(object sender, EventArgs e)
        {
            InitializeDrawingBitmaps();
            DecorateFormControls();
            InitializeTetrisGame();
            movementTimer.Tick += MovementTimer_Tick;
        }

        private void MainFrom_Deactivate(object sender, EventArgs e)
        {
            movementTimer.Stop();
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

        private void DrawNextTetromino(BlockEventArgs e)
        {
            using (var graphics = Graphics.FromImage(nextTetrominoBitmap))
            {
                graphics.Clear(BackColor);
                foreach (var block in e.Blocks)
                {
                    DrawSingleBlock(graphics, block.X, block.Y, visibleColor);
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
                    DrawSingleBlock(graphics, block.X, block.Y, vanishColor);
                }
                gameDeckPicBox.Refresh();

                Thread.Sleep(vanishDelayInterval);

                foreach (var block in e.Blocks)
                {
                    DrawSingleBlock(graphics, block.X, block.Y, visibleColor);
                }
            }
            gameDeckPicBox.Refresh();
        }

        private void DrawGhostBlocks(BlockEventArgs e)
        {
            using (var graphics = Graphics.FromImage(gameDeckBitmap))
            {
                foreach (var block in e.Blocks)
                {
                    DrawSingleBlock(graphics, block.X, block.Y, block.Status == BlockStatus.Visible ? ghostColor : hiddenColor);
                }
            }
            gameDeckPicBox.Refresh();
        }

        private void DrawChangedBlocks(BlockEventArgs e)
        {
            using (var graphics = Graphics.FromImage(gameDeckBitmap))
            {
                foreach (var block in e.Blocks)
                {
                    DrawSingleBlock(graphics, block.X, block.Y, block.Status == BlockStatus.Visible ? visibleColor : hiddenColor);
                }
            }
            gameDeckPicBox.Refresh();
        }

        private void DrawBoarder()
        {
            using (var graphic = Graphics.FromImage(gameDeckBitmap))
            {
                graphic.DrawRectangle(new Pen(Color.Black, deckBorderWidth), new Rectangle(0, 0, gameDeckPicBox.Width, gameDeckPicBox.Height));
            }
        }

        private void DrawSingleBlock(Graphics graphics, int x, int y, Color color)
        {
            //draw rectangle
            var currentX = x * (spaceBetweenBlocks + blockWidthHeight) + spaceBetweenBlocks + deckBorderWidth;
            var currentY = y * (spaceBetweenBlocks + blockWidthHeight) + spaceBetweenBlocks + deckBorderWidth;
            var blockRectangle = new Rectangle(currentX, currentY, blockWidthHeight, blockWidthHeight);
            graphics.DrawRectangle(new Pen(color), blockRectangle);

            // fill inner rectangle
            blockRectangle.X += spaceBetweenBlockBorderAndInnerRectangle;
            blockRectangle.Y += spaceBetweenBlockBorderAndInnerRectangle;
            blockRectangle.Width -= spaceBetweenBlockBorderAndInnerRectangle * 2 - 1;
            blockRectangle.Height -= spaceBetweenBlockBorderAndInnerRectangle * 2 - 1;
            graphics.FillRectangle(new SolidBrush(color), blockRectangle);


         //   graphics.DrawString(x + "," + y, DefaultFont, new SolidBrush(Color.Red), blockRectangle);
        }

        #endregion

        #region Other Methods

        private void DecorateFormControls()
        {
            gameDeckPicBox.Left = Width / 2 - gameDeckPicBox.Width / 2 - scoreGrp.Width / 2;
            gameDeckPicBox.Top = Height / 2 - gameDeckPicBox.Height / 2;

            nextLbl.Top = gameDeckPicBox.Top;
            nextLbl.Left = gameDeckPicBox.Left + gameDeckPicBox.Width + 100;

            nextTetrominoPicBox.Top = gameDeckPicBox.Top + 30;
            nextTetrominoPicBox.Left = gameDeckPicBox.Left + gameDeckPicBox.Width + 100;

            scoreGrp.Top = nextTetrominoPicBox.Top + nextTetrominoPicBox.Height + 20;
            scoreGrp.Left = nextTetrominoPicBox.Left;

            shortcutsGrp.Left = scoreGrp.Left;
            shortcutsGrp.Top = scoreGrp.Top + scoreGrp.Height + 20;
        }

        private void InitializeDrawingBitmaps()
        {
            var gameDeckwidth = deckWidth * (blockWidthHeight + spaceBetweenBlocks) + 10;
            var gameDeckHeight = deckHeight * (blockWidthHeight + spaceBetweenBlocks) + 10;

            if (gameDeckHeight > Height || gameDeckwidth > Width)
            {
                blockWidthHeight /= 2;
                gameDeckwidth = deckWidth * (blockWidthHeight + spaceBetweenBlocks) + 10;
                gameDeckHeight = deckHeight * (blockWidthHeight + spaceBetweenBlocks) + 10;
            }

            gameDeckBitmap = new Bitmap(gameDeckwidth, gameDeckHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            nextTetrominoBitmap = new Bitmap(tetrominoWidthHeightBlocks * (blockWidthHeight + spaceBetweenBlocks), tetrominoWidthHeightBlocks * (blockWidthHeight + spaceBetweenBlocks) + 10, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            gameDeckPicBox.Image = gameDeckBitmap;
            nextTetrominoPicBox.Image = nextTetrominoBitmap;
        }

        private void InitializeTetrisGame()
        {
            tetrisGame = new Game.Tetris(deckWidth, deckHeight);
            tetrisGame.ChangedBlocks += TetrisGame_ChangedBlocks;
            tetrisGame.GameOver += TetrisGame_GameOver;
            tetrisGame.NextTetromino += TetrisGame_NextTetromino;
            tetrisGame.RowVanish += TetrisGame_RowVanish;
            tetrisGame.Score += TetrisGame_Score;
            tetrisGame.GhostBlocks += TetrisGame_GhostBlocks;
            DrawBoarder();
            tetrisGame.Start();
        }

        private void UpdateScores(ScoreEventArgs scoreEventArgs)
        {
            linesLbl.Text = scoreEventArgs.Lines.ToString();
            scoreLbl.Text = scoreEventArgs.Score.ToString();
            levelLbl.Text = scoreEventArgs.Level.ToString();
        }

        private void MovementTimer_Tick(object sender, EventArgs e)
        {
            if (moveLeft)
            {
                tetrisGame.MoveLeft();
                return;
            }
            if (moveRight)
            {
                tetrisGame.MoveRight();
                return;
            }
            if (moveDown)
            {
                tetrisGame.MoveDown();
                return;
            }
        }

        private void GameOver()
        {
            MessageBox.Show(this, "Game Over");
        }

        #endregion

        #endregion

    }
}
