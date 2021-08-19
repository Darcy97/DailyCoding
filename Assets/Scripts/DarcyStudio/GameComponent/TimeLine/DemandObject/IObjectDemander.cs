/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月18日 星期三
 * Time: 下午3:06:06
 * Description: 物体需求者
 ***/

namespace DarcyStudio.GameComponent.TimeLine.RequireObject
{
    public interface IObjectDemander
    {
        void SetProvider (IObjectProvider provider);
    }
    
    public enum ObjectType
    {
        Specify,
        Self,
        Enemy1,
        Enemy2,
        Enemy3,
        Enemy4,
        Enemy5,
        Teammate1,
        Teammate2,
        Teammate3,
        Teammate4
    }
}