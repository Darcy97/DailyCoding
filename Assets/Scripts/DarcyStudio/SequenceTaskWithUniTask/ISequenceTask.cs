/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Friday, 07 January 2022
 * Time: 11:24:05
 ***/

using Cysharp.Threading.Tasks;

namespace DarcyStudio.SequenceTaskWithUniTask
{
    public interface ISequenceTask
    {
        /// <summary>
        /// 任务要做的事
        /// 在这个接口的实现里添加你的具体逻辑
        /// 异步接口 请用 async 声明
        /// </summary>
        UniTask Execute ();

        /// <summary>
        /// 返回 ture 则中断后面任务的执行
        /// </summary>
        /// <returns></returns>
        bool InterruptSubsequent ();
    }
}