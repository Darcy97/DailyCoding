/***
 * Created by Darcy
 * Date: Thursday, 10 June 2021
 * Time: 15:41:58
 * Description: Description
 ***/

using DarcyStudio.Extension;
using UnityEngine;

namespace DarcyStudio.StudyNotes.CSharpInDepth.VariableSplit
{
    public class TestUnityVariable
    {

        private void Test ()
        {
            var v2 = new Vector2 ();
            var (x, y) = v2;

            var v3 = new Vector3 ();
            var (x1, y1, z1) = v3;
        }

    }
}