using System.Collections.Generic;

namespace Tetris.Game.Results
{

    /// <summary>
    /// This class is the result of hold operation
    /// </summary>
    internal class HoldResult : ChangeResult
    {

        #region Public Properties

        /// <summary>
        /// The hold tetromino blocks
        /// </summary>
        public Block[] HoldBlocks { get; set; }

        /// <summary>
        /// The next tetrominoes in case of hold changes the next tetrominoes in first hold
        /// </summary>
        public List<Block[]> NextTetrominoes { get; set; }


        #endregion

    }
}
