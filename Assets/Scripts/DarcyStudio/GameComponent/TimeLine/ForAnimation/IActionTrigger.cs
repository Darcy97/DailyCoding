/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月17日 星期二
 * Time: 下午9:02:09
 * Description: 动画触发对象
 ***/

namespace DarcyStudio.GameComponent.TimeLine.ForAnimation
{
    public interface IActionTrigger
    {
        void OnPlayEnd ();
    }

    public enum ActionType
    {
        Default,
        Shoot,
        Hit
    }
}