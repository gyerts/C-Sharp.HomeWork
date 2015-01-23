using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop_CSharp
{
    public class File
    {
        string extension;
        string name;
        string path;

        public void SetPath(string path)
        {
            this.path = path;
        }
        public void SetName(string name)
        {
            this.name = name;
        }
        public void SetExtension(string extension)
        {
            this.extension = extension;
        }

        public string GetPath()
        {
            return path;
        }
        public string GetName()
        {
            return name;
        }
        public string GetExtension()
        {
            return extension;
        }
    }
}