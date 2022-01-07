/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Friday, 07 January 2022
 * Time: 11:57:38
 ***/

using System;

namespace DarcyStudio.SequenceTaskWithUniTask.Test
{
    public class LogTaskFactory : ITaskFactory<LogTaskType>
    {

        public ISequenceTask Create (LogTaskType type)
        {
            switch (type)
            {
                case LogTaskType.Hello:
                    return new LogHello ();
                case LogTaskType.Hi:
                    return new LogHi ();
                default:
                    return new LogHello ();
            }
        }
    }

    public enum LogTaskType
    {
        Hello,
        Hi,
    }

}