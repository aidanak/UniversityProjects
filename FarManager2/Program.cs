using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarManager2
{
    class Program
    {
        private static void Draw(string str,bool nl,ConsoleColor forecol)
        {
            Console.ForegroundColor = forecol;
            if (nl==false)
            {
                Console.Write(str);
            }
            else
            {
                Console.WriteLine(str);
            }
        }
        private static void Show(FileSystemInfo[] cur,int index)
        {
            Graphics.Draw();
            Console.CursorTop = 0;
            int ind = 0;
            try
            {
                Info(cur[index]);
                foreach (FileSystemInfo fsi in cur)
                {
                    string name = cur[ind].Name;
                    string display = name;
                    if (name.Length > 20) display = name.Substring(0, 20);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.CursorLeft = 3;
                    if (cur[ind] is DirectoryInfo)
                    {
                        Draw("[+]", false, ConsoleColor.DarkRed);
                        if (ind == index)
                            Draw(display, true, ConsoleColor.Red);
                        
                        else  Draw(display, true, ConsoleColor.Gray); 
                    }
                    else if (cur[ind] is FileInfo)
                    {
                        if (ind == index) Draw(display, true, ConsoleColor.Red);
                        else Draw(display, true, ConsoleColor.Cyan);
                    }
                    ind++;
                    if (ind > 40) break;
                }
            }
            catch { }
        }
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(3, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth-5));
            Console.SetCursorPosition(3, currentLineCursor);
        }
        private static void Change(FileSystemInfo[] cur,int index)
        {
            if (index == 0)
            {
                RedrawThree(cur, index + 1, false);
                RedrawThree(cur, Math.Min(40, cur.Length - 1), false);
            }
            else if(index== Math.Min(40, cur.Length - 1))
            {
                RedrawThree(cur, index - 1, false);
                RedrawThree(cur, 0, false);
            }
            else
            {
                RedrawThree(cur, index-1, false);
                RedrawThree(cur, index+1, false);
            }
            RedrawThree(cur, index, true);
        }
        private static void RedrawThree(FileSystemInfo[] cur,int index,bool bl)
        {
            Console.SetCursorPosition(0, index);
            ClearCurrentConsoleLine();
            ShowOnlyThreeChanged(cur[index], bl);
        }
        private static void ShowOnlyThreeChanged(FileSystemInfo fsi,bool bl)
        {
            string name = fsi.Name;
            string display = name;
            if (name.Length > 20) display = name.Substring(0, 20);
            if (!bl)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                if (fsi is DirectoryInfo)
                {
                    Draw("[+]", false, ConsoleColor.DarkRed);
                    Draw(display, true, ConsoleColor.Gray);
                }
                else
                {
                    Draw(display, true, ConsoleColor.Cyan);
                }
            }
            else
            {
                if(fsi is DirectoryInfo) Draw("[+]", false, ConsoleColor.DarkRed);
                Draw(display, true, ConsoleColor.Red);
            }
            
        }
        private static void Info(FileSystemInfo fsi)
        {
            var created = fsi.CreationTime;
            var modified =fsi.LastWriteTime;
            var name = fsi.Name;
            var path = fsi.FullName;
            int top = Console.CursorTop;
            Console.SetCursorPosition(2, 50);
            ClearCurrentConsoleLine();
            ClearCurrentConsoleLine();
            ClearCurrentConsoleLine();
            ClearCurrentConsoleLine();
            ClearCurrentConsoleLine();
            
            Console.CursorLeft = 2;
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(name.Substring(0,Math.Min(20,name.Length)).ToString());
            Console.CursorLeft = 2;
            Console.WriteLine(path.Substring(0, Math.Min(48, path.Length)).ToString());
            Console.CursorLeft = 2;
            Console.WriteLine(created.ToString());
            Console.CursorLeft = 2;
            Console.WriteLine(modified.ToString());





            Console.CursorTop = top;
          
        }
        static void Main(string[] args)
        {
            
            Stack <FileSystemInfo[]>  parent=new Stack<FileSystemInfo[]> { };
            Stack<int> parentind=new Stack<int> { };
            int index=0;
            string[] drives = Environment.GetLogicalDrives();
            FileSystemInfo[] cur=new FileSystemInfo[drives.Length] ;
            DirectoryInfo dir = new DirectoryInfo(drives[0]);
            for (int i = 0; i < drives.Length; i++)
            {
                dir = new DirectoryInfo(drives[i]);
                cur[i] = dir as DirectoryInfo;
            }
            Show(cur, index);
            ConsoleKeyInfo pressed = Console.ReadKey(true);
            List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };
            ImageProcess ip = new ImageProcess();
            while (pressed.Key != ConsoleKey.Escape)
            {
                switch (pressed.Key)
                {
                    case ConsoleKey.RightArrow:
                        try
                        {
                            if (cur[index] is DirectoryInfo)
                            {
                                parent.Push(cur);
                                parentind.Push(index);
                                dir = new DirectoryInfo(cur[index].FullName);
                                cur = dir.GetFileSystemInfos();
                                index = 0;
                                Show(cur, index);
                            }
                            else
                            {
                                if (ImageExtensions.Contains(Path.GetExtension(cur[index].FullName).ToUpperInvariant()))
                                {
                                    ip.Process(cur[index].FullName);
                                    Show(cur, index);

                                }
                                else
                                {
                                    System.Diagnostics.Process.Start(cur[index].FullName);
                                }   
                            }
                        }
                        catch
                        {

                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        cur = parent.Pop();
                        index = parentind.Pop();
                        Show(cur, index);
                        break;
                    case ConsoleKey.UpArrow:
                        if (index == 0) index = Math.Min(40,cur.Length - 1);
                        else index--;
                        Change(cur, index);
                        Info(cur[index]);
                        break;
                    case ConsoleKey.DownArrow:
                        if (index == cur.Length-1 || index==40) index = 0;
                        else index++;
                        Change(cur, index);
                        Info(cur[index]);
                        break;
                    case ConsoleKey.F2:
                        Console.SetCursorPosition(2, 42);
                        ClearCurrentConsoleLine();
                        Console.WriteLine("Enter name of a folder and press Enter");
 
                        FileSystemInfo   dirnew = dir as DirectoryInfo;
                        Console.SetCursorPosition(2, 43);
                        ClearCurrentConsoleLine();
                        string name = Console.ReadLine();
                        DirectoryInfo di = new DirectoryInfo((dirnew.FullName).ToString() + @"\" + name);
                        if (!di.Exists)
                        {
                            di.Create();
                            DirectoryInfo dirnew1 = new DirectoryInfo((dirnew.FullName).ToString());
                            cur = dirnew1.GetFileSystemInfos();
                            Show(cur, index);
                        }
                        else
                        {
                            Console.SetCursorPosition(2, 42);
                            ClearCurrentConsoleLine();
                            Console.WriteLine("Folder with such name already exists press F2");
                        }
                        break;
                    case ConsoleKey.F3:
                        Console.SetCursorPosition(2, 42);
                        ClearCurrentConsoleLine();
                        Console.WriteLine("Enter name of a file and press Enter");
                        FileSystemInfo dirnew2 = dir as DirectoryInfo;
                        Console.SetCursorPosition(2, 43);
                        ClearCurrentConsoleLine();
                        string name1 = Console.ReadLine();
                        FileInfo fi = new FileInfo((dirnew2.FullName).ToString() + @"\" + name1);
                        if (!fi.Exists)
                        {
                            fi.Create();
                            DirectoryInfo dirnew3 = new DirectoryInfo((dirnew2.FullName).ToString());
                            cur = dirnew3.GetFileSystemInfos();
                            Show(cur, index);
                        }
                        else
                        {
                            Console.SetCursorPosition(2, 42);
                            ClearCurrentConsoleLine();
                            Console.WriteLine("File with such name already exists press F3");
                        }
                        break;
                    case ConsoleKey.Delete:
                        if (cur.Length>0)
                        {
                            if (cur[index] is DirectoryInfo)
                            {
                                Directory.Delete(cur[index].FullName, true);
                            }
                            else
                            {
                                File.Delete(cur[index].FullName);
                            }
                            cur = dir.GetFileSystemInfos();
                            Show(cur, 0);
                            index = 0;
                        }
                        break;

                }
                pressed = Console.ReadKey(true);
            }
        }
    }
}
