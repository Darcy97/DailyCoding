/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月17日 星期二
 * Time: 下午9:01:01
 * Description: Description
 ***/

using System;

namespace DarcyStudio.GameComponent.TimeLine.ForAction
{
    public interface IActionReceiver
    {
        void Do (ActionData actionData, Action finishCallback = null);
    }
}