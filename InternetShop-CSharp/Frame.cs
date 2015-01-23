using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop_CSharp
{
    class Frame
    {
        string name;
        List<string> value;

        public Frame(string name)
        {
            this.name = name;
            value = new List<string>();
        }
        public string GetName()
        {
            return name;
        }
        public List<string> GetValue()
        {
            return value;
        }
        /*public string GetValueWithRaplace(string newValue)
        {
            return value.Replace("<параметр>", newValue);
        }*/
        public void AppendStrToValue(string str)
        {
            value.Add(str);
        }
        public void Draw()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(name);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(value);
        }
    }
}