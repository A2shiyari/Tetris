namespace Tetris.Game.Results
{

    /// <summary>
    /// This class is the result of tetromino initialization
    /// </summary>
    internal class TetrominoInitializationResult : ChangeResult
    {

        #region Public Properties

        /// <summary>
        /// Next tetromino blocks
        /// </summary>
        public Block[] NextTetromino { get; set; }

        #endregion

    }
}
