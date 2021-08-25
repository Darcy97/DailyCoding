/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月24日 星期二
 * Time: 下午6:03:34
 ***/

using System.Collections.Generic;
using DarcyStudio.GameComponent.TimeLine.DemandObject;
using DarcyStudio.GameComponent.TimeLine.ForAction;
using DarcyStudio.GameComponent.TimeLine.ForAction.Sender;
using DarcyStudio.GameComponent.TimeLine.RequireObject;
using DarcyStudio.GameComponent.TimeLine.State;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.Actor
{
    public class ActorObject : DemandObject.DemandObject, IBoneOwner, IDirectionOwner, IActionStatusOwner
    {
        [SerializeField] private BoneInfo[] boneConfigs;

        private Dictionary<string, BoneInfo> _boneConfigDict;

        private void Awake ()
        {
            if (boneConfigs == null || boneConfigs.Length < 1)
                return;
            _boneConfigDict = new Dictionary<string, BoneInfo> (boneConfigs.Length);
            foreach (var info in boneConfigs)
            {
                if (_boneConfigDict.ContainsKey (info.key))
                {
                    Log.Error ("Dont allow multi config with same key: {0} in \"{1}\"", info.key, transform.GetPath ());
                    return;
                }

                _boneConfigDict.Add (info.key, info);
            }
        }

        public BoneInfo GetBoneInfo (string key)
        {
            if (_boneConfigDict == null)
                return null;
            return _boneConfigDict.ContainsKey (key) ? _boneConfigDict[key] : null;
        }

        public IObject GetBoneObject (string key)
        {
            var boneInfo = GetBoneInfo (key);
            return boneInfo == null ? InvalidObject.Default : boneInfo.GetBoneObject ();
        }

        private Direction _direction;

        public void SetDirection (Direction direction)
        {
            _direction = direction;
        }

        public Direction GetDirection () => _direction;


        private ActionType _status;

        public void SetStatus (ActionType status)
        {
            _status = status;
        }

        public ActionType GetStatus () => _status;

    }
}