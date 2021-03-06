/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月17日 星期二
 * Time: 下午9:01:01
 * Description: Description
 ***/

using System;
using DarcyStudio.GameComponent.TimeLine.ForAction.Sender;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.Receiver
{
    public interface IActionReceiver
    {
        void Do (AttackActionConfigData attackActionConfigData, Action finishCallback = null);
    }
}