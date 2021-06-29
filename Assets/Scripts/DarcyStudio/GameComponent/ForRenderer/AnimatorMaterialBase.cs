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
            }

            return image.material;
        }

        protected Material GetMaterialFromRender ()
        {
            var tRenderer = GetComponent<Renderer> ();
            if (tRenderer == null)
            {
                Debug.LogError ("None Renderer component");
            }

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