/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月16日 星期一
 * Time: 下午5:46:20
 * Description:
 ***/

using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine
{
    public interface IRequireTarget
    {
        void SetTarget (GameObject go);
        int  GetTargetIndex ();
    }
}