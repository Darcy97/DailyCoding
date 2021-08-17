/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月17日 星期二
 * Time: 下午9:01:01
 * Description: Description
 ***/

namespace DarcyStudio.GameComponent.TimeLine.ForAnimation
{
    public interface IActionReceiver
    {
        void Do (ActionType key);

        void RegisterTrigger (IActionTrigger trigger);

    }
}