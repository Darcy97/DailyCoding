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
 ***/

using UnityEngine;
using UnityEngine.UI;

namespace DarcyStudio.GameComponent.UI
{
    public class PerspectiveText : BaseMeshEffect
    {

        [SerializeField] private float offsetYTop;
        [SerializeField] private float offsetYBottom;

        [SerializeField] private float offsetX;

        private float _startX;
        private float _endX;
        private float _diffX;
        private float _afterDiffX;

        private float GetOffsetX (float posX)
        {
            return (posX - _startX) / _diffX * _afterDiffX - (posX - _startX);
        }

        private float GetOffsetY (float posX, bool top)
        {
            var offsetY = top ? offsetYTop : offsetYBottom;
            return offsetY * (posX - _startX) / _diffX;
        }

        private (float x, float y) GetOffsetXY (float posX, bool top = true)
        {
            var offsetX = GetOffsetX (posX);
            var offsetY = GetOffsetY (posX, top);
            return (offsetX, offsetY);
        }

        private void SetVertexPosition (ref UIVertex vertex, bool isTop = true)
        {
            var (x, y) = GetOffsetXY (vertex.position.x, isTop);
            var sourcePos = vertex.position;
            vertex.position.x = sourcePos.x + x;
            vertex.position.y = sourcePos.y + y;
        }

        public override void ModifyMesh (VertexHelper vh)
        {
            UIVertex vertexLeftTop     = new UIVertex ();
            UIVertex vertexRightTop    = new UIVertex ();
            UIVertex vertexRightBottom = new UIVertex ();
            UIVertex vertexLeftBottom  = new UIVertex ();

            vh.PopulateUIVertex (ref vertexLeftTop,     0);
            vh.PopulateUIVertex (ref vertexRightBottom, vh.currentVertCount - 1);

            var textLeftTopPos     = vertexLeftTop.position;
            var textRightBottomPos = vertexRightBottom.position;


            _startX = textLeftTopPos.x;
            _endX   = textRightBottomPos.x;

            _diffX      = _endX  - _startX;
            _afterDiffX = _diffX + offsetX;

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
    }
}