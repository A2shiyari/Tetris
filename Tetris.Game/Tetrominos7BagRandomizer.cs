using System;
using System.Collections.Generic;
using Tetris.Game.Tetriminoes;

namespace Tetris.Game
{

    /// <summary>
    /// This class is responsible for tetrominos randomization based on 7 bag algorithm
    /// </summary>
    internal class Tetrominos7BagRandomizer
    {


        #region Private Variables

        /// <summary>
        /// Current bag tetrominoes
        /// </summary>
        private readonly List<Tetromino> tetrominoesBag = new List<Tetromino>(7);

        /// <summary>
        /// Random generator for selecting tetromino from bag
        /// </summary>
        private readonly Random randomGenerator = new Random();

        /// <summary>
        /// Game deck
        /// </summary>
        private readonly Deck deck;

        #endregion

        #region Private Methods

        /// <summary>
        /// Fills the bag with all 7 tetrominoes
        /// </summary>
        private void FillBag()
        {
            tetrominoesBag.Add(new OTetromino(deck));
            tetrominoesBag.Add(new ITetromino(deck));
            tetrominoesBag.Add(new LTetromino(deck));
            tetrominoesBag.Add(new ZTetromino(deck));
            tetrominoesBag.Add(new STetromino(deck));
            tetrominoesBag.Add(new JTetromino(deck));
            tetrominoesBag.Add(new TTetromino(deck));
        }

        #endregion

        #region Ctor

        /// <summary>
        /// Creates a new instance of Tetrominos7BagRandomizer class 
        /// </summary>
        /// <param name="deck"></param>
        public Tetrominos7BagRandomizer(Deck deck)
        {
            this.deck = deck;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Picks a random tetromino from the bag, if bag is empty, fills the bag.
        /// </summary>
        /// <returns></returns>
        public Tetromino GetNewTetromino()
        {
            if (tetrominoesBag.Count == 0)
            {
                FillBag();
            }
            var number = randomGenerator.Next(tetrominoesBag.Count);
            var tetromino = tetrominoesBag[number];
            tetrominoesBag.RemoveAt(number);
            return tetromino;
        }

        #endregion

    }
}