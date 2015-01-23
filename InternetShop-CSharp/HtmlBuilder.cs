using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace InternetShop_CSharp
{
    class HtmlBuilder
    {
        string path;
        List<Frame> frames;

        public HtmlBuilder(string path, List<Frame> frames)
        {
            this.path = path + "web\\";
            this.frames = frames;
        }
        public List<string> GetFrame(string name)
        {
            List<string> val = new List<string>();
            foreach (Frame f in frames)
            {
                if (f.GetName() == name)
                {
                    val = f.GetValue();
                    break;
                }
            }
            return val;
        }
        public void Open(Category ct)
        {
            //Создаем файл с именем категории ct та что в параметрах
            FileStream temp = new FileStream(path + ct.GetName() + ".html", FileMode.Create);
            //создаем «потоковый писатель» и связываем его с файловым потоком
            StreamWriter writer = new StreamWriter(temp);

            //Отправляем этот открытый файл в генератор кода
            WriteHtml(ct, GetFrame("test"), writer);

            //закрываем поток. Не закрыв поток, в файл ничего не запишется
            writer.Close();
            temp.Close();
        }
        public void WriteHtml(Category ct, List<string> frame, StreamWriter writer)
        {
            foreach (Category category in ct.GetCategories())
            {
                if (!category.IsTopic())
                {
                    Open(category);
                }
            }
            foreach (string str in frame)
            {
                //переменная которая говорит о том что str ключевое слово или нет
                bool found = false;


                //прежде чем приступать проверим 
                //str на принодлежность к двум ключевым словам:
                //-->product<-- и -->cotegory<--
                //которые в свою очередь запустят циклы
                //считывающие категории или продукты в файл
                if (str.Contains("-->" + "product" + "<--"))
                {
                    List<Product> prods = ct.GetProducts();

                    int last = str.LastIndexOf("@") + 1;
                    string typeOfProduct = str.Substring(last);
                    last = typeOfProduct.LastIndexOf('\n');
                    typeOfProduct = typeOfProduct.Remove(last);

                    foreach (Product pr in prods)
                    {
                        List<string> toFile = pr.GenHtmlByTag(GetFrame(typeOfProduct));
                        foreach (string strg in toFile)
                        {
                            writer.Write(strg);
                        }
                    }
                    continue;
                }
                else if (str.Contains("-->" + "category" + "<--"))
                {
                    List<Category> categs = ct.GetCategories();

                    int last = str.LastIndexOf("@") + 1;
                    string typeOfProduct = str.Substring(last);
                    last = typeOfProduct.LastIndexOf('\n');
                    typeOfProduct = typeOfProduct.Remove(last);

                    foreach (Category pr in categs)
                    {
                        List<string> toFile = pr.GenHtmlByTag(GetFrame(typeOfProduct));
                        foreach (string strg in toFile)
                        {
                            writer.Write(strg);
                            Console.Write(strg);
                        }
                    }
                    continue;
                }


                //проверим, что str не является ключевым словом
                foreach (Frame f in frames)
                {
                    if (str.Contains("-->" + f.GetName() + "<--"))
                    {
                        WriteHtml(ct, GetFrame(f.GetName()), writer);
                        found = true;
                        break;
                    }
                }

                //если мы здесь значит str не является 
                //ключевой строкой, записываем ее в файл
                if (!found) writer.Write(str); 
            }
        }
    }
}