using System.Collections.Generic;

namespace Tetris.Game
{

    /// <summary>
    /// This class is responsible for management of scores and lines in tetris game
    /// </summary>
    internal class ScoreManagement
    {

        #region Private Constants

        private const int firstLevelLinesCount = 20;
        private const int levelsLinesIncreaseAmount = 5;
        private const int hardDropScorePlus = 100;

        #endregion

        #region Private Variables

        private int currentLevelLines;
        private int currentLevelLineTop = firstLevelLinesCount;
        private readonly Dictionary<int, int> linesScores = new Dictionary<int, int>() { { 1, 40 }, { 2, 100 }, { 3, 300 }, { 4, 1200 } };

        #endregion

        #region Private Methods

        /// <summary>
        /// Checks for level upgrad after line vanish opration
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        private Level CheckForLevelUpgrade(int lines, Level level)
        {
            currentLevelLines += lines;
            if (currentLevelLines >= currentLevelLineTop)
            {
                currentLevelLines = 0;
                currentLevelLineTop += levelsLinesIncreaseAmount;
                return level.GetNextLevel();
            }
            return level;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Game score
        /// </summary>
        public int Score { get; private set; }

        /// <summary>
        /// Game vanished lines
        /// </summary>
        public int Lines { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates game score after line vanish operation
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="level"></param>
        /// <param name="hardDrop"></param>
        /// <returns></returns>
        public Level UpdateScores(int lines, Level level, bool hardDrop)
        {
            int lineScore = linesScores[lines];

            if (hardDrop)
            {
                lineScore += hardDropScorePlus;
            }
            Score += lineScore * (int)level;
            Lines += lines;
            var result = CheckForLevelUpgrade(lines, level);
            return result;
        }

        #endregion

    }
}
