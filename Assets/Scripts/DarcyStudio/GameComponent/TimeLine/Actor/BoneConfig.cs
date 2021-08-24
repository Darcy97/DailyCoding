/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月24日 星期二
 * Time: 下午5:05:04
 ***/

using System;
using System.Collections.Generic;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.Actor
{
    // public class BoneConfig : MonoBehaviour, IBoneOwner
    // {
    //     [SerializeField] private BoneInfo[] boneConfigs;
    //
    //     private Dictionary<string, BoneInfo> _boneConfigDict;
    //
    //     private void Awake ()
    //     {
    //         if (boneConfigs == null || boneConfigs.Length < 1)
    //             return;
    //         _boneConfigDict = new Dictionary<string, BoneInfo> (boneConfigs.Length);
    //         foreach (var info in boneConfigs)
    //         {
    //             if (_boneConfigDict.ContainsKey (info.key))
    //             {
    //                 Log.Error ("Dont allow multi config with same key: {0} in \"{1}\"", info.key, transform.GetPath ());
    //             }
    //         }
    //     }
    //
    //     public BoneInfo GetBoneInfo (string key)
    //     {
    //         if (_boneConfigDict == null)
    //             return null;
    //         return _boneConfigDict.ContainsKey (key) ? _boneConfigDict[key] : null;
    //     }
    // }


    
}