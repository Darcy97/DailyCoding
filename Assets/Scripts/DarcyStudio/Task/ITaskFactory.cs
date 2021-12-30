/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Thursday, 30 December 2021
 * Time: 14:50:09
 * Description: Task 生成器
 ***/

namespace DarcyStudio.Task
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"> Task 类型 枚举 </typeparam>
    public interface ITaskFactory<in T>
    {
        ITask Create (T type);
    }
}