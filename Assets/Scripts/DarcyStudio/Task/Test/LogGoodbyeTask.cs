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

        public void Execute ()
        {
            Log.Info ("Goodbye");
            _isFinish = true;
        }

        public bool IsFinish () => _isFinish;

        public bool IsBlock () => true;
    }
}