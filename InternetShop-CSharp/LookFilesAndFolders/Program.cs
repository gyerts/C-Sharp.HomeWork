using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop_CSharp
{
    static public class FindFilesAndFolders
    {
        public static Tree GetFolders(string path)
        {
            Tree all;

            all = new Tree();
            all.SetPath(path);
            int idx = path.LastIndexOf("\\") + 1;
            all.SetName(path.Substring(idx));

            System.IO.DirectoryInfo rootDir = new System.IO.DirectoryInfo(all.GetPath());

            WalkDirectoryTree(rootDir, all);

            return all;
        }
        static void WalkDirectoryTree(System.IO.DirectoryInfo root, Tree folder)
        {
            //Считываем все файлы с root
            System.IO.FileInfo[] files = root.GetFiles("*.*");

            foreach (System.IO.FileInfo fileInfo in files)
            {
                //заносим каждый файл в массив с файлами
                File file = new File();
                file.SetPath(fileInfo.FullName);
                int i = fileInfo.FullName.LastIndexOf("\\") + 1;
                string str = fileInfo.FullName.Substring(i);
                int j = str.LastIndexOf(".") + 1;
                file.SetName(str.Remove(j - 1));
                i = fileInfo.FullName.LastIndexOf(".") + 1;
                file.SetExtension(fileInfo.FullName.Substring(i));

                folder.AddFile(file);
            }
            //Считываем все суб-директории с root
            System.IO.DirectoryInfo[] subDirs = root.GetDirectories();

            foreach (System.IO.DirectoryInfo dirInfo in subDirs)
            {
                Tree subFolder = new Tree();
                subFolder.SetPath(dirInfo.FullName);
                int i = dirInfo.FullName.LastIndexOf("\\") + 1;
                subFolder.SetName(dirInfo.FullName.Substring(i));

                WalkDirectoryTree(dirInfo, subFolder);
                subFolder.SetParentFolder(folder);
                folder.AddFolder(subFolder);
            }
        }
    }
}