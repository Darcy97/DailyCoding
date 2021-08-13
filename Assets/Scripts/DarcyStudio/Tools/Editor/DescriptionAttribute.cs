/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Wednesday, 11 August 2021
 * Time: 21:50:49
 * Description: Description
 ***/

using System;

namespace DarcyStudio.Tools
{
    public class DescriptionAttribute : Attribute
    {
        private string description;

        public DescriptionAttribute (string str_description)
        {
            description = str_description;
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}