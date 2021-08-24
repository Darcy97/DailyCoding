/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月17日 星期二
 * Time: 下午8:56:23
 * Description: Description
 ***/

using DarcyStudio.GameComponent.TimeLine.ForAction;
using DarcyStudio.GameComponent.TimeLine.State;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine
{
    public class TTT : MonoBehaviour, IActionStatusOwner
    {

        public ActionType GetStatus () => throw new System.NotImplementedException ();

        public void SetStatus (ActionType status)
        {
            throw new System.NotImplementedException ();
        }
    }
}