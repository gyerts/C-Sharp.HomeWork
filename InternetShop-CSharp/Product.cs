using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop_CSharp
{
    class Product
    {
        string name;
        string path;
        List<Tag> myOwnTags;

        public Product()
        {
            myOwnTags = new List<Tag>();
        }

        public void SetPath(string path)
        {
            this.path = path;
        }
        public void SetName(string name)
        {
            this.name = name;
        }

        public string GetPath()
        {
            return path;
        }
        public string GetName()
        {
            return name;
        }

        public List<Tag> GetMyOwnTags()
        {
            return myOwnTags;
        }
        public void SetMyOwnTags(List<Tag> tags)
        {
            myOwnTags = tags;
        }
        public List<string> GenHtmlByTag(List<string> s)
        {
            List<string> newStr = new List<string>();
            foreach (string str in s)
            {
                foreach(Tag tag in myOwnTags)
                {
                    if (str.Contains("<" + tag.GetName() + ">"))
                    {
                        newStr.Add(str.Replace("<" + tag.GetName() + ">", tag.GetValue()));
                    }
                }
            }
            return newStr;
        }
    }
}
