/***
 * Created by Darcy
 * Github: https://github.com/Darcy97
 * Date: Thursday, 23 November 2023
 * Time: 16:32:42
 * Description: Description
 ***/

/***
 * Created by Darcy
 * Github: https://github.com/Darcy97
 * Date: Wednesday, 15 November 2023
 * Time: 20:13:59
 * Description: 见 README
 ***/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;


namespace DarcyStudio.Tools.Editor.VersionControl
{
    public class VersionControl : EditorWindow
    {

        [MenuItem ("Tools/VersionControl/Open")]
        private static void Open ()
        {
            GetWindow<VersionControl> ().Show ();
        }


        private Object Dict;


        public string resourceVersion = "0.0.0";

        private void UpdateByDict ()
        {
            var path = Path.Combine (Application.dataPath, ".AssetMd5.txt");
            ReadDictionaryFromFile (path);

            AddLabelByDict (Dict, "Lua");

            WriteDictionaryToFile (path);

            EditorUtility.ClearProgressBar ();

            AssetDatabase.SaveAssets ();
            AssetDatabase.Refresh ();
        }

        private void UpdateAll ()
        {
            var path = Path.Combine (Application.dataPath, ".AssetMd5.txt");
            ReadDictionaryFromFile (path);

            AddLabelByPath (Application.dataPath);

            WriteDictionaryToFile (path);

            EditorUtility.ClearProgressBar ();

            AssetDatabase.SaveAssets ();
            AssetDatabase.Refresh ();
        }

        private void DeleteAllVersionLabel ()
        {
            var files = Directory.GetFiles (Application.dataPath, "*", SearchOption.AllDirectories);

            var total = files.Length;
            var index = 0;

            foreach (var item in files)
            {
                index++;
                
                var tPath = item.Replace ("\\", "/");
                var obj =
                    AssetDatabase.LoadAssetAtPath<Object> (
                        tPath.Substring (tPath.IndexOf ("Assets/", StringComparison.Ordinal)));

                if (!obj)
                    continue;
                
                DisplayDialog ("Assets", tPath, (float)index / total);
                RemoveVersionLabel (obj);
            }
            
            EditorUtility.ClearProgressBar ();

            AssetDatabase.SaveAssets ();
            AssetDatabase.Refresh ();
        }

        private static void RemoveVersionLabel (Object obj)
        {
            var labels = AssetDatabase.GetLabels (obj);

            var versionLabelIndex = GetVersionLabelIndex (labels);

            if (versionLabelIndex < 0)
                return;

            var result = labels.ToList ();
            result.RemoveAt (versionLabelIndex);
            AssetDatabase.SetLabels (obj, result.ToArray ());

            RemoveVersionLabel (obj);
        }

        private void OnGUI ()
        {
            GUILayout.BeginVertical ();


            Dict            = EditorGUILayout.ObjectField ("资源目录", Dict, typeof (Object), true);
            resourceVersion = EditorGUILayout.TextField ("Version", resourceVersion);

            if (GUI.Button (new Rect (5, 35, 200, 30), "检查资源并设置 Label (选中文件)"))
            {
                UpdateByDict ();
            }

            if (GUI.Button (new Rect (5, 70, 200, 30), "检查资源并设置 Label (全部文件)"))
            {
                UpdateAll ();
            }
            
            if (GUI.Button (new Rect (5, 105, 200, 30), "清楚所有 Version Label (全部文件)"))
            {
                DeleteAllVersionLabel ();
            }

            GUILayout.EndVertical ();
        }

        private static readonly Dictionary<string, (string, string)> Dictionary =
            new Dictionary<string, (string, string)> ();

        private void AddLabelByDict (Object dict, string type)
        {
            if (dict == null)
                return;

            var assetPath  = AssetDatabase.GetAssetPath (dict).Replace ("\\", "/");
            var folderPath = assetPath.Replace ("Assets/", string.Empty);
            var path       = Path.Combine (Application.dataPath, folderPath);

            AddLabelByPath (path);
        }

        private void AddLabelByPath (string path)
        {
            var files = Directory.GetFiles (path, "*", SearchOption.AllDirectories);

            var total = files.Length;
            var index = 0;

            var versionLabel = "version_" + resourceVersion;

            foreach (var item in files)
            {
                var tPath = item.Replace ("\\", "/");
                var obj =
                    AssetDatabase.LoadAssetAtPath<Object> (
                        tPath.Substring (tPath.IndexOf ("Assets/", StringComparison.Ordinal)));
                index++;
                if (obj)
                {
                    DisplayDialog ("Assets", tPath, (float)index / total);
                    UpdateFile (obj, versionLabel, tPath);
                }
            }
        }

