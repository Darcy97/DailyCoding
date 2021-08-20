/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月17日 星期二
 * Time: 下午9:46:30
 ***/

using DarcyStudio.GameComponent.TimeLine.ForAction;

namespace DarcyStudio.GameComponent.TimeLine.WorkState
{
    public interface IRequireWaitDone
    {
        bool Require ();
        void SetWaitDoneListener (IWorkStateListener listener);
    }
}