namespace Tetris.Game.Results
{

    /// <summary>
    /// This class is the result of move down operation
    /// </summary>
    internal class MoveDownResult
    {

        #region Public Properties

        /// <summary>
        /// Indicates whether the game is over
        /// </summary>
        public bool GameOver { get; set; }

        /// <summary>
        /// Changed blocks after move down
        /// </summary>
        public Block[] ChangedBlocks { get; set; }

        /// <summary>
        /// Vanish row result in case of move down led to a vanish operation
        /// </summary>
        public VanishRowResult VanishRowResult { get; set; }

        /// <summary>
        /// The next tetromino in case of move down reached the bottom of the deck or collided with a visible block
        /// </summary>
        public Block[] NextTetromino { get; set; }

        #endregion

    }
}
