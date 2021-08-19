/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月19日 星期四
 * Time: 下午8:51:47
 * 
 ***/

using System;

namespace DarcyStudio.GameComponent.TimeLine.ForAction
{
    [Serializable]
    public class ActionData
    {
        public ActionType Type;
        public float      Para1;
        public float      Para2;
        public float      Para3;
    }

    public enum ActionType
    {
        Default,
        Shoot,
        Hit
    }
}