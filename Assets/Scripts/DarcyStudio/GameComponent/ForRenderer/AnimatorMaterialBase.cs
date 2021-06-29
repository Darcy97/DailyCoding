/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Tuesday, 29 June 2021
 * Time: 12:25:07
 * Description:
 * 之前为了满足美术需求写的一个脚本
 * 这个脚本有点问题，有时间完善下
 * 图片的材质是共用的，所以场景里同时有多个图片时，改变一个图片的材质其他图片也会变
 ***/

using UnityEngine;
using UnityEngine.UI;

namespace DarcyStudio.GameComponent.ForRenderer
{


    [ExecuteInEditMode]
    public abstract class AnimatorMaterialBase : MonoBehaviour
    {

        private Material _material;

        private bool _isValid = false;

        private void Awake ()
        {
            Init ();
        }

        private void Init ()
        {
            _material = GetMaterial ();
            if (_material == null)
            {
                Debug.LogError ("AnimatorMaterial -->> No Material");
                _isValid = false;
                return;
            }

            InitModify ();

            _isValid = true;
        }


        protected abstract Material GetMaterial ();

        protected abstract void InitModify ();

        private void LateUpdate ()
        {
            if (!_isValid)
                return;
            UpdateMaterial ();
        }

        protected abstract void UpdateMaterial ();

        public void DrawButtons ()
        {
            if (GUILayout.Button ("Init (编辑器下需手动点击一下初始化)"))
            {
                Init ();
            }
        }

        protected Material GetMaterialFromImage ()
        {
            var image = GetComponent<Image> ();
            if (image == null)
            {
                Debug.LogError ("None image component");
                return null;
            }

            // 因为 image.material 这个材质是共用的 所以直接修改这个材质会导致其他使用该材质的图片也受影响
            // 这里可以考虑做个池子缓存一下这个手动生成的 material 看具体业务情况吧
            var newMaterial = Instantiate (image.material);
            image.material = newMaterial;
            return newMaterial;
        }

        protected Material GetMaterialFromRender ()
        {
            var tRenderer = GetComponent<Renderer> ();
            if (tRenderer == null)
            {
                Debug.LogError ("None Renderer component");
                return null;
            }

            // 这里在编辑器非运行模式下会报一个错 只是一个 Unity 的警告： Instantiating material due to calling renderer.material during edit mode. This will leak materials into the scene. You most likely want to use renderer.sharedMaterial instead.
            // 大致意思是在 edit 模式下 会生成一个材质 所以会造成内存泄露在场景里，如果这时候保存场景了，会将这个材质带入这个场景中，
            // unity 推荐使用 SharedMaterial，但是我们这个脚本会改变 Material 的属性
            // 如果使用 SharedMaterial 会改变 Assets 中的 material 这样不小心提交了可能会导致无意识的修改资源中的材质
            // 鉴于 美术那边使用该脚本一般是做动效，所以还是使用 renderer.material
            Debug.LogError ("⚠️注意 如果看到该警告，且你在操作场景，请不要保存场景，不要保存场景，不要保存场景！！！，如果只是操作 Prefab️ 可以忽略该警告");
            return tRenderer.material;
        }

        protected int KeyToID (string key)
        {
            var id = Shader.PropertyToID (key);
            if (_material.HasProperty (id))
                return id;
            Debug.LogError ("AnimatorMaterial -->> Invalid Property: " + key);
            return -1;
        }

        #region Set Value Interface

        protected void SetColor (int keyID, Color value)
        {
            _material.SetColor (keyID, value);
        }

        protected void SetFloat (int keyID, float value, ref float preValue)
        {
            var dif = value - preValue;
            if (dif < 0)
                dif = -dif;

            if (dif > float.Epsilon)
            {
                _material.SetFloat (keyID, value);
                preValue = value;
            }
        }

        protected void SetVector (int keyID, Vector4 value)
        {
            _material.SetVector (keyID, value);
        }

        protected float GetFloat (int keyID)
        {
            return _material.GetFloat (keyID);
        }

        #endregion

    }

    // public class ModifyKeyValueInt : ModifyKeyValue
    // {
    //     public int value;
    // }
    //
    // public class ModifyKeyValueString : ModifyKeyValue
    // {
    //     public string value;
    // }

}