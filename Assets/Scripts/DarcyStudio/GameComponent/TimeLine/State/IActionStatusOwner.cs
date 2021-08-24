/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月17日 星期二
 * Time: 下午8:41:48
 ***/

using DarcyStudio.GameComponent.TimeLine.ForAction;

namespace DarcyStudio.GameComponent.TimeLine.State
{
    public interface IActionStatusOwner
    {
        ActionType GetStatus ();
        void       SetStatus (ActionType status);
    }

    public enum RoleStatus
    {
        Fall,
        Standing
    }
}