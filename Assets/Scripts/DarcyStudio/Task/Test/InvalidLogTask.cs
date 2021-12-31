/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Thursday, 30 December 2021
 * Time: 15:54:32
 ***/

using DarcyStudio.GameComponent.Tools;

namespace DarcyStudio.Task.Test
{
    public class InvalidLogTask : InvalidTask
    {
        private readonly LogTaskType _type;

        private readonly Logger _logger;
        
        public InvalidLogTask (LogTaskType type, Logger logger)
        {
            _type   = type;
            _logger = logger;
        }

        protected override void LogError ()
        {
            _logger.AddLog ($"Execute a invalid log task --- task type: {_type.ToString ()}");
        }
    }
}