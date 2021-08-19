/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月19日 星期四
 * Time: 下午4:58:19
 ***/

using System;

namespace DarcyStudio.GameComponent.TimeLine.Skill
{
    public interface ISkillPlayer
    {
        void Play (Action<ISkillPlayer> action = null);
    }
}