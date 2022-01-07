/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Thursday, 30 December 2021
 * Time: 15:59:17
 ***/

using System;
using System.Collections.Generic;

namespace DarcyStudio.Task
{
    [Obsolete]
    public static class TaskUtils
    {
        public static Driver CreateDriver (IEnumerator<ITask> enumerator, Driver.ExecuteFinish callBack,
            bool                                              disposable = true)
        {
            return new Driver (enumerator, callBack, disposable);
        }
    }
}