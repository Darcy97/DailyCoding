/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月25日 星期三
 * Time: 下午4:31:24
 ***/

namespace DarcyStudio.GameComponent.TimeLine.ForAction
{
    public interface IDirectionOwner
    {
        void      SetDirection (Direction direction);
        Direction GetDirection ();
    }

    public enum Direction
    {
        FaceToLeft,
        FaceToRight,
    }
}