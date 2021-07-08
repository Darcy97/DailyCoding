/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年7月8日 星期四
 * Time: 下午6:55:32
 * Description: Description
 ***/

using UnityEngine;
using UnityEditor;

namespace DarcyStudio.CustomEditor.ShortCut
{
    
    public static class ShortCutEditor
    {
        [MenuItem ("ShortCutEditor/First Command _%D")]
        private static void FirstCommand ()
        {
            Debug.Log ("You used the shortcut Shift+Cmd+D");
        }
    }

}