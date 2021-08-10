/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月4日 星期三
 * Time: 下午10:08:12
 * Description:
 * 最近美术又有需求 要在UI上面放一些可以实现 3D 透视效果的文字
 * 如果只是文字就要搞 3D UI 那么太浪费了
 * 所以有了该脚本
 * 扭曲文字定点实现视觉上的透视效果
 * 可以实现透视效果的问题
 * TIPS: 和描边还有阴影一起使用可能有问题
 * 有时间测一下，如果有问题就优化下
 * 可能扭曲的还不是很完美，效果上来看基本满足需求了
 * 还有个问题，就是不同字体，这个顶点列表排序好像不太一样，但是不影响使用
 ***/

using DarcyStudio.Extension;
using UnityEngine;
using UnityEngine.UI;

namespace DarcyStudio.GameComponent.UI
{
    [DisallowMultipleComponent]
    [RequireComponent (typeof (Text))]
    [AddComponentMenu ("UI/Skew Text (UI)", 99)]
    public class SkewText : BaseMeshEffect
    {

        [SerializeField] private float offsetYTop;
        [SerializeField] private float offsetYBottom;
        [SerializeField] private float offsetX;

        private float _startX;
        private float _endX;
        private float _diffX;
        private float _afterDiffX;

        private float _fontRightTopPosY;
        private float _fontRightBottomPosY;

        private float GetOffsetX (float posX)
        {
            return (posX - _startX) / _diffX * _afterDiffX - (posX - _startX);
        }

        private float GetOffsetY (float posX, float posY, bool top)
        {
            var   offsetY = top ? offsetYTop : -offsetYBottom;
            float rate;
            if (top)
                rate = (posY - _fontRightBottomPosY) / (_fontRightTopPosY - _fontRightBottomPosY);
            else
                rate = (posY - _fontRightTopPosY) / (_fontRightBottomPosY - _fontRightTopPosY);

            return offsetY * (posX - _startX) / _diffX * rate;
        }

        private (float x, float y) GetOffsetXY (float posX, float posY, bool top = true)
        {
            var offsetX = GetOffsetX (posX);
            var offsetY = GetOffsetY (posX, posY, top);
            return (offsetX, offsetY);
        }

        private void SetVertexPosition (ref UIVertex vertex, bool isTop = true)
        {
            var (x, y)          = GetOffsetXY (vertex.position.x, vertex.position.y, isTop);
            var (posX, posY, _) = vertex.position;
            vertex.position.x   = posX + x;
            vertex.position.y   = posY + y;
        }

        public override void ModifyMesh (VertexHelper vh)
        {
            if (!CanSkew (vh))
                return;

            var vertexLeftTop     = new UIVertex ();
            var vertexRightTop    = new UIVertex ();
            var vertexRightBottom = new UIVertex ();
            var vertexLeftBottom  = new UIVertex ();

            vh.PopulateUIVertex (ref vertexLeftTop,     0);
            vh.PopulateUIVertex (ref vertexRightBottom, vh.currentVertCount - 2);
            vh.PopulateUIVertex (ref vertexRightTop,    vh.currentVertCount - 3);

            var textLeftTopPos  = vertexLeftTop.position;
            var textRightTopPos = vertexRightTop.position;

            var (rightBottomX, rightBottomY, _) = vertexRightBottom.position;

            _startX = textLeftTopPos.x;
            _endX   = rightBottomX;

            _diffX      = _endX  - _startX;
            _afterDiffX = _diffX + offsetX;

            _fontRightTopPosY    = textRightTopPos.y;
            _fontRightBottomPosY = rightBottomY;

            for (var i = 0; i < vh.currentVertCount / 4; i++)
            {
                vh.PopulateUIVertex (ref vertexLeftTop,     i * 4);
                vh.PopulateUIVertex (ref vertexRightTop,    i * 4 + 1);
                vh.PopulateUIVertex (ref vertexRightBottom, i * 4 + 2);
                vh.PopulateUIVertex (ref vertexLeftBottom,  i * 4 + 3);

                SetVertexPosition (ref vertexLeftTop);
                SetVertexPosition (ref vertexLeftBottom, false);
                SetVertexPosition (ref vertexRightTop);
                SetVertexPosition (ref vertexRightBottom, false);

                vh.SetUIVertex (vertexLeftTop,     i * 4);
                vh.SetUIVertex (vertexRightTop,    i * 4 + 1);
                vh.SetUIVertex (vertexRightBottom, i * 4 + 2);
                vh.SetUIVertex (vertexLeftBottom,  i * 4 + 3);
            }
        }

        private bool CanSkew (VertexHelper vh)
        {
            if (!IsActive ())
                return false;
            return vh.currentVertCount >= 3;
        }
    }
}