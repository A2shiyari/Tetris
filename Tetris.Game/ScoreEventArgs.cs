using System;

namespace Tetris.Game
{

    /// <summary>
    /// The event args of the score event handler of tetris game
    /// </summary>
    public class ScoreEventArgs : EventArgs
    {

        #region Ctor

        /// <summary>
        /// Creates a new instance of the class
        /// </summary>
        /// <param name="score">Game score</param>
        /// <param name="lines">Vanished lines</param>
        /// <param name="level">Game level</param>
        public ScoreEventArgs(int score, int lines, Level level)
        {
            Score = score;
            Lines = lines;
            Level = level;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Game score
        /// </summary>
        public int Score { get; private set; }

        /// <summary>
        /// Vanished lines
        /// </summary>
        public int Lines { get; private set; }

        /// <summary>
        /// Game level
        /// </summary>
        public Level Level { get; private set; }

        #endregion

    }
}
