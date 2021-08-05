using UnityEngine;
using UnityEditor;

public class EditorHelper : MonoBehaviour
{

    [MenuItem ("Assets/CreateBMFont")]
    public static void CreateBmFont ()
    {
        ArtistFont.BatchCreateArtistFont ();
    }
}