        private static void DisplayDialog (string title, string info, float percent)
        {
            EditorUtility.DisplayProgressBar (title, info, percent);
        }

        private void UpdateFile (Object obj, string label, string filePath)
        {
            var (preMd5, preVersion) = GetPreMd5AndVersion (filePath);

            //version 相同，说明是最新，更新过了
            if (preVersion == resourceVersion)
            {
                return;
            }

            var curMD5 = CalculateMD5 (filePath);

            //相同说明文件没变
            if (curMD5 == preMd5)
            {
                return;
            }

            //不同，文件变了，更新 version 及 md5
            AddInfoToFile (filePath, curMD5);
            AddVersionLabel (obj, label);
            AddTypeLabel (obj);

        }
        
        private void AddTypeLabel (Object asset)
        {
            if (!(asset is Texture2D texture2D))
                return;

            var path = AssetDatabase.GetAssetPath (asset);

            var importer = AssetImporter.GetAtPath (path);


            if (!(importer is TextureImporter textureImporter))
                return;

            if (textureImporter.maxTextureSize >= 2048 && texture2D.width >= 2048)
            {
                AddLabel (asset, "texture_2048");
            }
            else if (textureImporter.maxTextureSize >= 1024 && texture2D.width >= 1024)
            {
                AddLabel (asset, "texture_1024");
            }
            else if (textureImporter.maxTextureSize >= 512 && texture2D.width >= 512)
            {
                AddLabel (asset, "texture_512");
            }
            else
            {
                AddLabel (asset, "texture_<512");
            }
        }

        private void AddLabel (Object obj, string label)
        {
            var labels = AssetDatabase.GetLabels (obj);


            if (labels.Contains (label))
            {
                return;
            }

            var tLabels = labels.ToList ();
            tLabels.Add (label);
            AssetDatabase.SetLabels (obj, tLabels.ToArray ());
        }

        private void AddVersionLabel (Object obj, string label)
        {
            var labels = AssetDatabase.GetLabels (obj);


            if (labels.Contains (label))
            {
                return;
            }

            var versionLabelIndex = GetVersionLabelIndex (labels);
            AddLabel (obj, labels, label, versionLabelIndex);
        }

        private static void AddLabel (
            Object        obj,
            IList<string> labels,
            string        versionLabel,
            int           oldVersionLabelIndex)
        {
            var tLabels = new List<string> ();

            if (oldVersionLabelIndex >= 0)
            {
                labels[oldVersionLabelIndex] = versionLabel;
            }
            else
            {
                tLabels.Add (versionLabel);
            }

            tLabels.AddRange (labels);
            AssetDatabase.SetLabels (obj, tLabels.ToArray ());
        }

        private static (string, string) GetPreMd5AndVersion (string assetPath)
        {
            return Dictionary.ContainsKey (assetPath) ? Dictionary[assetPath] : default;
        }

        private void AddInfoToFile (string assetPath, string md5)
        {
            Dictionary[assetPath] = (md5, resourceVersion);
        }

        private static int GetVersionLabelIndex (IEnumerable<string> labels)
        {
            var index = 0;
            foreach (var item in labels)
            {
                if (item.Contains ("version") || item.Equals (string.Empty))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        private static string CalculateMD5 (string filename)
        {
            using (var md5 = MD5.Create ())
            {
                using (var stream = File.OpenRead (filename))
                {
                    var hash = md5.ComputeHash (stream);
                    return BitConverter.ToString (hash).Replace ("-", "").ToLowerInvariant ();
                }
            }
        }

        private void WriteDictionaryToFile (string filePath)
        {
            using (var file = new StreamWriter (filePath))
            {
                file.Write ($"Version_{resourceVersion} Update");
                file.WriteLine ();

                var strBuilder = new StringBuilder ();
                foreach (var entry in Dictionary)
                {
                    strBuilder.Clear ();
                    strBuilder.Append (entry.Key).Append ("\t").Append (entry.Value.Item1).Append ("\t")
                        .Append (entry.Value.Item2);
                    file.WriteLine (strBuilder.ToString ());
                }
            }

            Dictionary.Clear ();
        }

        private static void ReadDictionaryFromFile (string filePath)
        {
            if (!File.Exists (filePath))
                File.Create (filePath).Close ();

            Dictionary.Clear ();
            using (var file = new StreamReader (filePath))
            {
                string line;
                while ((line = file.ReadLine ()) != null)
                {
                    string[] parts = line.Split ('\t');
                    if (parts.Length == 3)
                    {
                        Dictionary.Add (parts[0], (parts[1], parts[2]));
                    }
                }
            }
        }
    }
}