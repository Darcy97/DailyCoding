/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月24日 星期二
 * Time: 下午5:15:30
 ***/

using System.Text;
using UnityEngine;

namespace DarcyStudio.GameComponent.Tools
{
    public static class TransformExtension
    {
        public static string GetPath (this Transform trans)
        {
            if (trans == null)
            {
                return "传入对象为空";
            }

            var tempPath = new StringBuilder (trans.name);
            var tempTra  = trans;
            var g        = "/";
            while (tempTra.parent != null)
            {
                tempTra = tempTra.parent;
                tempPath.Insert (0, tempTra.name + g);
            }

            return tempPath.ToString ();
        }
    }
}