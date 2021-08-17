/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月17日 星期二
 * Time: 下午8:41:48
 ***/

namespace DarcyStudio.GameComponent.TimeLine.State
{
    public interface IStatusOwner
    {
        RoleStatus GetStatus ();
        void       SetStatus (RoleStatus status);
    }

    public enum RoleStatus
    {
        Fall,
        Standing
    }
}