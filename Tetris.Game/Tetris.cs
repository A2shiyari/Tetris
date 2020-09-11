using System;
using System.Linq;
using System.Threading;
using Tetris.Game.Results;

namespace Tetris.Game
{

    /// <summary>
    /// The tetris game. This is the main tetris game class.This class responsiles for tetrominoes movements and rotations as well as notifying changed blocks and new scores and game play
    /// </summary>
    public class Tetris
    {

        #region Private Variables

        /// <summary>
        /// game runner thread
        /// </summary>
        private Thread runnerThread;

        /// <summary>
        /// synchronization token used for locking purposes
        /// </summary>
        private readonly object synchronizationToken = new object();

        /// <summary>
        /// Status of the game
        /// </summary>
        private GameStatus Status;

        /// <summary>
        /// Game deck
        /// </summary>
        private Deck deck;

        /// <summary>
        /// Game tetromino handler
        /// </summary>
        private TetrominoHandler tetrominoHandler;

        /// <summary>
        /// Runner thread interval based on game level
        /// </summary>
        private int runnerThreadInterval = Level.One.GetRunningInterval();

        /// <summary>
        /// Game score management
        /// </summary>
        private ScoreManagement scoreManagement;

        /// <summary>
        /// Delay between move downs in hard drop
        /// </summary>
        private readonly int hardDropDelay = 10;

        /// <summary>
        /// Deck width
        /// </summary>
        private readonly int deckWidth;

        /// <summary>
        /// Deck height
        /// </summary>
        private readonly int deckHeight;

        /// <summary>
        /// Indicates whether hard drop is on going
        /// </summary>
        private bool hardDrop = false;

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets the status to game over and notifies game over
        /// </summary>
        private void GameIsOver()
        {
            Status = GameStatus.GameOver;
            OnGameOver();
        }

        /// <summary>
        /// Initializes a new game and starts the runner thread
        /// </summary>
        private void InitializeTetris()
        {
            runnerThread = new Thread(OnTetris)
            {
                Name = "Tetris Main Thread",
                IsBackground = true
            };
            runnerThread.Start();
        }

        /// <summary>
        /// Initializes the deck. This is called on every new game
        /// </summary>
        private void InitializeDeck()
        {
            deck = new Deck(deckWidth, deckHeight);
            var initialBlocks = deck.Initialize();
            OnChangedBlocks(initialBlocks);
        }

        /// <summary>
        /// Initializes the tetromino handler. This is called on every new game
        /// </summary>
        private void InitilizeTetrominoHandler()
        {
            tetrominoHandler = new TetrominoHandler(deck);
            var initialResult = tetrominoHandler.Initialize();
            OnNextTetromino(initialResult.NextTetromino);
            OnChangedBlocks(initialResult.ChangedBlocks);
            OnGhostBlocks(initialResult.GhostBlocks);
        }

        /// <summary>
        /// Initializes the score management. This is called on every new game
        /// </summary>
        private void InitilizeScoreManagement()
        {
            Level = Level.One;
            runnerThreadInterval = Level.GetRunningInterval();
            scoreManagement = new ScoreManagement();
            OnScore(0, 0, Level);
        }

        /// <summary>
        /// The runner thread method that controls the game 
        /// </summary>
        private void OnTetris()
        {
            InitializeDeck();
            InitilizeTetrominoHandler();
            InitilizeScoreManagement();
            Status = GameStatus.Running;
            while (true)
            {
                Thread.Sleep(runnerThreadInterval);

                if (hardDrop || Status == GameStatus.Paused) continue;

                if (Status == GameStatus.RestartPending)
                {
                    SetGhostValue(false);
                    return;
                }

                MoveDown();

                if (Status == GameStatus.GameOver)
                {
                    SetGhostValue(false);
                    return;
                }
            }
        }

