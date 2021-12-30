/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Thursday, 30 December 2021
 * Time: 15:30:10
 ***/

using DarcyStudio.GameComponent.Tools;

namespace DarcyStudio.Task.Test
{
    public class LogHiTask : ITask
    {

        private bool _isFinish;

        public void Execute ()
        {
            Log.Info ("Hi");
            _isFinish = true;
        }

        public bool IsBlock ()   => true;
        public bool IsFinish ()  => _isFinish;
        public bool InterruptSubsequent () => false;

    }
}