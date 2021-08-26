/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月26日 星期四
 * Time: 下午2:16:44
 ***/

namespace DarcyStudio.CustomEditor.Attribute.CustomPropertyAttribute
{

    using UnityEngine;
    using System;

    [AttributeUsage (AttributeTargets.Enum | AttributeTargets.Field)]
    public class EnumLabelAttribute : PropertyAttribute
    {
        public string label;
        public int[]  order = new int[0];

        public EnumLabelAttribute (string label)
        {
            this.label = label;
        }

        public EnumLabelAttribute (string label, params int[] order)
        {
            this.label = label;
            this.order = order;
        }
    }


}