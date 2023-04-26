using System;
using System.Collections.Generic;
using System.Text;

namespace dotNetStandardLibraryClass
{
    public class Operator
    {
        public string ToContat(string value)
        {
            var dateTime = DateTime.Now.ToString("HH:mm:ss");
            return dateTime + ": Hello, " + value;
        }
    }
}
