/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Tuesday, 29 June 2021
 * Time: 14:17:54
 * Description: Description
 ***/

namespace DarcyStudio.GameComponent.ForRenderer.Editor
{
    [UnityEditor.CustomEditor (typeof (AnimatorMaterialBase), true)]
    public class AnimatorMaterialEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI ()
        {
            base.OnInspectorGUI ();
            var myTarget = (AnimatorMaterialBase) target;

            myTarget.DrawButtons ();
        }
    }
}