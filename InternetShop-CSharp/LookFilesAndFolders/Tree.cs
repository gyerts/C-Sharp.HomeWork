using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop_CSharp
{
    public class Tree
    {
        List<File> files;
        List<Tree> folders;
        Tree parentFolder;
        string name;
        string path;

        public Tree()
        {
            files = new List<File>();
            folders = new List<Tree>();
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
        public List<Tree> GetFolders()
        {
            return folders;
        }
        public List<File> GetFiles()
        {
            return files;
        }

        internal void SetParentFolder(Tree f)
        {
            parentFolder = f;
        }
        internal void AddFolder(Tree f)
        {
            folders.Add(f);
        }
        internal void AddFile(File f)
        {
            files.Add(f);
        }


        private void Draw(string tab, Tree subTree)
        {
            tab += "  |";
            foreach (File file in subTree.files)
            {
                Console.Write(tab);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("  file ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(file.GetName() + "." + file.GetExtension());
            }

            foreach (Tree tree in subTree.folders)
            {
                Console.Write(tab);
                Console.Write(">>");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("folder ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(tree.GetName() + " ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(tree.parentFolder.path + "\n");
                Draw(tab, tree);
            }
            tab = tab.Substring(3);
        }
        private void Draw(string tab)
        {
            tab += "  |";
            foreach (File file in files)
            {
                Console.Write(tab);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("  file ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(file.GetName() + "." + file.GetExtension());
            }

            foreach (Tree tree in folders)
            {
                Console.Write(tab);
                Console.Write(">>");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("folder ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(tree.GetName() + " ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(tree.parentFolder.path + "\n");
                Draw(tab, tree);
            }
            tab = tab.Substring(3);
        }
        public void Draw()
        {
            Console.Clear();
            int idx = path.LastIndexOf("\\") + 1;
            string tmp = path.Remove(idx);
            Console.Write(">>");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("folder ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(GetName() + " ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(tmp + "\n");
            Draw("");
        }
    }
}