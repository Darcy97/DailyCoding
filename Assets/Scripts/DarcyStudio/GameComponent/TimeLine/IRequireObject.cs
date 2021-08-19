/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月17日 星期二
 * Time: 下午8:31:03
 ***/

using DarcyStudio.GameComponent.TimeLine.RequireObject;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine
{
    public interface IRequireObject
    {
        void       SetObject (GameObject go);
        ObjectType GetRequireType ();
    }

    
}