/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月18日 星期三
 * Time: 下午3:07:35
 * Description: 物体提供者
 ***/

namespace DarcyStudio.GameComponent.TimeLine.RequireObject
{
    public interface IObjectProvider
    {
        IObject Get (ObjectType objectType);
    }
}