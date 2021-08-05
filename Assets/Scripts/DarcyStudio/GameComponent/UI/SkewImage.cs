/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月5日 星期四
 * Time: 下午3:00:14
 * Description: 在搞 SkewText 时候刚好看到个 SkewImage 也记录一下吧
 * 原理一样，其实 Image 的 Skew 要简单很多
 ***/

using UnityEngine;
using UnityEngine.UI;

namespace DarcyStudio.GameComponent.UI
{

    [DisallowMultipleComponent]
    [RequireComponent (typeof (Image))]
    [AddComponentMenu ("UI/SkewImage (UI)", 99)]
    public class SkewImage : BaseMeshEffect
    {

        [SerializeField] private Vector3 offsetLeftBottom = Vector3.zero;

        [SerializeField] private Vector3 offsetRightBottom = Vector3.zero;

        [SerializeField] private Vector3 offsetLeftTop = Vector3.zero;

        [SerializeField] private Vector3 offsetRightTop = Vector3.zero;

        public Vector3 OffsetLeftBottom
        {
            get => offsetLeftBottom;
            set
            {
                offsetLeftBottom = value;
                graphic.SetAllDirty ();
            }
        }

        public Vector3 OffsetRightBottom
        {
            get => offsetRightBottom;
            set
            {
                offsetRightBottom = value;
                graphic.SetAllDirty ();
            }
        }

        public Vector3 OffsetLeftTop
        {
            get => offsetLeftTop;
            set
            {
                offsetLeftTop = value;
                graphic.SetAllDirty ();
            }
        }

        public Vector3 OffsetRightTop
        {
            get => offsetRightTop;
            set
            {
                offsetRightTop = value;
                graphic.SetAllDirty ();
            }
        }

        private Vector3 GetOffsetVector (int i)
        {
            switch (i)
            {
                case 0:
                    return offsetLeftBottom;
                case 1:
                    return offsetLeftTop;
                case 2:
                    return offsetRightTop;
                default:
                    return offsetRightBottom;
            }
        }

        public override void ModifyMesh (VertexHelper vh)
        {
            if (!IsActive ())
                return;
            var count = vh.currentVertCount;
            for (var i = 0; i < count; i++)
            {
                var vertex = new UIVertex ();
                vh.PopulateUIVertex (ref vertex, i);
                vertex.position += GetOffsetVector (i);
                vh.SetUIVertex (vertex, i);
            }
        }
    }
}