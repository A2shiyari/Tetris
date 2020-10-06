using System.Collections.Generic;

namespace Tetris.Game.Results
{

    /// <summary>
    /// This class is the result of move down operation
    /// </summary>
    internal class MoveDownResult : ChangeResult
    {

        #region Public Properties

        /// <summary>
        /// Indicates whether the game is over
        /// </summary>
        public bool GameOver { get; set; }

        /// <summary>
        /// Vanish row result in case of move down led to a vanish operation
        /// </summary>
        public VanishRowResult VanishRowResult { get; set; }

        /// <summary>
        /// The next tetrominoes in case of move down reached the bottom of the deck or collided with a visible block
        /// </summary>
        public List<Block[]> NextTetrominoes { get; set; }

        #endregion

    }
}
