using System;
using System.Collections.Generic;
using System.Linq;
using Tetris.Game.Results;

namespace Tetris.Game
{

    /// <summary>
    /// Deck is a n*m matrix which encapsulates tetris deck related operations
    /// </summary>
    internal class Deck
    {

        #region Private Variables

        /// <summary>
        /// Deck blocks
        /// </summary>
        private BlockStatus[,] Blocks;

        /// <summary>
        /// The position of lowest row wich has a visible block
        /// This variable holds data to prevent looping all rows
        /// </summary>
        private int lowestRowPostion;

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the rows which it's whole blocks are visible to vanish
        /// </summary>
        /// <param name="rowsToScan"></param>
        /// <returns></returns>
        private int[] GetRowsToVanish(int[] rowsToScan)
        {
            var rowsToVanish = new List<int>();
            foreach (var row in rowsToScan)
            {
                var flag = true;
                for (var i = 0; i < Width; i++)
                {
                    if (Blocks[i, row] == BlockStatus.Hidden)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    rowsToVanish.Add(row);
                }
            }
            return rowsToVanish.ToArray();
        }

        /// <summary>
        /// Gets the blocks in the the rows to vanish
        /// </summary>
        /// <param name="rowsToVanish"></param>
        /// <returns></returns>
        private Block[] GetVanishedBlocks(int[] rowsToVanish)
        {
            var changedBlocks = new List<Block>();
            foreach (var row in rowsToVanish)
            {
                for (var i = 0; i < Width; i++)
                {
                    changedBlocks.Add(new Block(i, row));
                }
            }
            return changedBlocks.ToArray();
        }

        /// <summary>
        /// Falls down the blocks above the given row in the deck and returns the changed blocks after fall down
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private Block[] FallDownBlocksByRow(int row)
        {
            var changedBlocks = new List<Block>();
            for (var i = row; i >= lowestRowPostion; i--)
            {
                for (var j = 0; j < 10; j++)
                {
                    if (i == 0)
                    {
                        if (Blocks[j, i] == BlockStatus.Visible)
                        {
                            Blocks[j, i] = BlockStatus.Hidden;
                            changedBlocks.Add(new Block(j, i));
                        }
                    }
                    else
                    {
                        if (Blocks[j, i] != Blocks[j, i - 1])
                        {
                            Blocks[j, i] = Blocks[j, i - 1];
                            changedBlocks.Add(new Block(j, i, Blocks[j, i]));
                        }
                    }
                }
            }
            lowestRowPostion++;
            return changedBlocks.ToArray();
        }

        #endregion

        #region Ctor

        /// <summary>
        /// Creates a new instance of the deck class 
        /// </summary>
        /// <param name="width">deck width</param>
        /// <param name="height">deck height</param>
        public Deck(int width, int height)
        {
            Width = width;
            Height = height;
            lowestRowPostion = height;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Deck width
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Deck height
        /// </summary>
        public int Height { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes the deck with given width and height and return the deck initial blocks
        /// </summary>
        /// <returns></returns>
        public Block[] Initialize()
        {
            Blocks = new BlockStatus[Width, Height];
            var initDeckBlocks = new Block[Width * Height];
            var blockIndex = 0;
            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    initDeckBlocks[blockIndex++] = new Block(i, j);
                }
            }
            return initDeckBlocks;
        }

        /// <summary>
        /// Fixes the given blocks in deck. 
        /// </summary>
        /// <param name="blocks"></param>
        public void FixBlocks(Block[] blocks)
        {
            foreach (var block in blocks)
            {
                Blocks[block.X, block.Y] = BlockStatus.Visible;
            }
            var tempLowestY = blocks.Min(s => s.Y);
            if (tempLowestY < lowestRowPostion)
                lowestRowPostion = tempLowestY;
        }

        /// <summary>
        /// Vanishes the given rows in deck and returns changed blocks
        /// </summary>
        /// <param name="currentRows"></param>
        /// <returns></returns>
        public VanishRowResult VanishRows(int[] currentRows)
        {
            var rowsToVanish = GetRowsToVanish(currentRows);
            if (rowsToVanish.Length == 0) return null;

            var vanishedBlocks = GetVanishedBlocks(rowsToVanish);
            Array.Sort(rowsToVanish);

            var changedBlocks = new List<Block>();
            foreach (var row in rowsToVanish)
            {
                var localChangedBlocks = FallDownBlocksByRow(row);

                foreach (var block in localChangedBlocks)
                {
                    if (changedBlocks.Contains(block))
                    {
                        changedBlocks.Remove(block);
                    }
                }
                changedBlocks.AddRange(localChangedBlocks);
            }

            return new VanishRowResult
            {
                ChangedBlocks = changedBlocks.ToArray(),
                VanishedBlocks = vanishedBlocks,
                VanishedRowCount = rowsToVanish.Length
            };
        }

        /// <summary>
        /// Indicates whether game is over based on the input y position
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool GameOver(int y)
        {
            return y < 0;
        }

        /// <summary>
        /// Indicates whether given x and y position collides with a visible block in the deck or exceeds deck borders 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Collision(int x, int y)
        {
            if (x < 0 || x == Width || y == Height) return true;
            if (y < 0)
            {
                return false;
            }
            return Blocks[x, y] == BlockStatus.Visible;
        }

        #endregion

    }
}
