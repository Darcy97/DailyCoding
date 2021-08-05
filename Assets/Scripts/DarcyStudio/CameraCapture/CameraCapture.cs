/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年7月7日 星期三
 * Time: 下午3:20:01
 * Description: Description
 ***/

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DarcyStudio.CaptureScreen
{
    public class CameraCapture : MonoBehaviour
    {
        private const string Folder = "ScreenshotFolder";

        // [SerializeField] private Camera _camera;

        [SerializeField] private List<Camera> _cameras;

        [SerializeField] private int _textureSizeX = 128;
        [SerializeField] private int _textureSizeY = 128;

        private int _frameCount = 0;

        private string _rootFolder;

        public void ResetFrameCount ()
        {
            _frameCount = 0;
        }


        private void Start ()
        {
            _rootFolder                 = Environment.GetFolderPath (Environment.SpecialFolder.DesktopDirectory);
            Application.targetFrameRate = 30;
            Directory.CreateDirectory (Path.Combine (_rootFolder, Folder));
        }

        private string GetFilePath (string model, string animationName, int frameCount, int cameraIndex, int modelIndex,
            int                            animationIndex)
        {
            return
                $"{Folder}/{model}_{modelIndex:D04}/{animationName}_{animationIndex:D04}/camera{cameraIndex:D02}/{frameCount:D04}-shot.png";
        }

        private void SaveTextureToFile (Texture2D texture, string fileName)
        {
            var dict = Path.GetDirectoryName (fileName);
            if (!string.IsNullOrEmpty (dict))
            {
                Directory.CreateDirectory (Path.Combine (_rootFolder, dict));
            }

            var bytes  = texture.EncodeToPNG ();
            var file   = File.Open (Path.Combine (_rootFolder, fileName), FileMode.Create);
            var binary = new BinaryWriter (file);
            binary.Write (bytes);
            file.Close ();
        }

        public void Capture (string modelName, string animationName, int modelIndex, int animationIndex)
        {
            _frameCount++;
            var index = 0;
            foreach (var c in _cameras)
            {
                Capture (index, c, _textureSizeX, _textureSizeY, modelName, animationName, modelIndex, animationIndex);
                index++;
            }
        }

        private void Capture (int cameraIndex, Camera camera, float textureSizeX, float textureSizeY, string modelName,
            string                animationName, int modelIndex, int animationIndex)
        {
            var rect = new Rect (0, 0, textureSizeX, textureSizeY);
            if (camera == null)
            {
                return;
            }

            var rt = RenderTexture.GetTemporary ((int) rect.width, (int) rect.height, 0);
            if (rt == null)
            {
                return;
            }

            camera.targetTexture = rt;
            camera.Render ();
            RenderTexture.active = rt;

            var screenShot = new Texture2D ((int) rect.width, (int) rect.height, TextureFormat.RGB24, false);
            screenShot.ReadPixels (rect, 0, 0); // 注：这个时候，它是从RenderTexture.active中读取像素
            screenShot.Apply ();


            camera.targetTexture = null;
            RenderTexture.active = null;
            SaveTextureToFile (screenShot,
                GetFilePath (modelName, animationName, _frameCount, cameraIndex, modelIndex, animationIndex));
            RenderTexture.ReleaseTemporary (rt);
        }
    }
}