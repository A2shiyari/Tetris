using System.Collections.Generic;
using Tetris.Game.Results;

namespace Tetris.Game.Tetriminoes
{

    /// <summary>
    /// Tetromino I class. I tetromino is a 4*4 matrix
    /// </summary>
    internal class ITetromino : Tetromino
    {

        #region Private Methods

        /// <summary>
        /// Moves the teromino to right and tries to rotate. If rotation fails after first move, attemps second move and tries to rotate. If rotation is successful in any stage returns changed blocks after movement and rotation, otherwise rolls back the movements with move left
        /// </summary>
        /// <returns></returns>
        private ChangeResult[] CanMoveRightButCanNotMoveLeft()
        {
            var changeList = new List<ChangeResult>
            {
                MoveRight()
            };
            if (CanRotate())
            {
                changeList.AddRange(base.Rotate());
                return changeList.ToArray();
            }
            else if (CanMoveRight())
            {
                changeList.Add(MoveRight());

                if (CanRotate())
                {
                    changeList.AddRange(base.Rotate());
                    return changeList.ToArray();
                }

                MoveLeft();
            }
            MoveLeft();
            return new ChangeResult[0];
        }

        /// <summary>
        /// Moves the teromino to left and tries to rotate. If rotation fails after first move, attemps second move and tries to rotate. If rotation is successful in any stage returns changed blocks after movement and rotation, otherwise rolls back the movements with move right 
        /// </summary>
        /// <returns></returns>
        private ChangeResult[] CanMoveLeftButCanNotMoveRight()
        {
            var changeList = new List<ChangeResult>
            {
                MoveLeft()
            };

            if (CanRotate())
            {
                changeList.AddRange(base.Rotate());
                return changeList.ToArray();
            }
            else if (CanMoveLeft())
            {
                changeList.Add(MoveLeft());
                if (CanRotate())
                {
                    changeList.AddRange(base.Rotate());
                    return changeList.ToArray();
                }
                MoveRight();
            }
            MoveRight();
            return new ChangeResult[0];
        }

        /// <summary>
        /// Moves the tetromino to left and rotates it and returns changed blocks. If rotation fails, rolls back the move with a move right
        /// </summary>
        /// <returns></returns>
        private ChangeResult[] MoveLeftAndRotate()
        {
            var changeList = new List<ChangeResult>
            {
                MoveLeft()
            };
            var rotate = Rotate();
            if (rotate.Length == 0)
            {
                MoveRight();
                return new ChangeResult[0];
            }
            else
            {
                changeList.AddRange(rotate);
                return changeList.ToArray();
            }
        }

        /// <summary>
        /// Moves the tetromino to right and rotates it and returns changed blocks. If rotation fails, rolls back the move with a move left
        /// </summary>
        /// <returns></returns>
        private ChangeResult[] MoveRightAndRotate()
        {
            var changeList = new List<ChangeResult>
            {
                MoveRight()
            };
            var rotate = Rotate();
            if (rotate.Length == 0)
            {
                MoveLeft();
                return new ChangeResult[0];
            }
            else
            {
                changeList.AddRange(rotate);
                return changeList.ToArray();
            }
        }

        /// <summary>
        /// Tries to move tetromino to left and rotate it. if it was unbale to move to left then tries to move it to right and rotate it and finally returns changed blocks
        /// </summary>
        /// <returns></returns>
        private ChangeResult[] MoveToBordersAndRotate()
        {
            var moveLeftAndRotate = MoveLeftAndRotate();
            if (moveLeftAndRotate.Length != 0)
            {
                return moveLeftAndRotate;
            }
            return MoveRightAndRotate();
        }

        #endregion

        #region Ctor

        /// <summary>
        /// Creates a new instance of the tetromino
        /// </summary>
        /// <param name="deck">Deck which tetromino belongs to</param>
        public ITetromino(Deck deck) : base(deck, 4) { }

        #endregion

        #region Public Methods

        /// <summary>
        /// Rotates I tetromino and returns changed blocks after rotaion. If rotation is possible rotates it by base class rotation mechanism.
        /// If rotation is not possible for I tetromino, it tries to move the tetromino and rotates it. Based on I tetromino dimension, it is likely to move it twice to rotate it
        /// </summary>
        /// <returns></returns>
        public override ChangeResult[] Rotate()
        {
            if (!CanRotate())
            {
                var canMoveRight = CanMoveRight();
                var canMoveLeft = CanMoveLeft();

                if (!canMoveRight && !canMoveLeft)
                {
                    return new ChangeResult[0];
                }

                if (canMoveLeft && !canMoveRight)
                {
                    return CanMoveLeftButCanNotMoveRight();
                }

                if (!canMoveLeft && canMoveRight)
                {
                    return CanMoveRightButCanNotMoveLeft();
                }
                return MoveToBordersAndRotate();

            }
            return base.Rotate();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Specifies tetromino visible blocks for I shape
        /// </summary>
        protected override void SpecifyVisibleBlocks()
        {

            /*  
               '00' 'II' '08' '12'
               '01' 'II' '09' '13'
               '02' 'II' '10' '14'
               '03' 'II' '11' '15'
            */

            Blocks[4].Status = BlockStatus.LightBlue;
            Blocks[5].Status = BlockStatus.LightBlue;
            Blocks[6].Status = BlockStatus.LightBlue;
            Blocks[7].Status = BlockStatus.LightBlue;
        }

        #endregion

    }
}
