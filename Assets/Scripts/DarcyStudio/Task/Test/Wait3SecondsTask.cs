/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Thursday, 30 December 2021
 * Time: 15:31:55
 ***/

using DarcyStudio.GameComponent.Tools;

namespace DarcyStudio.Task.Test
{
    public class Wait3SecondsTask : ITask
    {
        private bool _isFinish;

        private Logger _logger;

        public Wait3SecondsTask (Logger logger)
        {
            _logger = logger;
        }

        public void Execute ()
        {
            _isFinish = false;
            _logger.AddLog ("Wait 3 seconds ...");
            YieldUtils.DelayActionWithOutContext (3, () => _isFinish = true);
        }

        public bool IsBlock ()             => true;
        public bool IsFinish ()            => _isFinish;
        public bool InterruptSubsequent () => false;

    }
}