using System;

namespace Tetris.Game
{

    /// <summary>
    /// Block Event Args class is used for block related event handlers
    /// </summary>
    public class BlockEventArgs : EventArgs
    {

        #region Ctor

        /// <summary>
        /// Creates new instance of BlockEventArgs
        /// </summary>
        /// <param name="blocks"></param>
        public BlockEventArgs(Block[] blocks)
        {
            Blocks = blocks;
        }

        #endregion

        #region public Properties

        /// <summary>
        /// Blocks
        /// </summary>
        public Block[] Blocks { get; private set; }

        #endregion

    }
}
