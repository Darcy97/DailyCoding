/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Thursday, 30 December 2021
 * Time: 15:27:37
 ***/

namespace DarcyStudio.Task
{
    public abstract class InvalidTask : ITask
    {
        private bool _isFinish;

        public void Execute ()
        {
            LogError ();
            _isFinish = true;
        }

        protected abstract void LogError ();

        public bool IsBlock ()   => false;
        public bool IsFinish ()  => _isFinish;
        public bool InterruptSubsequent () => false;

    }
}