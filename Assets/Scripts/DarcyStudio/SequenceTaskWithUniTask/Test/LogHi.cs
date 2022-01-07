/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Friday, 07 January 2022
 * Time: 11:58:53
 ***/

using Cysharp.Threading.Tasks;
using DarcyStudio.GameComponent.Tools;

namespace DarcyStudio.SequenceTaskWithUniTask.Test
{
    public class LogHi : ISequenceTask
    {
        public async UniTask Execute ()
        {
            Log.Info ("Hi");


            //这部分是模拟一个异步操作，操作结束会把 finish 改为 true
            var finish = false;
            YieldUtils.DelayActionWithOutContext (3, () => finish = true);

            await UniTask.WaitUntil (() => finish);
        }

        public bool InterruptSubsequent () => false;
    }
}