/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年7月7日 星期三
 * Time: 下午3:20:01
 * Description: Description
 ***/

using System;
using System.IO;
using UnityEngine;

namespace DarcyStudio.CaptureScreen
{
    public class CameraCapture : MonoBehaviour
    {
        private const string Folder    = "ScreenshotFolder";
        private       int    frameRate = 25;

        [SerializeField] private Camera _camera;

        private void Start ()
        {
            Directory.CreateDirectory (Application.dataPath + Path.DirectorySeparatorChar + Folder);
            //
            // Time.captureFramerate = frameRate;
        }

        // private void Update ()
        // {
        //     // 将文件名附加到文件夹名称(格式为'0005 shot.png ')
        //     var name = $"{folder}/{Time.frameCount:D04} shot.png";
        //     //将屏幕截图捕获到指定文件夹
        //     ScreenCapture.CaptureScreenshot (name);
        // }

        private string GetFilePath (string model, string animationName, int frameCount)
        {
            return $"{Folder}/{model}/{animationName}_{frameCount:D04}_shot.png";
        }

        private void SaveTextureToFile (Texture2D texture, string fileName)
        {
            var dict = Path.GetDirectoryName (fileName);
            if (!string.IsNullOrEmpty (dict))
            {
                Directory.CreateDirectory (dict);
            }

            var bytes  = texture.EncodeToPNG ();
            var file   = File.Open (Application.dataPath + Path.DirectorySeparatorChar + fileName, FileMode.Create);
            var binary = new BinaryWriter (file);
            binary.Write (bytes);
            file.Close ();
        }

        private void Update ()
        {
            if (_start)
                Capture ();
        }

        private bool _start = true;

        public void SCapture ()
        {
            _start = true;
        }

        public void Capture ()
        {
            Capture (_camera, 128, 128);
        }

        public void Capture (Camera camera, float textureSizeX, float textureSizeY)
        {
            var rect = new Rect (0, 0, textureSizeX, textureSizeY);
            if (camera == null)
            {
                return;
            }

            // 创建一个RenderTexture对象
            var rt = RenderTexture.GetTemporary ((int) rect.width, (int) rect.height, 0);
            if (rt == null)
            {
                return;
            }

            // 临时设置相关相机的targetTexture为rt, 并手动渲染相关相机
            camera.targetTexture = rt;
            camera.Render ();
            //ps: --- 如果这样加上第二个相机，可以实现只截图某几个指定的相机一起看到的图像。
            //ps: camera2.targetTexture = rt;
            //ps: camera2.Render();
            //ps: -------------------------------------------------------------------

            // 激活这个rt, 并从中中读取像素。
            RenderTexture.active = rt;

            var screenShot = new Texture2D ((int) rect.width, (int) rect.height, TextureFormat.RGB24, false);
            screenShot.ReadPixels (rect, 0, 0); // 注：这个时候，它是从RenderTexture.active中读取像素
            screenShot.Apply ();


            // 重置相关参数，以使用camera继续在屏幕上显示
            camera.targetTexture = null;
            //ps: camera2.targetTexture = null;
            RenderTexture.active = null; // JC: added to avoid errors
            SaveTextureToFile (screenShot, GetFilePath ("Model1", "Animation1", Time.frameCount));
            // ScreenCapture.CaptureScreenshot ();
        }
    }
}