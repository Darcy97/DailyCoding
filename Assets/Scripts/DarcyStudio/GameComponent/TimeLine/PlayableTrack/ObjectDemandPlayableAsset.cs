/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月18日 星期三
 * Time: 下午3:01:44
 ***/

using System.Collections.Generic;
using DarcyStudio.GameComponent.TimeLine.Actor;
using DarcyStudio.GameComponent.TimeLine.DemandObject;
using DarcyStudio.GameComponent.TimeLine.RequireObject;
using UnityEngine;
using UnityEngine.Playables;

namespace DarcyStudio.GameComponent.TimeLine.PlayableTrack
{
    public abstract class ObjectDemandPlayableAsset : PlayableAsset, IObjectDemander
    {
        [SerializeField] private List<ObjectDemandInfo> objectDemandInfos;

        private IObjectProvider _provider;

        public void SetProvider (IObjectProvider provider)
        {
            _provider = provider;
        }

        public IObject GetObject (DemandType type)
        {
            var demandInfo = GetDemandInfoByDemandType (type);

            if (demandInfo == null)
            {
                return InvalidObject.Default;
            }

            if (_provider == null)
                return InvalidObject.Default;

            var demandObject = demandInfo.ObjectType == ObjectType.Specify
                ? demandInfo.GetObject ()
                : _provider.Get (demandInfo.ObjectType);

            if (!(demandObject is IBoneOwner iBoneOwner) || string.IsNullOrEmpty (demandInfo.BoneKey))
                return demandObject;

            var bone = iBoneOwner.GetBoneObject (demandInfo.BoneKey);
            return bone.IsValid () ? bone : demandObject;
        }

        private static bool InEditor ()
        {
#if UNITY_EDITOR
            return !Application.isPlaying;
#endif
            return false;
        }

        private ObjectDemandInfo GetDemandInfoByDemandType (DemandType type)
        {
            return objectDemandInfos?.Find (info => info.DemandType == type);
        }

        protected abstract ObjectDemandPlayableBehaviour CreateBehaviour ();

        protected abstract Playable CreatePlayable (
            ObjectDemandPlayableBehaviour bhv, PlayableGraph graph);

        public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
        {
            var bhv = CreateBehaviour ();
            bhv.SetPlayableAsset (this);
            return CreatePlayable (bhv, graph);
        }
    }
}