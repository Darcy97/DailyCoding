/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Thursday, 30 December 2021
 * Time: 15:25:27
 ***/

using DarcyStudio.GameComponent.Tools;

namespace DarcyStudio.Task.Test
{
    public class LogHelloTask : ITask
    {
        private bool _isFinish;

        public void Execute ()
        {
            Log.Info ("Hello");
            _isFinish = true;
        }

        public bool IsBlock ()   => true;
        public bool IsFinish ()  => _isFinish;
        public bool InterruptSubsequent () => false;

    }
}