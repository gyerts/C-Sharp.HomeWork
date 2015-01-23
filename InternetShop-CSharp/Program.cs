using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternetShop_CSharp;
using System.IO;

namespace InternetShop_CSharp
{
    public class InternetShop
    {
        static void Main()
        {
            Console.Clear();
            string str = System.Environment.CommandLine;

            int end = str.LastIndexOf('\\') + 1;
            str = str.Remove(end);
            str = str.Remove(0, 1);
            Console.WriteLine(str); //Взятие пути программы в строку string str
            string temp = str + "shop";
            Tree products = FindFilesAndFolders.GetFolders(temp); //Поиск файлов и папок и запись в переменную Tree products
            Category ct = new Category(products); //Построение дерева Категорий и Продуктов в соответствии с найденными папками в переменную Category ct
            temp = str + "frames";
            Tree TreeFrames = FindFilesAndFolders.GetFolders(temp);

            List<Frame> frames = new List<Frame>();

            foreach (File file in TreeFrames.GetFiles())
            {
                FileStream file1 = new FileStream(file.GetPath(), FileMode.Open); //создаем файловый поток
                StreamReader reader = new StreamReader(file1); // создаем «потоковый читатель» и связываем его с файловым потоком 

                Frame newFrame = new Frame(file.GetName());
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    newFrame.AppendStrToValue(line + "\n");
                }
                frames.Add(newFrame);

                reader.Close(); //закрываем поток
                file1.Close();
            } //Поиск фреймов в папке frames и запись их в переменную List<Frame> frames


            HtmlBuilder hb = new HtmlBuilder(str, frames);
            hb.Open(ct);
        }
    }
}