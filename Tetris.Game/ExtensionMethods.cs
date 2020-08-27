namespace Tetris.Game
{

    /// <summary>
    /// Extension Methods of the game engine
    /// </summary>
    internal static class ExtensionMethods
    {

        #region Public Methods

        /// <summary>
        /// Returns level thread delay interval 
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static int GetRunningInterval(this Level level)
        {
            switch (level)
            {

                case Level.One:
                    return 500;

                case Level.Two:
                    return 450;

                case Level.Three:
                    return 400;

                case Level.Four:
                    return 300;

                case Level.Five:
                    return 200;

                case Level.Six:
                    return 150;

                case Level.Seven:
                    return 120;

                case Level.Eight:
                    return 100;

                case Level.Nine:
                    return 80;
            }
            return 60;
        }

        /// <summary>
        /// Returns next level
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static Level GetNextLevel(this Level level)
        {
            switch (level)
            {

                case Level.One:
                    return Level.Two;

                case Level.Two:
                    return Level.Three;

                case Level.Three:
                    return Level.Four;

                case Level.Four:
                    return Level.Five;

                case Level.Five:
                    return Level.Six;

                case Level.Six:
                    return Level.Seven;

                case Level.Seven:
                    return Level.Eight;

                case Level.Eight:
                    return Level.Nine;

            }
            return Level.Ten;
        }

        #endregion

    }
}
