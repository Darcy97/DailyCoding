/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月24日 星期二
 * Time: 下午5:18:32
 ***/

using System;
using DarcyStudio.GameComponent.TimeLine.DemandObject;
using DarcyStudio.GameComponent.TimeLine.RequireObject;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.Actor
{
    public interface IBoneOwner
    {
        BoneInfo GetBoneInfo (string   key);
        IObject  GetBoneObject (string key);
    }

    [Serializable]
    public class BoneInfo
    {
        public string     key;
        public GameObject Go;

        private IObject _object;

        public IObject GetBoneObject ()
        {
            if (Go == null)
            {
                Log.Error ("Bone config error");
                return InvalidObject.Default;
            }

            if (_object == null)
                _object = Go.GetComponent<IObject> ();

            if (_object == null)
            {
                _object = Go.AddComponent<DemandObject.DemandObject> ();
            }

            return _object;
        }
    }
}