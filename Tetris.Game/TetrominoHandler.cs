using System;
using Tetris.Game.Results;
using Tetris.Game.Tetriminoes;

namespace Tetris.Game
{

    /// <summary>
    /// This class handels tetromino related operations
    /// </summary>
    internal class TetrominoHandler
    {

        #region Private Variables

        /// <summary>
        /// Game current tetromino
        /// </summary>
        private Tetromino current;

        /// <summary>
        /// Game next tetromino
        /// </summary>
        private Tetromino next;

        /// <summary>
        /// Random generator for generating tetrominos
        /// </summary>
        private readonly Random randomGenerator;

        /// <summary>
        /// Game deck
        /// </summary>
        private readonly Deck deck;

        #endregion

        #region Private Methods

        /// <summary>
        /// Generates a new tetromino
        /// </summary>
        /// <returns></returns>
        private Tetromino GenerateNewTetromino()
        {
            var generatedRandomNumber = randomGenerator.Next(0, 7);
            switch (generatedRandomNumber)
            {
                case 0:
                    return new OTetromino(deck);
                case 1:
                    return new ITetromino(deck);
                case 2:
                    return new LTetromino(deck);
                case 3:
                    return new ZTetromino(deck);
                case 4:
                    return new STetromino(deck);
                case 5:
                    return new JTetromino(deck);
                default:
                    return new TTetromino(deck);
            }
        }

        #endregion

        #region Ctor

        /// <summary>
        /// Creates a new instance of the class
        /// </summary>
        /// <param name="deck"></param>
        public TetrominoHandler(Deck deck)
        {
            randomGenerator = new Random();
            this.deck = deck;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initizes the tetromino handler. This is called once at the beginning of every game
        /// </summary>
        /// <returns></returns>
        public TetrominoInitializationResult Initialize()
        {
            current = GenerateNewTetromino();
            next = GenerateNewTetromino();
            return new TetrominoInitializationResult
            {
                ChangedBlocks = current.VisibleBlocks,
                NextTetromino = next.BaseBlocks
            };
        }

        /// <summary>
        /// Moves the current tetromino to right
        /// </summary>
        /// <returns></returns>
        public ChangeResult MoveRight()
        {
            return current.MoveRight();
        }

        /// <summary>
        /// Moves the current tetromino to down
        /// </summary>
        /// <returns></returns>
        public MoveDownResult MoveDown()
        {

            var moveDownResult = current.MoveDown();

            if (moveDownResult.GameOver || moveDownResult.ChangedBlocks != null)
            {
                return moveDownResult;
            }

            current = next;
            next = GenerateNewTetromino();

            moveDownResult.NextTetromino = next.BaseBlocks;

            return moveDownResult;
        }

        /// <summary>
        /// Moves the current tetromino to left
        /// </summary>
        /// <returns></returns>
        public ChangeResult MoveLeft()
        {
            return current.MoveLeft();
        }

        /// <summary>
        /// Rotates the current tetromino
        /// </summary>
        /// <returns></returns>
        public ChangeResult[] Rotate()
        {
            return current.Rotate();
        }

        #endregion

    }
}
