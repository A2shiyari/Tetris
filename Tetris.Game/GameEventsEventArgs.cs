using System;

namespace Tetris.Game
{
    /// <summary>
    /// Game Events Event Args class is used for game events event handler
    /// </summary>
    public class GameEventsEventArgs : EventArgs
    {

        #region Ctor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameEvent"></param>
        public GameEventsEventArgs(GameEvent gameEvent)
        {
            Event = gameEvent;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Occured event in the game
        /// </summary>
        public GameEvent Event { get; set; } 

        #endregion

    }
}