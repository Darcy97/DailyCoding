/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月26日 星期四
 * Time: 下午2:51:58
 ***/

using UnityEngine;

namespace DarcyStudio.CustomEditor.Attribute.CustomPropertyAttribute
{
    public class ColorAttribute : PropertyAttribute
    {
        public string Color;

        public ColorAttribute (string color)
        {
            Color = color;
        }
    }
}