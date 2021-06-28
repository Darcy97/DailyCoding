/***
 * Created by Darcy
 * Github: https://github.com/Darcy97
 * Date: Monday, 28 June 2021
 * Time: 21:02:20
 * Description:
 * 该脚本的由来
 * 当时美术有一个需求想要通过动画 Animation 动态控制材质球的属性
 * 但是 Unity 不支持（2018.4.27)
 * 所以想到通过脚本来控制，然后动画来控制脚本的参数
 * 当时为了想要脚本通用性更高，可以通过简单配置 Inspector 来适配所有材质
 * 所以开发了该脚本
 * 但是无奈，Unity animation 只能控制脚本的变量，像这种 List 里面的变量对象的值 Animation 记录不了
 * 遂，该脚本呢最后没有产生生产价值，但是这过程中研究了一些 PropertyDrawer 所以也不算做无用功 😄
 * 最终投入使用的脚本在另一个目录下: AnimatorMaterialBase 通用性不是很高，但是符合实际需求，因为美术同学也只是需要几个材质做这个处理（实际好像只有一个材质用了。。。)
 ***/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DarcyStudio.CustomEditor.ExploreForPropertyDrawer.ModifyKeyValueProperty
{

    [ExecuteInEditMode]
    public class AnimatorMaterial : MonoBehaviour
    {

        [SerializeField] private Material _material;

        [SerializeField] private List<ModifyKeyValue> _modifyValues;

        private bool _isValid;

        private void Awake ()
        {
            Init ();
        }

        private void Init ()
        {
            var render = GetComponent<Image> ();
            if (render == null)
            {
                Debug.LogError ("AnimatorMaterial -->> No Renderer Component");
                return;
            }

            _material = render.material;

            InitModify ();

            _isValid = true;
        }

        private void InitModify ()
        {
            foreach (var item in _modifyValues)
            {
                if (item == null)
                    continue;

                item.Init ();

                var hasProperty = _material.HasProperty (item.KeyID);
                if (hasProperty)
                    continue;
                Debug.LogError ("AnimatorMaterial -->> Invalid Property: " + item.Key);
                item.SetInvalid ();
            }
        }

        private void LateUpdate ()
        {
            if (!_isValid)
                return;
            UpdateMaterial ();
        }

        private void UpdateMaterial ()
        {
            foreach (var item in _modifyValues)
            {
                if (item == null)
                    continue;
                if (!item.IsValid)
                    continue;

                switch (item.ModifyValueType)
                {
                    case ModifyValueType.TypeInt:
                        _material.SetInt (item.KeyID, item.IntValue);
                        break;
                    case ModifyValueType.TypeFloat:
                        _material.SetFloat (item.KeyID, item.FloatValue);
                        break;
                    case ModifyValueType.TypeColor:
                        _material.SetColor (item.KeyID, item.ColorValue);
                        break;
                    case ModifyValueType.Default:
                        Debug.LogError ("AnimatorMaterial -->> Un Handle type: " + item.ModifyValueType);
                        break;
                    default:
                        Debug.LogError ("AnimatorMaterial -->> Un Handle type" + item.ModifyValueType);
                        break;
                }
            }
        }

        public void DrawButtons ()
        {

            if (GUILayout.Button ("ReInit"))
            {
                Init ();
            }
        }
    }
    
    
   
}