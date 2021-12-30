/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Thursday, 30 December 2021
 * Time: 14:49:34
 ***/

namespace DarcyStudio.Task
{
    public interface ITask
    {
        void Execute ();

        /// <summary>
        /// 是否阻塞
        /// True 阻塞队列中下一个任务执行，直至 IsFinish 返回 true 才执行下一个任务
        /// False 不阻塞
        /// </summary>
        /// <returns></returns>
        bool IsBlock ();

        /// <summary>
        /// 每帧调用
        /// </summary>
        /// <returns></returns>
        bool IsFinish ();

        /// <summary>
        /// 返回 ture 则中断后面任务的执行
        /// </summary>
        /// <returns></returns>
        bool InterruptSubsequent ();

    }
}