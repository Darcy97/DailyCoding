/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月18日 星期三
 * Time: 下午6:46:06
 ***/

using DarcyStudio.GameComponent.TimeLine.RequireObject;
using UnityEngine;
using UnityEngine.Playables;

namespace DarcyStudio.GameComponent.TimeLine.PlayableTrack
{
    public class ObjectDemandPlayableBehaviour : PlayableBehaviour
    {
        public ObjectDemandPlayableBehaviour ()
        {
        }

        private ObjectDemandPlayableAsset _playableAsset;

        public void SetPlayableAsset (ObjectDemandPlayableAsset playableAsset)
        {
            _playableAsset = playableAsset;
        }

        protected IObject GetObject (DemandType type)
        {
            return _playableAsset.GetObject (type);
        }

        protected bool IsPlaying ()
        {
#if UNITY_EDITOR
            return Application.isPlaying;
#endif
            return true;
        }

    }
}