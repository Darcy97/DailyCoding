/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月26日 星期四
 * Time: 下午3:14:06
 ***/

using System;
using UnityEngine;

namespace DarcyStudio.CustomEditor.Attribute.CustomPropertyAttribute
{
    [AttributeUsage (AttributeTargets.Enum | AttributeTargets.Field)]
    public class ColorEnumAttribute : PropertyAttribute
    {

    }
}