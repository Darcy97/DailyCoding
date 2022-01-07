/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Friday, 07 January 2022
 * Time: 11:24:45
 ***/

namespace DarcyStudio.SequenceTaskWithUniTask
{
    /// <typeparam name="T"> Task 类型 枚举 </typeparam>
    public interface ITaskFactory<in T>
    {
        ISequenceTask Create (T type);
    }

    /// <typeparam name="T1"> Task 类型 枚举 </typeparam>
    /// <typeparam name="T2"> 参数 </typeparam>
    public interface ITaskFactory<in T1, in T2>
    {
        ISequenceTask Create (T1 type, T2 para);
    }
}