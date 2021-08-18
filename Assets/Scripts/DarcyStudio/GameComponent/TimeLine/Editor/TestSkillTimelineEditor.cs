/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月18日 星期三
 * Time: 下午6:32:02
 * Description: Description
 ***/

namespace DarcyStudio.GameComponent.TimeLine.Editor
{
    [UnityEditor.CustomEditor (typeof (TestSkillTimeline))]
    public class TestSkillTimelineEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI ()
        {
            base.OnInspectorGUI ();
            var myTarget = (TestSkillTimeline) target;
            myTarget.DrawButtons ();
        }
    }
}