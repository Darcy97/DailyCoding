/***
 * Created by Darcy
 * Date: Tuesday, 22 June 2021
 * Time: 14:20:15
 * Description: Description
 ***/

using UnityEditor;

namespace DarcyStudio.GameComponent.SortingLayerDrawerAttribute.Editor
{
    public readonly struct DrawerValuePair
    {
        public readonly string             StringValue;
        public readonly SerializedProperty Property;

        public DrawerValuePair (string val, SerializedProperty property)
        {
            StringValue = val;
            Property    = property;
        }
    }
}