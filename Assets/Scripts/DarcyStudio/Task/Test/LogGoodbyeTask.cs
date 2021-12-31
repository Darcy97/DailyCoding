/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Thursday, 30 December 2021
 * Time: 15:30:40
 ***/

using DarcyStudio.GameComponent.Tools;

namespace DarcyStudio.Task.Test
{
    public class LogGoodbyeTask : ITask
    {

        private bool _isFinish;

        private readonly Logger _logger;

        public LogGoodbyeTask (Logger logger)
        {
            _logger = logger;
        }

        public void Execute ()
        {
            _logger.AddLog ("GoodBye");
            _isFinish = false;
            YieldUtils.DelayActionWithOutContext (1, () => _isFinish = true);
        }

        public bool IsFinish ()            => _isFinish;
        public bool InterruptSubsequent () => false;

        public bool IsBlock () => true;
    }
}