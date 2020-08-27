using Tetris.Game.Results;

namespace Tetris.Game.Tetriminoes
{

    /// <summary>
    /// Tetromino O class. O tetromino is a 2*2 matrix
    /// </summary>
    internal class OTetromino : Tetromino
    {

        #region Ctor

        /// <summary>
        /// Creates a new instance of the tetromino
        /// </summary>
        /// <param name="deck">Deck which tetromino belongs to</param>
        public OTetromino(Deck deck) : base(deck, 2) { }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Specifies tetromino visible blocks for O shape
        /// </summary>
        protected override void SpecifyVisibleBlocks()
        {

            /*  
              'O' 'O'
              'O' 'O'
           */

            Blocks[0].Status = BlockStatus.Visible;
            Blocks[1].Status = BlockStatus.Visible;
            Blocks[2].Status = BlockStatus.Visible;
            Blocks[3].Status = BlockStatus.Visible;
        }

        /// <summary>
        /// This tetromino does not rotate
        /// </summary>
        /// <returns></returns>
        public override ChangeResult[] Rotate()
        {
            return new ChangeResult[0];
        }

        #endregion

    }
}
