/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月23日 星期一
 * Time: 下午3:14:51
 ***/

using DarcyStudio.CustomEditor.Attribute.Editor;
using DarcyStudio.GameComponent.TimeLine.ForAction.Receiver;
using UnityEditor;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.Editor
{
    [CustomPropertyDrawer (typeof (PerformData))]
    public class PerformDataDrawer : SuperPropertyDrawer
    {
        protected override void SuperOnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            var performType = DrawProperty (nameof (PerformData.performType), string.Empty, ref position, 0, 90,
                property);

            DrawPara (ref position, property, (PerformType) performType.enumValueIndex);

            //绘制第二行
            NewLine (ref position);

            DrawProperty (nameof (PerformData.waitDone), "WaitDone(是否等待结束)", ref position, 130, 20, property,
                true);
        }

        protected override int GetLineCount (SerializedProperty property)
        {
            var p = property.FindPropertyRelative (nameof (PerformData.performType));
            switch ((PerformType) p.enumValueIndex)
            {
                case PerformType.Default:
                    return 2;
                case PerformType.Animation:
                    return 2;
                case PerformType.ShowGo:
                    return 2;
                case PerformType.Move:
                    return 4;
                default:
                    return 2;
            }
        }

        private void DrawPara (ref Rect position, SerializedProperty property, PerformType actionType)
        {
            switch (actionType)
            {
                case PerformType.Default:
                    break;
                case PerformType.Animation:
                    DrawProperty (nameof (PerformData.animationKey), "Key", ref position, 25, 100, property);
                    break;
                case PerformType.ShowGo:
                    DrawProperty (nameof (PerformData.go),       string.Empty, ref position, 0,  130, property);
                    DrawProperty (nameof (PerformData.duration), "Duration",   ref position, 50, 30,  property);
                    break;
                case PerformType.Move:

                    NewLine (ref position);
                    DrawProperty (nameof (PerformData.moveVelocity), "速度系数", ref position, 45, 200, property);

                    NewLine (ref position);
                    DrawProperty (nameof (PerformData.moveAcceleration), "加速度", ref position, 45, 200, property);
                    break;

                case PerformType.ResetPosition:
                    DrawProperty (nameof (PerformData.duration), "Duration", ref position, 50, 30, property);
                    break;
                default:
                    break;
            }
        }
    }
}