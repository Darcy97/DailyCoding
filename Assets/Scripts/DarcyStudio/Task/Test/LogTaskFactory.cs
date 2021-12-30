/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Thursday, 30 December 2021
 * Time: 15:24:14
 ***/

using DarcyStudio.GameComponent.Tools;

namespace DarcyStudio.Task.Test
{
    public class LogTaskFactory : ITaskFactory<LogTaskType>
    {
        public ITask Create (LogTaskType type)
        {
            switch (type)
            {
                case LogTaskType.LogHello:
                    return new LogHelloTask ();
                case LogTaskType.LogHi:
                    return new LogHiTask ();
                case LogTaskType.LogGoodbye:
                    return new LogGoodbyeTask ();
                case LogTaskType.Wait3Seconds:
                    return new Wait3SecondsTask ();
                default:
                {
                    Log.Error ($"Un fix type: {type.ToString ()}");
                    return new InvalidLogTask (type);
                }
            }
        }
    }

    public enum LogTaskType
    {
        Default,
        LogHello,
        LogHi,
        LogGoodbye,
        Wait3Seconds
    }
}