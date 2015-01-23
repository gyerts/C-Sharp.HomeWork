using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop_CSharp
{
    class Tag
    {
        string name;
        string value;

        public Tag(string name, string val)
        {
            this.name = name;
            this.value = val;
        }
        public string GetName()
        {
            return name;
        }
        public string GetValue()
        {
            return value;
        }
        public string GetValueWithRaplace(string newValue)
        {
            return value.Replace("<параметр>", newValue);
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