/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Thursday, 30 December 2021
 * Time: 17:20:06
 ***/

using DarcyStudio.GameComponent.Tools;

namespace DarcyStudio.Task.Test
{
    public class InterruptLogTask : ITask
    {

        private bool _isFinish;

        private readonly Logger _logger;

        public InterruptLogTask (Logger logger)
        {
            _logger = logger;
        }

        public void Execute ()
        {
            _logger.AddLog ("Interrupt ...");
            _isFinish = true;
        }

        public bool IsBlock () => true;

        public bool IsFinish () => _isFinish;

        public bool InterruptSubsequent () => true;
    }
}