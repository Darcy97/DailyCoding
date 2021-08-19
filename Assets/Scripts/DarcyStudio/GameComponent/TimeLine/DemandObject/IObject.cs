/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月18日 星期三
 * Time: 下午3:10:02
 * Description: IObject
 ***/

using System;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.RequireObject
{
    public interface IObject
    {
        bool       IsValid ();
        GameObject GetGameObject ();
        Vector3    GetPos ();
        Transform  GetTransform ();
        T          GetComponent<T> ();
    }
}