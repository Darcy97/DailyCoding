using UnityEngine;
using UnityEditor;

public class EditorHelper : MonoBehaviour
{

    [MenuItem ("Assets/BatchCreateBMFont")]
    public static void BatchCreateBMFont ()
    {
        ArtistFont.BatchCreateArtistFont ();
    }
}