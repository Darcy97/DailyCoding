/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Friday, 07 January 2022
 * Time: 11:52:30
 ***/

using Cysharp.Threading.Tasks;
using DarcyStudio.GameComponent.Tools;

namespace DarcyStudio.SequenceTaskWithUniTask.Test
{
    public class LogHello : ISequenceTask
    {

        public async UniTask Execute ()
        {
            Log.Info ("Hello");


            //这部分是模拟一个异步操作，操作结束会把 finish 改为 true
            var finish = false;
            YieldUtils.DelayActionWithOutContext (2, () => finish = true);

            await UniTask.WaitUntil (() => finish);
        }

        public bool InterruptSubsequent () => false;
    }
}