using System.Collections.Generic;
using System.Linq;
using Tetris.Game.Results;

namespace Tetris.Game.Tetriminoes
{

    /// <summary>
    /// The base class of the tetromino 
    /// This class is responsible for tetromino related operations such as movements and rotation
    /// </summary>
    internal abstract class Tetromino
    {

        #region Private Variables

        /// <summary>
        /// The deck which tetromino belongs to
        /// </summary>
        private readonly Deck deck;

        /// <summary>
        /// The tetromino width and height
        /// </summary>
        private readonly byte tetrominoWidthHeight;

        #endregion

        #region Private Methods

        /// <summary>
        /// clones the visible blocks of the tetromino and inverses the values to hidden in order to calculate changed blocks
        /// </summary>
        /// <returns></returns>
        private List<Block> InverseVisibleBlocks()
        {
            var changedBlocks = new List<Block>();
            foreach (var block in VisibleBlocks)
            {
                changedBlocks.Add(new Block(block, BlockStatus.Hidden));
            }
            return changedBlocks;
        }

        /// <summary>
        /// Adds the visible blocks to changed blocks. This method is used after movement or rotation to create changed blocks
        /// </summary>
        /// <param name="changedBlocks"></param>
        /// <returns></returns>
        private Block[] AddVisibleBlocksToChangedBlocks(List<Block> changedBlocks)
        {
            foreach (var item in VisibleBlocks)
            {
                if (changedBlocks.Contains(item))
                {
                    changedBlocks.Remove(item);
                }
            }
            changedBlocks.AddRange(VisibleBlocks);
            return changedBlocks.ToArray();
        }

        /// <summary>
        /// Creates tetromino blocks in the top middle position of the deck
        /// </summary>
        private void CreateBlocks()
        {
            Blocks = new Block[tetrominoWidthHeight * tetrominoWidthHeight];
            for (var i = 0; i < tetrominoWidthHeight; i++)
            {
                for (var j = 0; j < tetrominoWidthHeight; j++)
                {
                    Blocks[i * tetrominoWidthHeight + j] = new Block(deck.Width / 2 - tetrominoWidthHeight / 2 + i, j - tetrominoWidthHeight);
                }
            }
            SpecifyVisibleBlocks();
        }

        /// <summary>
        /// Calculates the tetromino blocks in 0,0 postion for next tetromino event handler
        /// </summary>
        /// <returns></returns>
        private Block[] CalculateBaseBlocks()
        {
            var blockIndex = 0;
            var blockList = new List<Block>();
            for (var i = 0; i < tetrominoWidthHeight; i++)
            {
                for (var j = 0; j < tetrominoWidthHeight; j++)
                {
                    if (Blocks[blockIndex].Status != BlockStatus.Hidden)
                    {
                        blockList.Add(new Block(i, j, Blocks[blockIndex].Status));
                    }
                    blockIndex++;
                }
            }
            return blockList.ToArray();
        }

        /// <summary>
        /// Checks for game over 
        /// </summary>
        /// <returns></returns>
        private bool GameOver()
        {
            foreach (Block block in VisibleBlocks)
            {
                if (deck.GameOver(block.Y)) return true;
            }
            return false;
        }

        /// <summary>
        /// Moves the tetromino to right and then rotates it and return move right and rotate changed blocks. If it can't rotate after move right, then it rolls back the move with a move left
        /// </summary>
        /// <returns></returns>
        private ChangeResult[] MoveRightAndRotate()
        {
            var changeResults = new List<ChangeResult>
            {
                MoveRight()
            };
            if (CanRotate())
            {
                changeResults.AddRange(Rotate());
                return changeResults.ToArray();
            }
            MoveLeft();
            return new ChangeResult[0];
        }

        /// <summary>
        /// Moves the tetromino to left and then rotates it and return move left and rotate changed blocks. If it can't rotate after move left, then it rolls back the move with a move right
        /// </summary>
        /// <returns></returns>
        private ChangeResult[] MoveLeftAndRotate()
        {
            var changeResults = new List<ChangeResult>
            {
                MoveLeft()
            };
            if (CanRotate())
            {
                changeResults.AddRange(Rotate());
                return changeResults.ToArray();
            }
            MoveRight();
            return new ChangeResult[0];
        }

        /// <summary>
        /// Rotates the tetromino
        /// </summary>
        private void RotateTetromino()
        {
            var counter = 0;
            var sPoint = tetrominoWidthHeight - 1;
            var tempBlocks = new Block[Blocks.Length];
            var index = sPoint;
            for (var i = 0; i < tetrominoWidthHeight; i++)
            {
                for (var j = 0; j < tetrominoWidthHeight; j++)
                {
                    tempBlocks[counter] = new Block(Blocks[counter], Blocks[index].Status);
                    counter++;
                    index += tetrominoWidthHeight;
                }
                sPoint--;
                index = sPoint;
            }

            for (var i = 0; i < Blocks.Length; i++)
            {
                Blocks[i] = tempBlocks[i];
            }
        }

        #endregion

        #region Ctor

