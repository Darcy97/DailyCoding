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
        /// True 阻塞队列中下一个命令执行，直至 IsFinish 返回 true 才执行下一个命令
        /// False 不阻塞
        /// </summary>
        /// <returns></returns>
        bool IsBlock ();

        /// <summary>
        /// 每帧调用
        /// </summary>
        /// <returns></returns>
        bool IsFinish ();


    }
}