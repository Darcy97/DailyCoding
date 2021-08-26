/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月26日 星期四
 * Time: 下午4:51:50
 ***/

using DarcyStudio.CustomEditor.Attribute.Editor;
using DarcyStudio.GameComponent.TimeLine.ForAction.Receiver;
using UnityEditor;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.Editor
{
    [CustomPropertyDrawer (typeof (ActionBaseConfig))]
    public class ActionBaseConfigDrawer : SuperPropertyDrawer
    {
        protected override void SuperOnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            DrawProperty (nameof (ActionBaseConfig.WaitDone), "是否等待结束", ref position, 80, 20, property);
            NewLine (ref position);
            var p = DrawProperty (nameof (ActionBaseConfig.SpecifyNextAction), "指定下一个动作", ref position, 80, 20,
                property);
            if (p.boolValue)
                DrawProperty (nameof (ActionBaseConfig.NextAction), string.Empty, ref position, 0, 100, property);
        }

        protected override int GetLineCount (SerializedProperty property)
        {
            return 2;
        }
    }
}