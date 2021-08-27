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
    [CustomPropertyDrawer (typeof (PerformConfig))]
    public class PerformConfigDrawer : SuperPropertyDrawer
    {
        protected override void SuperOnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            var performType = DrawProperty (nameof (PerformConfig.performType), string.Empty, ref position, 0, 90,
                property);

            DrawPara (ref position, property, (PerformType) performType.enumValueIndex);

            //绘制第二行
            NewLine (ref position);

            DrawProperty (nameof (PerformConfig.waitDone), "WaitDone(是否等待结束)", ref position, 130, 20, property,
                true);
        }

        protected override int GetLineCount (SerializedProperty property)
        {
            var p = property.FindPropertyRelative (nameof (PerformConfig.performType));
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
                    DrawProperty (nameof (PerformConfig.animationKey), "Key", ref position, 25, 100, property);
                    break;
                case PerformType.ShowGo:
                    DrawProperty (nameof (PerformConfig.go),       string.Empty, ref position, 0,  130, property);
                    DrawProperty (nameof (PerformConfig.duration), "Duration",   ref position, 50, 30,  property);
                    break;
                case PerformType.Move:

                    NewLine (ref position);
                    DrawProperty (nameof (PerformConfig.moveVelocityRate), "速度系数", ref position, 50, 200, property);

                    NewLine (ref position);
                    DrawProperty (nameof (PerformConfig.moveAcceleration), "加速度", ref position, 50, 200, property);
                    break;

                case PerformType.ResetPosition:
                    DrawProperty (nameof (PerformConfig.duration), "Duration", ref position, 50, 30, property);
                    break;
                case PerformType.Wait:
                    DrawProperty (nameof (PerformConfig.k0), "停滞时间系数", ref position, 70, 100, property);
                    break;
                default:
                    break;
            }
        }
    }
}