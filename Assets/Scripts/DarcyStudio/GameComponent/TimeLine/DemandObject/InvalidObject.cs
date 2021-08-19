/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月18日 星期三
 * Time: 下午6:24:30
 * Description: Description
 ***/

using DarcyStudio.GameComponent.TimeLine.RequireObject;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.DemandObject
{
    public class InvalidObject : IObject
    {

        public bool IsValid () => false;

        public GameObject GetGameObject () => default;

        public Vector3 GetPos () => default;

        public Transform GetTransform () => default;

        public T GetComponent<T> () => default;

        public static readonly InvalidObject Default = new InvalidObject ();
    }
}