using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Ghost blocks of the game
        /// </summary>
        private Block[] ghostBlocks;

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

        private ChangeResult CalculateGhostBlock(ChangeResult moveResult)
        {
            if (moveResult == null || !GhostBlocksActiveStatus) return moveResult;
            var changedGhostBlocks = new List<Block>();
            foreach (var ghostBlock in ghostBlocks)
            {
                changedGhostBlocks.Add(new Block(ghostBlock, BlockStatus.Hidden));
            }
            ghostBlocks = deck.GetGhostBlocks(current.VisibleBlocks);
            changedGhostBlocks.AddRange(ghostBlocks);
            moveResult.GhostBlocks = changedGhostBlocks.ToArray();
            return moveResult;
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

        #region Public Properties

        /// <summary>
        /// Indicates whether ghost blocks are active
        /// </summary>
        public bool GhostBlocksActiveStatus { get; private set; } = true;

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
            ghostBlocks = deck.GetGhostBlocks(current.VisibleBlocks);
            return new TetrominoInitializationResult
            {
                ChangedBlocks = current.VisibleBlocks,
                NextTetromino = next.BaseBlocks,
                GhostBlocks = ghostBlocks
            };
        }

        /// <summary>
        /// Moves the current tetromino to right
        /// </summary>
        /// <returns></returns>
        public ChangeResult MoveRight()
        {
            var moveResult = current.MoveRight();
            return CalculateGhostBlock(moveResult);
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

            moveDownResult.ChangedBlocks = current.VisibleBlocks;

            moveDownResult.NextTetromino = next.BaseBlocks;
            CalculateGhostBlock(moveDownResult);

            return moveDownResult;
        }

        /// <summary>
        /// Moves the current tetromino to left
        /// </summary>
        /// <returns></returns>
        public ChangeResult MoveLeft()
        {
            var moveResult = current.MoveLeft();
            return CalculateGhostBlock(moveResult);
        }

        /// <summary>
        /// Rotates the current tetromino
        /// </summary>
        /// <returns></returns>
        public ChangeResult[] Rotate()
        {
            var rotateResult = current.Rotate();
            if (rotateResult.Length != 0)
            {
                CalculateGhostBlock(rotateResult.Last());
            }
            return rotateResult;
        }

        /// <summary>
        /// Activates the ghost blocks
        /// </summary>
        /// <returns></returns>
        public ChangeResult ActiveGhostBlocks()
        {
            GhostBlocksActiveStatus = true;
            ghostBlocks = deck.GetGhostBlocks(current.VisibleBlocks);
            return new ChangeResult { ChangedBlocks = ghostBlocks };
        }

        /// <summary>
        /// Deactivates the ghost blocks
        /// </summary>
        /// <returns></returns>
        public ChangeResult DeactiveGhostBlocks()
        {
            GhostBlocksActiveStatus = false;
            var hiddenGhostBlocks = new List<Block>();
            foreach (var item in ghostBlocks)
            {
                hiddenGhostBlocks.Add(new Block(item, BlockStatus.Hidden));
            }
            return new ChangeResult { ChangedBlocks = hiddenGhostBlocks.ToArray() };
        }

        #endregion

    }
}
