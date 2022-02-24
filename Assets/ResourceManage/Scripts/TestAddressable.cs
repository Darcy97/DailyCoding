/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Wednesday, 23 February 2022
 * Time: 15:21:51
 ***/

using System;
using Cysharp.Threading.Tasks;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace ResourceManage.Scripts
{
    public class TestAddressable : MonoBehaviour
    {

        [SerializeField] private Image image;

        [SerializeField] private AssetReference assetReference;

        private void Start ()
        {
            LoadImage ().Forget ();
            Log.Info ("Async ");
        }

        private async UniTaskVoid LoadImage ()
        {
            var asyncOperation = Addressables.LoadAssetAsync<Sprite> ("Icon_Avatar_Boy");

            // while (!asyncOperation.IsDone)
            // {
            //     await UniTask.WaitForEndOfFrame ();
            //     Log.Info ($"Percent: {asyncOperation.PercentComplete}");
            // }

            var sprite = await asyncOperation.Task;

            Log.Info ("Load success");
            image.sprite = sprite;
        }
    }

}