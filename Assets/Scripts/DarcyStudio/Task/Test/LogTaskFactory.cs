/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Thursday, 30 December 2021
 * Time: 15:24:14
 ***/

using DarcyStudio.GameComponent.Tools;

namespace DarcyStudio.Task.Test
{
    public class LogTaskFactory : ITaskFactory<LogTaskType, Logger>
    {
        public ITask Create (LogTaskType type, Logger logger)
        {
            switch (type)
            {
                case LogTaskType.LogHello:
                    return new LogHelloTask (logger);
                case LogTaskType.LogHi:
                    return new LogHiTask (logger);
                case LogTaskType.LogGoodbye:
                    return new LogGoodbyeTask (logger);
                case LogTaskType.Wait3Seconds:
                    return new Wait3SecondsTask (logger);
                case LogTaskType.Invalid:
                    Log.Error ($"Un fix type: {type.ToString ()}");
                    return new InvalidLogTask (type, logger);
                case LogTaskType.Interrupt:
                    return new InterruptLogTask (logger);
                default:
                {
                    Log.Error ($"Un fix type: {type.ToString ()}");
                    return new InvalidLogTask (type, logger);
                }
            }
        }
    }

    public enum LogTaskType
    {
        Invalid,
        LogHello,
        LogHi,
        LogGoodbye,
        Wait3Seconds,
        Interrupt
    }
}