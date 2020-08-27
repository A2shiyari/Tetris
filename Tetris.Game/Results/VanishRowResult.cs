namespace Tetris.Game.Results
{

    /// <summary>
    /// This class is the result of vanish row(s) operation
    /// </summary>
    internal class VanishRowResult
    {

        #region Public Properties

        /// <summary>
        /// The blocks which are vanished
        /// </summary>
        public Block[] VanishedBlocks { get; set; }

        /// <summary>
        /// The blocks which are changed after vanishing row(s)
        /// </summary>
        public Block[] ChangedBlocks { get; set; }

        /// <summary>
        /// Number of vanished rows
        /// </summary>
        public int VanishedRowCount { get; set; }

        #endregion

    }
}
