namespace Tetris.Game.Tetriminoes
{

    /// <summary>
    /// Tetromino L class. L tetromino is a 3*3 matrix 
    /// </summary>
    internal class LTetromino : Tetromino
    {

        #region Ctor

        /// <summary>
        /// Creates a new instance of the tetromino
        /// </summary>
        /// <param name="deck">Deck which tetromino belongs to</param>
        public LTetromino(Deck deck) : base(deck, 3) { }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Specifies tetromino visible blocks for L shape
        /// </summary>
        protected override void SpecifyVisibleBlocks()
        {

            /*  
              '0' 'L' '6'
              '1' 'L' '7'
              '2' 'L' 'L'
            */

            Blocks[3].Status = BlockStatus.Visible;
            Blocks[4].Status = BlockStatus.Visible;
            Blocks[5].Status = BlockStatus.Visible;
            Blocks[8].Status = BlockStatus.Visible;
        }

        #endregion

    }
}
