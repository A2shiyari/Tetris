namespace Tetris.Game.Tetriminoes
{

    /// <summary>
    /// Tetromino T class. T tetromino is a 3*3 matrix
    /// </summary>
    internal class TTetromino : Tetromino
    {

        #region Ctor

        /// <summary>
        /// Creates a new instance of the tetromino
        /// </summary>
        /// <param name="deck">Deck which tetromino belongs to</param>
        public TTetromino(Deck deck) : base(deck, 3) { }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Specifies tetromino visible blocks for T shape
        /// </summary>
        protected override void SpecifyVisibleBlocks()
        {

            /*  
             '0' '3' '6'
             'T' 'T' 'T'
             '2' 'T' '8'
            */

            Blocks[1].Status = BlockStatus.Magenta;
            Blocks[4].Status = BlockStatus.Magenta;
            Blocks[5].Status = BlockStatus.Magenta;
            Blocks[7].Status = BlockStatus.Magenta;
        }

        #endregion

    }
}
