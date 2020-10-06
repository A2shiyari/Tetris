using System.Collections.Generic;

namespace Tetris.Game.Results
{

    /// <summary>
    /// This class is the result of tetromino initialization
    /// </summary>
    internal class TetrominoInitializationResult : ChangeResult
    {

        #region Public Properties 

        /// <summary>
        /// Next tetrominoes blocks
        /// </summary>
        public List<Block[]> NextTetrominoes { get; set; }

        #endregion

    }
}
