/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Friday, 07 January 2022
 * Time: 11:25:35
 ***/

using System.Collections.Generic;

namespace DarcyStudio.SequenceTaskWithUniTask
{
    public static class TaskUtils
    {
        public static Driver CreateDriver (IEnumerator<ISequenceTask> enumerator, Driver.ExecuteFinish callBack)
        {
            return new Driver (enumerator, callBack);
        }
    }
}