/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Thursday, 30 December 2021
 * Time: 14:50:09
 * Description: Task 生成器
 ***/

namespace DarcyStudio.Task
{
    /// <typeparam name="T"> Task 类型 枚举 </typeparam>
    public interface ITaskFactory<in T>
    {
        ITask Create (T type);
    }

    /// <typeparam name="T1"> Task 类型 枚举 </typeparam>
    /// <typeparam name="T2"> 参数 </typeparam>
    public interface ITaskFactory<in T1, in T2>
    {
        ITask Create (T1 type, T2 para);
    }
}