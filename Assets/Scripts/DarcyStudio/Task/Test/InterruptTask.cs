/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Thursday, 30 December 2021
 * Time: 17:20:06
 ***/

using DarcyStudio.GameComponent.Tools;

namespace DarcyStudio.Task.Test
{
    public class InterruptTask : ITask
    {

        private bool _isFinish;

        public void Execute ()
        {
            Log.Info ("Execute Interrupt task");
            _isFinish = true;
        }

        public bool IsBlock () => true;

        public bool IsFinish () => _isFinish;

        public bool InterruptSubsequent () => true;
    }
}