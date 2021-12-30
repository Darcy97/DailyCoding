/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Thursday, 30 December 2021
 * Time: 15:59:17
 ***/

using System.Collections.Generic;

namespace DarcyStudio.Task
{
    public static class TaskUtils
    {
        public static Driver CreateDriver (List<ITask>.Enumerator enumerator, Driver.ExecuteFinish callBack)
        {
            return new Driver (enumerator, callBack);
        }
    }
}