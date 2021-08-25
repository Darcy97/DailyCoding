/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月17日 星期二
 * Time: 下午8:41:48
 ***/

using DarcyStudio.GameComponent.TimeLine.ForAction;
using DarcyStudio.GameComponent.TimeLine.ForAction.Sender;

namespace DarcyStudio.GameComponent.TimeLine.State
{
    public interface IActionStatusOwner
    {
        void       SetStatus (ActionType status);
        ActionType GetStatus ();
    }

    public enum RoleStatus
    {
        Fall,
        Standing
    }
}