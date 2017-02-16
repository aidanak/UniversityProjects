using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;

namespace FarManager2
{
    class ImageProcess
    {
        public void Process(string path)
        {
            Console.Clear();
            ConsoleColor[] col = {ConsoleColor.White,
                                  ConsoleColor.Black,
                                  ConsoleColor.Blue,
                                  ConsoleColor.Cyan,
                                  ConsoleColor.DarkBlue,
                                  ConsoleColor.DarkCyan,
                                  ConsoleColor.DarkGray,
                                  ConsoleColor.DarkGreen,
                                  ConsoleColor.DarkMagenta,
                                  ConsoleColor.DarkRed,
                                  ConsoleColor.DarkYellow,
                                  ConsoleColor.Gray,
                                  ConsoleColor.Green,
                                  ConsoleColor.Magenta,
                                  ConsoleColor.Red,
                                  ConsoleColor.Yellow
                                  };
            int[] r = { 255, 0, 0, 0, 0, 0, 169, 0, 139, 139, 204, 211, 0, 255, 255, 255 };
            int[] g = { 255, 0, 0, 255, 0, 139, 169, 100, 0, 0, 204, 211, 255, 0, 0, 255 };
            int[] b = { 255, 0, 255, 255, 139, 139, 169, 0, 139, 0, 0, 211, 0, 255, 0, 0 };
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(path);
            Color d;
            double mindev = Int32.MaxValue, dev = Int32.MaxValue;
            int minind = 0;
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    d = image.GetPixel(i, j);
                    mindev = Int32.MaxValue;
                    for (int k = 0; k < 16; k++)
                    {
                        dev = Math.Pow(((d.R - r[k]) * 0.3), 2.0) + Math.Pow(((d.G - g[k]) * 0.59), 2.0) + Math.Pow(((d.B - b[k]) * 0.11), 2.0);
                        if (mindev > dev)
                        {
                            mindev = dev;
                            minind = k;
                        }

                    }
                    Console.SetCursorPosition(i, j);
                    Console.BackgroundColor = col[minind];
                    Console.Write(" ");
                }
            }
            Console.ReadKey();
        }
    }
}
