/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Wednesday, 11 August 2021
 * Time: 21:49:38
 * Description: Description
 ***/

namespace DarcyStudio.Tools
{
    /// <summary>
    /// 可用于保存到Excel文件的抽象类
    /// </summary>
    [System.Serializable]
    public abstract class ExcelData
    {
        /// 唯一标识符
        protected string _guid;

        public abstract string guid { get; }
    }

    /// <summary>
    /// 测试数据类
    /// </summary>
    public class TestData : ExcelData
    {
        [Description ("ID")] public int ID;

        [Description ("Name")] public string Name;

        [Description ("UpdateTime")] public string UpdateTime;

        public TestData (int id, string name, string updateTime)
        {
            ID         = id;
            Name       = name;
            UpdateTime = updateTime;
        }

        public TestData ()
        {
        }

        public override string guid
        {
            get { return string.Format ("{0}_{1}", ID, Name); }
        }
    }

}