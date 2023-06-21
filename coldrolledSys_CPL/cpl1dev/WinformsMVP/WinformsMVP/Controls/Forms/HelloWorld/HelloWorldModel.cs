using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinformsMVP.Controls.Forms.HelloWorld
{
    public class HelloWorldModel : HelloWorldContract.IModel
    {
        public string ShowText { get; private set; }

        public HelloWorldModel(string showText)
        {
            ShowText = showText;
        }
    }
}
