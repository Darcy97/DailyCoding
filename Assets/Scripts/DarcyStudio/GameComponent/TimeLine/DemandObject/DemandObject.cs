/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月18日 星期三
 * Time: 下午8:37:05
 ***/

using DarcyStudio.GameComponent.TimeLine.RequireObject;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.DemandObject
{
    [DisallowMultipleComponent]
    public class DemandObject : MonoBehaviour, IObject
    {

        public bool IsValid () => true;

        public GameObject GetGameObject () => gameObject;

        public Vector3 GetPos () => gameObject.transform.position;

        public Transform GetTransform () => gameObject.transform;
    }
}