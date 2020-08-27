namespace Tetris.Game.Results
{

    /// <summary>
    /// This class is the result of tetromino initialization
    /// </summary>
    internal class TetrominoInitializationResult
    {

        #region Public Properties

        /// <summary>
        /// Current tetromino changed blocks
        /// </summary>
        public Block[] ChangedBlocks { get; set; }

        /// <summary>
        /// Next tetromino blocks
        /// </summary>
        public Block[] NextTetromino { get; set; }

        #endregion

    }
}
