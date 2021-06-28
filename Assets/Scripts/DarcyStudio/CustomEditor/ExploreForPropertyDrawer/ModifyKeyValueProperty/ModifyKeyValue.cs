/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Monday, 28 June 2021
 * Time: 21:20:43
 * Description: Description
 ***/

using System;
using UnityEngine;

namespace DarcyStudio.CustomEditor.ExploreForPropertyDrawer.ModifyKeyValueProperty
{
    [Serializable]
    public class ModifyKeyValue //:PropertyAttribute
    {
        public string          Key;
        public int             KeyID { get; private set; }
        public ModifyValueType ModifyValueType = ModifyValueType.Default;


        public int   IntValue;
        public float FloatValue;
        public Color ColorValue;

        public ModifyValueType GetModifyValueType ()
        {
            return ModifyValueType;
        }

        private bool _isValid = false;

        public void Init ()
        {
            KeyID    = Shader.PropertyToID (Key);
            _isValid = true;
        }

        public bool IsValid => _isValid;

        public void SetInvalid ()
        {
            _isValid = false;
        }
    }

    [Serializable]
    public enum ModifyValueType
    {
        Default,
        TypeInt,
        TypeFloat,
        TypeColor,
    }

}