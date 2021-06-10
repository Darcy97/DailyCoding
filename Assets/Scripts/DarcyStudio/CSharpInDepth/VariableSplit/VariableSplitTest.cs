/***
 * Created by Darcy
 * Date: Wednesday, 09 June 2021
 * Time: 16:06:37
 * Description: Tuple Split
 ***/

using DarcyStudio.CSharpInDepth.Tuple;
using UnityEngine;

namespace DarcyStudio.CSharpInDepth.VariableSplit
{
    public class VariableSplitTest:MonoBehaviour
    {
        private void Test ()
        {
            var tuple = (10, "text");
            var (a, b)     = tuple; //隐式分解
            (var c, var d) = tuple; //显示分解

            int    e;
            string f;
            (e, f) = tuple;


            //非元组的分解
            var p = new Point (2, 4);
            var (x, y) = p;

            // p.Deconstruct (out var x1, out var x2);
        }

        private void Start ()
        {
            Test ();
        }
    }

}