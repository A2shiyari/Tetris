namespace Tetris.Game
{

    /// <summary>
    /// This enum indicates the status of the game
    /// </summary>
    internal enum GameStatus
    {
        /// <summary>
        /// The game has not been started yet
        /// </summary>
        None,
        /// <summary>
        /// The game has been paused
        /// </summary>
        Paused,
        /// <summary>
        /// The game is running
        /// </summary>
        Running,
        /// <summary>
        /// The game is over
        /// </summary>
        GameOver,
        /// <summary>
        /// Game is pending to restart
        /// </summary>
        RestartPending
    }
}
