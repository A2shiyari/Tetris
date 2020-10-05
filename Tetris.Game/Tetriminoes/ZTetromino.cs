namespace Tetris.Game.Tetriminoes
{

    /// <summary>
    /// Tetromino Z class. Z tetromino is a 3*3 matrix 
    /// </summary>
    internal class ZTetromino : Tetromino
    {

        #region Ctor

        /// <summary>
        /// Creates a new instance of the tetromino
        /// </summary>
        /// <param name="deck">Deck which tetromino belongs to</param>
        public ZTetromino(Deck deck) : base(deck, 3) { }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Specifies tetromino visible blocks for Z shape
        /// </summary>
        protected override void SpecifyVisibleBlocks()
        {

            /*  
             '0' '3' '6'
             'Z' 'Z' '7'
             '2' 'Z' 'Z'
            */

            Blocks[1].Status = BlockStatus.Red;
            Blocks[4].Status = BlockStatus.Red;
            Blocks[5].Status = BlockStatus.Red;
            Blocks[8].Status = BlockStatus.Red;
        }

        #endregion

    }
}
