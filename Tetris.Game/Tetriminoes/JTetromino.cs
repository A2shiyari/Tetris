namespace Tetris.Game.Tetriminoes
{

    /// <summary>
    /// Tetromino J class. J tetromino is a 3*3 matrix
    /// </summary>
    internal class JTetromino : Tetromino
    {

        #region Ctor

        /// <summary>
        /// Creates a new instance of the tetromino
        /// </summary>
        /// <param name="deck">Deck which tetromino belongs to</param>
        public JTetromino(Deck deck) : base(deck, 3) { }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Specifies tetromino visible blocks for J shape
        /// </summary>
        protected override void SpecifyVisibleBlocks()
        {
            
            /*  
               '0' 'J' '6'
               '1' 'J' '7'
               'J' 'J' '8'
            */

            Blocks[2].Status = BlockStatus.DarkBlue;
            Blocks[3].Status = BlockStatus.DarkBlue;
            Blocks[4].Status = BlockStatus.DarkBlue;
            Blocks[5].Status = BlockStatus.DarkBlue;
        }

        #endregion

    }
}
