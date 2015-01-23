using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace InternetShop_CSharp
{
    class Category
    {
        List<Product> products;
        List<Category> categories;

        Category parentCategory;
        string name;
        string path;

        List<Tag> myOwnTags;

        public Category(Tree tree)
        {
            myOwnTags = new List<Tag>();
            products = new List<Product>();
            categories = new List<Category>();
            parentCategory = new Category();
            SetName(tree.GetName());
            SetPath(tree.GetPath());
            Initialize(tree, this);
        }
        public Category()
        {
            products = new List<Product>();
            categories = new List<Category>();
            myOwnTags = new List<Tag>();
            //parentCategory не проинициализированна в этом конструкторе
        }

        public void Initialize(Tree tree, Category parentCt)
        {
            //В этой же ветке ишем все вложенные папки (ветка из параметраметров)
            foreach (Tree insideFolder in tree.GetFolders())
            {
                Category ct = new Category();
                ct.SetName(insideFolder.GetName());
                ct.SetPath(insideFolder.GetPath());
                ct.SetParentCategory(parentCt);

                Initialize(insideFolder, ct);
                parentCt.AddCategory(ct);

                //В ветке tree ищем все вложенные файлы (ветка из параметраметров)
                //Ищем описания для категории ct
                foreach (File insideFile in tree.GetFiles())
                {
                    //если имя папки совпадает с именем файла
                    if (insideFile.GetName() == insideFolder.GetName())
                    {
                        //если найденый файл с расширением inf
                        if (insideFile.GetExtension() == "inf")
                        {
                            //начинаем считывать содержимое файла
                            //и записывать в parentCt
                            using (StreamReader sr = new StreamReader(insideFile.GetPath()))
                            {
                                string line;
                                //Console.WriteLine();

                                string str1 = "", str2 = "";

                                while ((line = sr.ReadLine()) != null)
                                {
                                    if (line.Contains("<тег>"))
                                    {
                                        str1 = line.Substring(5);
                                    }
                                    else
                                    {
                                        str2 += line;
                                    }

                                    if (str2.Length > 0)
                                    {
                                        ct.myOwnTags.Add(new Tag(str1, str2));
                                        str2 = "";
                                    }
                                    //Console.WriteLine(line);
                                }
                                sr.Close();
                            }
                        }
                    }
                }
            }



            //-------------------------------------------------------------------//
            //Шерстим внутри папок на предмет продуктов, иными словами ищем продукты
            foreach (File file in tree.GetFiles())
            {
                bool product = true;

                //Сравниваем имена файлов и папок на принадлежность
                //Если имена не принадлежат друг другу
                //значит этот файл относится к продукту
                foreach (Tree folder in tree.GetFolders())
                {
                    if (file.GetName() == folder.GetName())
                    {
                        product = false;
                        break;
                    }
                }
                if (product)
                {
                    Product pr = new Product();
                    bool isPresent = false;
                    //Ищем среди продутов, продукт с таким же именем
                    foreach (Product prod in parentCt.GetProducts())
                    {
                        //Если такой продукт уже есть
                        if (prod.GetName() == file.GetName())
                        {
                            //Присваеваем указатель обрабатываемой позиции продукта и продолжаем над ней работать
                            pr = prod;
                            isPresent = true;
                            break;
                        }
                    }

                    if (!isPresent)
                    {
                        pr = new Product();
                        pr.SetName(file.GetName());
                        string path = file.GetPath();
                        int last = path.LastIndexOf("\\") + 1;
                        pr.SetPath(path.Remove(last));
                    }

                    if (file.GetExtension() == "inf")
                    {
                        using (StreamReader sr = new StreamReader(file.GetPath()))
                        {
                            string line;
                            //Console.WriteLine();
                            List<Tag> myOwnTags = new List<Tag>();
                            string str1 = "", str2 = "";

                            while ((line = sr.ReadLine()) != null)
                            {
                                if (line.Contains("<тег>"))
                                {
                                    str1 = line.Substring(5);
                                }
                                else
                                {
                                    str2 += line;
                                }

                                if (str2.Length > 0)
                                {
                                    myOwnTags.Add(new Tag(str1, str2));
                                    str2 = "";
                                }
                                //Console.WriteLine(line);
                            }
                            sr.Close();
                            pr.SetMyOwnTags(myOwnTags);
                        }
                    }
                    if(!isPresent) parentCt.AddProduct(pr);
                }
            }
        }

        internal void SetPath(string path)
        {
            this.path = path;
        }
        internal void SetName(string name)
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
        public List<Category> GetCategories()
        {
            return categories;
        }
        public List<Product> GetProducts()
        {
            return products;
        }

        internal void SetParentCategory(Category f)
        {
            parentCategory = f;
        }
        internal void AddCategory(Category f)
        {
            categories.Add(f);
        }
        internal void AddProduct(Product f)
        {
            products.Add(f);
        }
        public bool IsTopic()
        {
            bool f = false;
            if (categories.Count == 0)
            {
                f = true;
            }
            return f;
        }
        public List<string> GenHtmlByTag(List<string> s)
        {
            List<string> newStr = new List<string>();
            foreach (string str in s)
            {
                foreach (Tag tag in myOwnTags)
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