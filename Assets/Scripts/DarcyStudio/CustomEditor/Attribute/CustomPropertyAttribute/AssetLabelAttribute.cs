/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Tuesday, 30 November 2021
 * Time: 16:22:29
 ***/

using UnityEngine;

namespace DarcyStudio.CustomEditor.Attribute.CustomPropertyAttribute
{
    public class AssetLabelAttribute:PropertyAttribute
    {
        public int MinScore;
        public int MaxCount;
        public AssetLabelAttribute (int minScore = 0, int maxCount = 15)
        {
            MinScore    = minScore;
            MaxCount = maxCount;
        }
    }
}