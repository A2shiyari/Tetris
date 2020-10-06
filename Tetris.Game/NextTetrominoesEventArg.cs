using System;
using System.Collections.Generic;

namespace Tetris.Game
{

    /// <summary>
    /// Next Tetrominoes Event Args class is used for next tetromioes event handler
    /// </summary>
    public class NextTetrominoesEventArg : EventArgs
    {
        
        #region Ctor

        /// <summary>
        /// Creates a new instance of the class
        /// </summary>
        /// <param name="nextTetrominoes"></param>
        public NextTetrominoesEventArg(List<Block[]> nextTetrominoes)
        {
            NextTetrominoes = nextTetrominoes;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Next tetrominoes
        /// </summary>
        public List<Block[]> NextTetrominoes { get; private set; } 

        #endregion


    }
}
