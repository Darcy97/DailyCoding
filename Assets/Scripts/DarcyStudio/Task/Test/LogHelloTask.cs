/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Thursday, 30 December 2021
 * Time: 15:25:27
 ***/

using DarcyStudio.GameComponent.Tools;
using UnityEngine.UI;

namespace DarcyStudio.Task.Test
{
    public class LogHelloTask : ITask
    {
        private bool _isFinish;

        private readonly Logger _logger;

        public LogHelloTask (Logger logger)
        {
            _logger = logger;
        }

        public void Execute ()
        {
            _logger.AddLog ("Hello");
            _isFinish = false;
            YieldUtils.DelayActionWithOutContext (1, () => _isFinish = true);
        }

        public bool IsBlock ()             => true;
        public bool IsFinish ()            => _isFinish;
        public bool InterruptSubsequent () => false;

    }
}