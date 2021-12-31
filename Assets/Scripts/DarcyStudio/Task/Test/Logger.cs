/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Friday, 31 December 2021
 * Time: 15:44:45
 ***/

using UnityEngine.UI;

namespace DarcyStudio.Task.Test
{
    public class Logger
    {
        private Text _text;

        public Logger (Text text)
        {
            _text      = text;
            _text.text = string.Empty;
        }

        public void AddLog (string str)
        {
            var content = _text.text + "\n" + str;
            _text.text = content;
        }

        public void Clear ()
        {
            _text.text = string.Empty;
        }
    }
}