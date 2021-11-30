/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Tuesday, 29 June 2021
 * Time: 21:54:45
 * Description: Description
 ***/

using System;
using System.Collections.Generic;
using DarcyStudio.CustomEditor.Attribute.CustomPropertyAttribute;
using UnityEngine;

namespace Test
{
    public class TestSomeThing : MonoBehaviour
    {
        [AssetLabel] public string labels;

        private void Start ()
        {
            // const int low  = 0;
            // const int high = 2;
            // var       mid  = low + ((high - low) >> 1);

            var list = new List<int> ();

            list.Add (0);
            list.Add (1);
            list.Add (2);
            list.Add (3);

            list.RemoveAt (0);

            Debug.Log (list[1]);
        }
    }
}