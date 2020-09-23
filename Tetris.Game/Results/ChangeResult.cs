namespace Tetris.Game.Results
{

    /// <summary>
    /// This class is the result of changes in the deck blocks
    /// </summary>
    internal class ChangeResult
    {

        #region Public Properties

        /// <summary>
        /// The changed blocks
        /// </summary>
        public Block[] ChangedBlocks { get; set; }

        /// <summary>
        /// The ghost blocks
        /// </summary>
        public Block[] GhostBlocks { get; set; }

        /// <summary>
        /// Indicates whether it was the last move of the current tetromino before locking
        /// </summary>
        public bool LastMove { get; set; }

        #endregion

    }
}
