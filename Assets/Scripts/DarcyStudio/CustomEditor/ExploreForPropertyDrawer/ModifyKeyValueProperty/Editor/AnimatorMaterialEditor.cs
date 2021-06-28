/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Monday, 28 June 2021
 * Time: 21:21:57
 * Description: Description
 ***/


namespace DarcyStudio.CustomEditor.ExploreForPropertyDrawer.ModifyKeyValueProperty.Editor
{

    [UnityEditor.CustomEditor (typeof (AnimatorMaterial), true)]
    public class AnimatorMaterialEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI ()
        {
            base.OnInspectorGUI ();
            var myTarget = (AnimatorMaterial) target;

            myTarget.DrawButtons ();
        }
    }
}