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
        private LogTaskType _type;

        public InvalidLogTask (LogTaskType type)
        {
            _type = type;
        }

        protected override void LogError ()
        {
            Log.Error ($"Execute a invalid log task --- task type: {_type.ToString ()}");
        }
    }
}