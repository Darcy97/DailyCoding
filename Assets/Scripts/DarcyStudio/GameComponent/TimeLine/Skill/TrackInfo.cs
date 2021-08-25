/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月17日 星期二
 * Time: 上午11:01:34
 * Description:
 ***/

using System;

namespace DarcyStudio.GameComponent.TimeLine.Skill
{
    [Serializable]
    public class TrackInfo
    {
        public string    Name;
        public TrackType Type;

        public TrackInfo (string name, TrackType type)
        {
            Name = name;
            Type = type;
        }
    }

    [Serializable]
    public enum TrackType
    {
        Default,
        Self,
        Enemy1,
        Enemy2,
        Enemy3,
        Enemy4,
        Enemy5
    }
}