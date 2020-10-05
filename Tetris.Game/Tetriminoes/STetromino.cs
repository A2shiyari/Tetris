namespace Tetris.Game.Tetriminoes
{

    /// <summary>
    /// Tetromino S class. S tetromino is a 3*3 matrix
    /// </summary>
    internal class STetromino : Tetromino
    {

        #region Ctor

        /// <summary>
        /// Creates a new instance of the tetromino
        /// </summary>
        /// <param name="deck">Deck which tetromino belongs to</param>
        public STetromino(Deck deck) : base(deck, 3) { }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Specifies tetromino visible blocks for S shape
        /// </summary>
        protected override void SpecifyVisibleBlocks()
        {

            /*  
              '0' '3' '6'
              '1' 'S' 'S'
              'S' 'S' '8'
           */

            Blocks[2].Status = BlockStatus.Green;
            Blocks[4].Status = BlockStatus.Green;
            Blocks[5].Status = BlockStatus.Green;
            Blocks[7].Status = BlockStatus.Green;
        }

        #endregion

    }
}
