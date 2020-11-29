using System;
using System.Collections.Generic;
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
        private bool hardDrop;

        /// <summary>
        /// Indicates whether the previous move was the tetromino last move before locking
        /// </summary>
        private bool lastMove;

        /// <summary>
        /// The interval of lock delay
        /// </summary>
        private readonly int lockDelayInterval = 500;

        /// <summary>
        /// The tick count of the last successfull move
        /// </summary>
        private int lastMoveTickCount;

        #endregion

        #region Private Methods

        /// <summary>
        /// Checks for lock delay 
        /// </summary>
        /// <returns></returns>
        private bool CheckForLockDelay()
        {
            if (!lastMove) return false;

            if (Environment.TickCount - lastMoveTickCount >= lockDelayInterval)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Set the last move to current tick count in order to be used for lock delay mechanism
        /// </summary>
        /// <param name="changeResult"></param>
        private void SetLastMove(ChangeResult changeResult)
        {
            if (changeResult != null)
            {
                lastMove = changeResult.LastMove;
                lastMoveTickCount = Environment.TickCount;
            }
        }

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
            OnNextTetrominoes(initialResult.NextTetrominoes);
            OnChangedBlocks(initialResult.ChangedBlocks);
            OnGhostBlocks(initialResult.GhostBlocks);
        }

        /// <summary>
        /// Initializes the score management. This is called on every new game
        /// </summary>
        private void InitilizeScoreManagement()
        {
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

                if (CheckForLockDelay()) continue;

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
            if (!hardDrop)
            {
                if (CheckForLockDelay())
                {
                    return false;
                }
            }

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

            if (!hardDrop)
            {
                SetLastMove(moveDownResult);
            }

            if (moveDownResult.VanishRowResult != null)
            {
                if (hardDrop)
                {
                    switch (moveDownResult.VanishRowResult.VanishedRowCount)
                    {
                        case 1:
                            OnGameEvents(GameEvent.HardDropWithOneLineClear);
                            break;
                        case 2:
                            OnGameEvents(GameEvent.HardDropWithTwoLinesClear);
                            break;
                        case 3:
                            OnGameEvents(GameEvent.HardDropWithThreeLinesClear);
                            break;
                        case 4:
                            OnGameEvents(GameEvent.HardDropWithFourLinesClear);
                            break;
                    }
                }
                else
                {
                    switch (moveDownResult.VanishRowResult.VanishedRowCount)
                    {
                        case 1:
                            OnGameEvents(GameEvent.OneLineCleared);
                            break;
                        case 2:
                            OnGameEvents(GameEvent.TwoLinesCleared);
                            break;
                        case 3:
                            OnGameEvents(GameEvent.ThreeLinesCleared);
                            break;
                        case 4:
                            OnGameEvents(GameEvent.FourLinesCleared);
                            break;
                    }
                }
                ProcessVanishRowResult(moveDownResult.VanishRowResult, hardDrop);
            }

            if (moveDownResult.GhostBlocks != null)
            {
                OnGhostBlocks(moveDownResult.GhostBlocks);
            }

            if (moveDownResult.NextTetrominoes != null)
            {
                if (!hardDrop && moveDownResult.VanishRowResult == null)
                {
                    OnGameEvents(GameEvent.TetrominoLocked);
                }
                else if (moveDownResult.VanishRowResult == null)
                {
                    OnGameEvents(GameEvent.HardDrop);
                }
                OnNextTetrominoes(moveDownResult.NextTetrominoes);
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
        public event EventHandler<NextTetrominoesEventArg> NextTetrominoes;

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

        /// <summary>
        /// Occures when holds a tetromino
        /// </summary>
        public event EventHandler<BlockEventArgs> HoldTetromino;

        /// <summary>
        /// Occures when something happens in the game
        /// </summary>
        public event EventHandler<GameEventsEventArgs> GameEvents;

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
        /// Start the game from the input level
        /// </summary>
        /// <param name="level"></param>
        public void Start(Level level)
        {
            Level = level;
            InitializeTetris();
        }

        /// <summary>
        /// Start the game
        /// </summary>
        public void Start()
        {
            Start(Level.One);
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
                SetLastMove(moveResult);
                if (moveResult != null)
                {
                    OnGameEvents(GameEvent.MoveRightSuccessful);
                    if (moveResult.GhostBlocks != null)
                    {
                        OnGhostBlocks(moveResult.GhostBlocks);
                    }
                    OnChangedBlocks(moveResult.ChangedBlocks);
                }
                else
                {
                    OnGameEvents(GameEvent.MoveRightFailed);
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
                SetLastMove(moveResult);
                if (moveResult != null)
                {
                    OnGameEvents(GameEvent.MoveLeftSuccessful);
                    if (moveResult.GhostBlocks != null)
                    {
                        OnGhostBlocks(moveResult.GhostBlocks);
                    }
                    OnChangedBlocks(moveResult.ChangedBlocks);
                }
                else
                {
                    OnGameEvents(GameEvent.MoveLeftFailed);
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
                SetLastMove(rotateResult.LastOrDefault());
                if (rotateResult.Length > 0)
                {
                    OnGameEvents(GameEvent.RotationSuccessful);
                    foreach (var changeResult in rotateResult)
                    {
                        if (changeResult.GhostBlocks != null)
                        {
                            OnGhostBlocks(changeResult.GhostBlocks);
                        }
                        OnChangedBlocks(changeResult.ChangedBlocks);
                    }
                }
                else
                {
                    OnGameEvents(GameEvent.RotationFailed);
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
            Restart(Level.One);
        }

        /// <summary>
        /// Restart the game from the input level
        /// </summary>
        /// <param name="level"></param>
        public void Restart(Level level)
        {
            Status = GameStatus.RestartPending;
            runnerThread.Join();
            Level = level;
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

        /// <summary>
        /// Holds the current tetromino
        /// </summary>
        public void Hold()
        {
            if (Status != GameStatus.Running)
            {
                return;
            }
            lock (synchronizationToken)
            {
                var holdResult = tetrominoHandler.Hold();
                SetLastMove(holdResult);
                if (holdResult != null)
                {
                    OnGameEvents(GameEvent.HoldSuccessful);
                    OnHoldTetromino(holdResult.HoldBlocks);
                    OnChangedBlocks(holdResult.ChangedBlocks);
                    if (holdResult.GhostBlocks != null)
                    {
                        OnGhostBlocks(holdResult.GhostBlocks);
                    }
                    if (holdResult.NextTetrominoes != null)
                    {
                        OnNextTetrominoes(holdResult.NextTetrominoes);
                    }
                }
                else
                {
                    OnGameEvents(GameEvent.HoldFailed);
                }
            }
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
        /// <param name="nextTetrominoesBlocks"></param>
        protected virtual void OnNextTetrominoes(List<Block[]> nextTetrominoesBlocks)
        {
            NextTetrominoes?.Invoke(this, new NextTetrominoesEventArg(nextTetrominoesBlocks));
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
        protected virtual void OnGhostBlocks(Block[] ghostBlocks)
        {
            GhostBlocks?.Invoke(this, new BlockEventArgs(ghostBlocks));
        }

        /// <summary>
        /// Invokes the HoldTetromino event
        /// </summary>
        /// <param name="holdBlocks"></param>
        protected virtual void OnHoldTetromino(Block[] holdBlocks)
        {
            HoldTetromino?.Invoke(this, new BlockEventArgs(holdBlocks));
        }

        /// <summary>
        /// Invokes the GameEvents event
        /// </summary>
        /// <param name="gameEvent"></param>
        protected virtual void OnGameEvents(GameEvent gameEvent)
        {
            GameEvents?.Invoke(this, new GameEventsEventArgs(gameEvent));
        }

        #endregion

    }
}