        /// <summary>
        /// Process post vanished row(s) operation and processes the result
        /// </summary>
        /// <param name="vanishRowResult"></param>
        /// <param name="hardDrop"></param>
        private void ProcessVanishRowResult(VanishRowResult vanishRowResult, bool hardDrop)
        {
            OnRowVanish(vanishRowResult.VanishedBlocks);
            OnChangedBlocks(vanishRowResult.ChangedBlocks);
            var vanishedRowCount = vanishRowResult.VanishedRowCount;
            Level = scoreManagement.UpdateScores(vanishedRowCount, this.Level, hardDrop);
            runnerThreadInterval = Level.GetRunningInterval();
            OnScore(scoreManagement.Score, scoreManagement.Lines, Level);
        }

        /// <summary>
        /// Moves down the current tetromino
        /// </summary>
        /// <param name="hardDrop"></param>
        /// <returns></returns>
        private bool MoveDownInternal(bool hardDrop)
        {
            var moveDownResult = tetrominoHandler.MoveDown();
            if (moveDownResult.GameOver)
            {
                GameIsOver();
                return true;
            }

            if (moveDownResult.ChangedBlocks != null)
            {
                OnChangedBlocks(moveDownResult.ChangedBlocks);
            }

            if (moveDownResult.VanishRowResult != null)
            {
                ProcessVanishRowResult(moveDownResult.VanishRowResult, hardDrop);
            }

            if (moveDownResult.GhostBlocks != null)
            {
                OnGhostBlocks(moveDownResult.GhostBlocks);
            }

            if (moveDownResult.NextTetromino != null)
            {
                OnNextTetromino(moveDownResult.NextTetromino);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Actives or deactives the ghost blocks
        /// </summary>
        /// <param name="value"></param>
        private void SetGhostValue(bool value)
        {
            if (value)
            {
                var ghostBlocks = tetrominoHandler.ActiveGhostBlocks();
                OnGhostBlocks(ghostBlocks.ChangedBlocks);
            }
            else
            {
                var ghostBlocks = tetrominoHandler.DeactiveGhostBlocks();
                OnGhostBlocks(ghostBlocks.ChangedBlocks);
            }
        }

        #endregion

        #region Ctor

        /// <summary>
        /// Creates a new instance of the game
        /// </summary>
        /// <param name="deckWidth">Deck width</param>
        /// <param name="deckHeight">Deck height</param>
        public Tetris(int deckWidth, int deckHeight)
        {
            this.deckWidth = deckWidth;
            this.deckHeight = deckHeight;
        }

        #endregion

        #region Events

        /// <summary>
        /// Occures when game score updates
        /// </summary>
        public event EventHandler<ScoreEventArgs> Score;

        /// <summary>
        /// Occures when next tetromino is available
        /// </summary>
        public event EventHandler<BlockEventArgs> NextTetromino;

        /// <summary>
        /// Occures when blocks change in deck
        /// </summary>
        public event EventHandler<BlockEventArgs> ChangedBlocks;

        /// <summary>
        /// Occures when row(s) vanish
        /// </summary>
        public event EventHandler<BlockEventArgs> RowVanish;

        /// <summary>
        /// Occures when game is over
        /// </summary>
        public event EventHandler GameOver;

        /// <summary>
        /// Occures when ghost blocks change
        /// </summary>
        public event EventHandler<BlockEventArgs> GhostBlocks;

        #endregion

        #region Public Properties

        /// <summary>
        /// Game Level
        /// </summary>
        public Level Level { get; set; }

        /// <summary>
        /// Indicates whether ghost blocks are visible or not
        /// </summary>
        public bool GhostBlocksVisible
        {
            get { return tetrominoHandler.GhostBlocksActiveStatus; }
            set { SetGhostValue(value); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Start the game
        /// </summary>
        public void Start()
        {
            InitializeTetris();
        }

        /// <summary>
        /// Move right the current tetromino
        /// </summary>
        public void MoveRight()
        {
            if (Status != GameStatus.Running)
            {
                return;
            }
            lock (synchronizationToken)
            {
                var moveResult = tetrominoHandler.MoveRight();
                if (moveResult != null)
                {
                    if (moveResult.GhostBlocks != null)
                    {
                        OnGhostBlocks(moveResult.GhostBlocks);
                    }
                    OnChangedBlocks(moveResult.ChangedBlocks);
                }
            }
        }

        /// <summary>
        /// Move Left the current tetromino
        /// </summary>
        public void MoveLeft()
        {
            if (this.Status != GameStatus.Running)
            {
                return;
            }
            lock (synchronizationToken)
            {
                var moveResult = tetrominoHandler.MoveLeft();
                if (moveResult != null)
                {
                    if (moveResult.GhostBlocks != null)
                    {
                        OnGhostBlocks(moveResult.GhostBlocks);
                    }
                    OnChangedBlocks(moveResult.ChangedBlocks);
                }
            }
        }

        /// <summary>
        /// Move down the current tetromino
        /// </summary>
        public void MoveDown()
        {
            if (this.Status != GameStatus.Running)
            {
                return;
            }
            lock (synchronizationToken)
            {
                MoveDownInternal(false);
            }
        }

        /// <summary>
        /// Rotate the current tetromino
        /// </summary>
        public void Rotate()
        {
            if (Status != GameStatus.Running)
            {
                return;
            }
            lock (synchronizationToken)
            {
                var rotateResult = tetrominoHandler.Rotate();
                foreach (var changeResult in rotateResult)
                {
                    if (changeResult.GhostBlocks != null)
                    {
                        OnGhostBlocks(changeResult.GhostBlocks);
                    }
                    OnChangedBlocks(changeResult.ChangedBlocks);
                }
            }
        }

        /// <summary>
        /// Pause the game
        /// </summary>
        public void Pause()
        {
            if (Status != GameStatus.Running)
            {
                return;
            }
            Status = GameStatus.Paused;
        }

        /// <summary>
        /// Resume the game
        /// </summary>
        public void Resume()
        {
            if (Status != GameStatus.Paused)
            {
                return;
            }
            Status = GameStatus.Running;
        }

        /// <summary>
        /// Restart the game
        /// </summary>
        public void Restart()
        {
            Status = GameStatus.RestartPending;
            runnerThread.Join();
            InitializeTetris();
        }

        /// <summary>
        /// Hard drop current tetromino
        /// </summary>
        public void HardDrop()
        {
            if (this.Status != GameStatus.Running)
            {
                return;
            }
            hardDrop = true;
            lock (synchronizationToken)
            {
                while (!MoveDownInternal(true))
                {
                    Thread.Sleep(hardDropDelay);
                }
            }
            hardDrop = false;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Invokes the Score event
        /// </summary>
        /// <param name="score"></param>
        /// <param name="lines"></param>
        /// <param name="level"></param>
        protected virtual void OnScore(int score, int lines, Level level)
        {
            Score?.Invoke(this, new ScoreEventArgs(score, lines, Level));
        }

        /// <summary>
        /// Invokes the RowVanish event
        /// </summary>
        /// <param name="vanishedRowsBlocks"></param>
        protected virtual void OnRowVanish(Block[] vanishedRowsBlocks)
        {
            RowVanish?.Invoke(this, new BlockEventArgs(vanishedRowsBlocks));
        }

        /// <summary>
        /// Invokes the NextTetromino event
        /// </summary>
        /// <param name="nextTetrominoBlocks"></param>
        protected virtual void OnNextTetromino(Block[] nextTetrominoBlocks)
        {
            NextTetromino?.Invoke(this, new BlockEventArgs(nextTetrominoBlocks));
        }

        /// <summary>
        /// Invokes the ChangedBlocks event
        /// </summary>
        /// <param name="changedBlocks"></param>
        protected virtual void OnChangedBlocks(Block[] changedBlocks)
        {
            ChangedBlocks?.Invoke(this, new BlockEventArgs(changedBlocks));
        }

        /// <summary>
        /// Invokes the GameOver event
        /// </summary>
        protected virtual void OnGameOver()
        {
            GameOver?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Invokes the GhostBlocks event
        /// </summary>
        /// <param name="ghostBlocks"></param>
        public virtual void OnGhostBlocks(Block[] ghostBlocks)
        {
            GhostBlocks?.Invoke(this, new BlockEventArgs(ghostBlocks));
        }

        #endregion

    }
}
