namespace Tetris.Game
{

    /// <summary>
    /// Block class is for storing deck cell's data
    /// </summary>
    public class Block
    {

        #region Ctor

        /// <summary>
        /// Creates a new instance of the block with default hidden status
        /// </summary>
        /// <param name="x">X postion of the block</param>
        /// <param name="y">Y postion of the block</param>
        public Block(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Creates a new instance of the block
        /// </summary>
        /// <param name="x">X postion of the block</param>
        /// <param name="y">X postion of the block</param>
        /// <param name="status">Status of the block</param>
        public Block(int x, int y, BlockStatus status)
        {
            X = x;
            Y = y;
            Status = status;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// X postion of the block in deck
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// Y position of the block in deck
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        /// Status of the block in deck
        /// </summary>
        public BlockStatus Status { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Moves the block to right in deck
        /// </summary>
        public void MoveRight()
        {
            X++;
        }

        /// <summary>
        /// Moves the block to left in deck
        /// </summary>
        public void MoveLeft()
        {
            X--;
        }

        /// <summary>
        /// Moves the block to down in deck
        /// </summary>
        public void MoveDown()
        {
            Y++;
        }

        /// <summary>
        /// Indicates whether two blocks are equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Block inputBlock)) return false;

            return inputBlock.X == X && inputBlock.Y == Y;

        }

        /// <summary>
        /// Gets the block hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

    }
}