        /// <summary>
        /// Creates a new instance of the tetromino
        /// </summary>
        /// <param name="deck">Deck which this tetromino belongs to</param>
        /// <param name="tetrominoWidthHeight">tetromino width height</param>
        public Tetromino(Deck deck, byte tetrominoWidthHeight)
        {
            this.deck = deck;
            this.tetrominoWidthHeight = tetrominoWidthHeight;
            CreateBlocks();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Visible blocks of tetromino
        /// </summary>
        public Block[] VisibleBlocks { get { return Blocks.Where(s => s.Status != BlockStatus.Hidden).ToArray(); } }

        /// <summary>
        /// Base blocks of tetromino in the postion of 0,0 for next tetromino event handler
        /// </summary>
        public Block[] BaseBlocks { get { return CalculateBaseBlocks(); } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Checks whether tetromino can move down
        /// </summary>
        /// <returns></returns>
        public bool CanMoveDown()
        {
            foreach (var block in VisibleBlocks)
            {
                if (deck.Collision(block.X, block.Y + 1)) return false;
            }
            return true;
        }

        /// <summary>
        /// Moves the tetromino to right if it's possible and return the changed blocks after movement
        /// </summary>
        /// <returns></returns>
        public ChangeResult MoveRight()
        {
            if (!CanMoveRight()) return null;

            var inversedBlocks = InverseVisibleBlocks();
            foreach (var block in Blocks)
            {
                block.MoveRight();
            }
            var changedBlocks = AddVisibleBlocksToChangedBlocks(inversedBlocks);
            return new ChangeResult { ChangedBlocks = changedBlocks };
        }

        /// <summary>
        /// Moves the tetromino to left if it's possible and return the changed blocks after movement
        /// </summary>
        /// <returns></returns>
        public ChangeResult MoveLeft()
        {
            if (!CanMoveLeft()) { return null; }

            var inversedBlocks = InverseVisibleBlocks();

            foreach (var block in Blocks)
            {
                block.MoveLeft();
            }

            var changedBlocks = AddVisibleBlocksToChangedBlocks(inversedBlocks);
            return new ChangeResult() { ChangedBlocks = changedBlocks };
        }

        /// <summary>
        /// Moves the tetromino to down if it's possible and return the changed blocks after movement
        /// If it is not possible to move down, checks for game over and if game has not been over, fixes the tetromino in deck and checks for rows to vanish
        /// </summary>
        /// <returns></returns>
        public MoveDownResult MoveDown()
        {
            if (!CanMoveDown())
            {
                if (GameOver())
                {
                    return new MoveDownResult { GameOver = true };
                }
                deck.FixBlocks(VisibleBlocks);
                var currentTetrominosRows = VisibleBlocks.Select(s => s.Y).Distinct().ToArray();
                var vanishRowResult = deck.VanishRows(currentTetrominosRows);
                return new MoveDownResult
                {
                    VanishRowResult = vanishRowResult
                };
            }
            else
            {
                var inversedBlocks = InverseVisibleBlocks();
                foreach (var block in Blocks)
                {
                    block.MoveDown();
                }
                var changedBlocks = AddVisibleBlocksToChangedBlocks(inversedBlocks);
                return new MoveDownResult { ChangedBlocks = changedBlocks };
            }
        }

        /// <summary>
        /// Rotates the tetromino if it's possible and return the changed blocks after rotation.It may moves the tetromino to left or right if rotation is not possible 
        /// </summary>
        /// <returns></returns>
        public virtual ChangeResult[] Rotate()
        {
            if (!CanRotate())
            {
                var canMoveRight = CanMoveRight();
                var canMoveLeft = CanMoveLeft();

                if (canMoveRight && !canMoveLeft)
                {
                    return MoveRightAndRotate();
                }

                if (!canMoveRight && canMoveLeft)
                {
                    return MoveLeftAndRotate();
                }
                return new ChangeResult[0];
            }
            var inversedBlocks = InverseVisibleBlocks();
            RotateTetromino();
            var changedBlocks = AddVisibleBlocksToChangedBlocks(inversedBlocks);
            return new[] { new ChangeResult() { ChangedBlocks = changedBlocks } };
        }

        /// <summary>
        /// Moves the tetromino in top middle of the deck
        /// </summary>
        public void ResetToTopMiddle()
        {
            for (var i = 0; i < tetrominoWidthHeight; i++)
            {
                for (var j = 0; j < tetrominoWidthHeight; j++)
                {
                    Blocks[i * tetrominoWidthHeight + j].X = deck.Width / 2 - tetrominoWidthHeight / 2 + i;
                    Blocks[i * tetrominoWidthHeight + j].Y = j - tetrominoWidthHeight;                    
                }
            }
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Blocks of the tetromino 
        /// </summary>
        protected Block[] Blocks { get; private set; }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Checks whether tetromino can move left
        /// </summary>
        /// <returns></returns>
        protected bool CanMoveLeft()
        {
            foreach (var block in VisibleBlocks)
            {
                if (deck.Collision(block.X - 1, block.Y)) return false;
            }
            return true;
        }

        /// <summary>
        /// Checks whether tetromino can move right
        /// </summary>
        /// <returns></returns>
        protected bool CanMoveRight()
        {
            foreach (var block in VisibleBlocks)
            {
                if (deck.Collision(block.X + 1, block.Y)) return false;
            }
            return true;
        }

        /// <summary>
        /// Checks whether tetromino can rotate
        /// </summary>
        /// <returns></returns>
        protected bool CanRotate()
        {
            var cnt = 0;
            var sPoint = tetrominoWidthHeight - 1;
            var index = sPoint;
            for (var i = 0; i < tetrominoWidthHeight; i++)
            {
                for (var j = 0; j < tetrominoWidthHeight; j++)
                {
                    if (Blocks[index].Status != BlockStatus.Hidden && deck.Collision(Blocks[cnt].X, Blocks[cnt].Y))
                    {
                        return false;
                    }
                    cnt++;
                    index += tetrominoWidthHeight;
                }
                sPoint--;
                index = sPoint;
            }
            return true;
        }

        /// <summary>
        /// This method is for specyfing tetromino visible blocks in derived classes
        /// </summary>
        protected abstract void SpecifyVisibleBlocks();

        #endregion

    }
}
