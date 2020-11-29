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
        /// Game next tetrominoes
        /// </summary>
        private Queue<Tetromino> next;

        /// <summary>
        /// Game deck
        /// </summary>
        private readonly Deck deck;

        /// <summary>
        /// Ghost blocks of the game
        /// </summary>
        private Block[] ghostBlocks;

        /// <summary>
        /// Indicates whether hold is possible
        /// </summary>
        private bool holdIsPossible;

        /// <summary>
        /// The held tetromino
        /// </summary>
        private Tetromino held;

        /// <summary>
        /// tetromino generator based on 7 bag algorithm
        /// </summary>
        private readonly Tetrominos7BagRandomizer tetrominos7Bag;

        /// <summary>
        /// number of tetrominoes in queue for preview box
        /// </summary>
        private readonly int nextTetrominoesQueueLenght = 5;

        #endregion

        #region Private Methods

        /// <summary>
        /// Generates a new tetromino
        /// </summary>
        /// <returns></returns>
        private Tetromino GenerateNewTetromino()
        {
            return tetrominos7Bag.GetNewTetromino();
        }

        /// <summary>
        /// Calculates the ghost block for current tetromino
        /// </summary>
        /// <param name="moveResult"></param>
        private void CalculateGhostBlock(ChangeResult moveResult)
        {
            if (moveResult == null || !GhostBlocksActiveStatus) return;
            var changedGhostBlocks = new List<Block>();
            foreach (var ghostBlock in ghostBlocks)
            {
                changedGhostBlocks.Add(new Block(ghostBlock, BlockStatus.Hidden));
            }
            ghostBlocks = deck.GetGhostBlocks(current.VisibleBlocks);
            changedGhostBlocks.AddRange(ghostBlocks);
            moveResult.GhostBlocks = changedGhostBlocks.ToArray();
        }

        /// <summary>
        /// Sets the last move value in the result
        /// </summary>
        /// <param name="moveResult"></param>
        private void SetLastMove(ChangeResult moveResult)
        {
            if (moveResult == null) return;

            moveResult.LastMove = !current.CanMoveDown();
        }

        #endregion

        #region Ctor

        /// <summary>
        /// Creates a new instance of the class
        /// </summary>
        /// <param name="deck"></param>
        public TetrominoHandler(Deck deck)
        {
            this.deck = deck;
            tetrominos7Bag = new Tetrominos7BagRandomizer(deck);
            next = new Queue<Tetromino>(nextTetrominoesQueueLenght);
            holdIsPossible = true;
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
            for (var i = 0; i < nextTetrominoesQueueLenght; i++)
            {
                next.Enqueue(GenerateNewTetromino());
            }
            ghostBlocks = deck.GetGhostBlocks(current.VisibleBlocks);

            return new TetrominoInitializationResult
            {
                ChangedBlocks = current.VisibleBlocks,
                NextTetrominoes = next.Select(s => s.BaseBlocks).ToList(),
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
            SetLastMove(moveResult);
            CalculateGhostBlock(moveResult);
            return moveResult;
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
                SetLastMove(moveDownResult);
                return moveDownResult;
            }

            current = next.Dequeue();
            next.Enqueue(GenerateNewTetromino());

            moveDownResult.ChangedBlocks = current.VisibleBlocks;

            moveDownResult.NextTetrominoes = next.Select(s => s.BaseBlocks).ToList();
            CalculateGhostBlock(moveDownResult);
            holdIsPossible = true;
            SetLastMove(moveDownResult);
            return moveDownResult;
        }

        /// <summary>
        /// Moves the current tetromino to left
        /// </summary>
        /// <returns></returns>
        public ChangeResult MoveLeft()
        {
            var moveResult = current.MoveLeft();
            SetLastMove(moveResult);
            CalculateGhostBlock(moveResult);
            return moveResult;
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
                SetLastMove(rotateResult.Last());
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

        /// <summary>
        /// Holds the current tetromino and replaces the current tetromino with the held tetromino
        /// </summary>
        /// <returns></returns>
        public HoldResult Hold()
        {
            if (!holdIsPossible)
            {
                return null;
            }
            
            var changedBlocks = new List<Block>();
            
            foreach (var item in current.VisibleBlocks)
            {
                changedBlocks.Add(new Block(item, BlockStatus.Hidden));
            }

            var holdResult = new HoldResult();

            if (held == null)
            {    
                held = current;
                current = next.Dequeue();
                next.Enqueue(GenerateNewTetromino());  
                holdResult.NextTetrominoes =  next.Select(s => s.BaseBlocks).ToList();
            }
            else
            {
                var tempTetro = held;
                held = current;
                current = tempTetro;
            }

            held.ResetToTopMiddle();
            foreach (var item in current.VisibleBlocks)
            {
                changedBlocks.Add(new Block(item));
            }

            holdResult.HoldBlocks = held.BaseBlocks;
            holdResult.ChangedBlocks = changedBlocks.ToArray();
            CalculateGhostBlock(holdResult);
            SetLastMove(holdResult);
            holdIsPossible = false;
            return holdResult;
        }

        #endregion

    }
}
