using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarManager2
{
    class Graphics
    {
        public static void Draw()
        {
            int h = 58, w = 50;
            Console.SetWindowSize(w, h);
            Console.SetBufferSize(w, h);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Clear();
            for (int i = 0; i < h; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("|");
                Console.SetCursorPosition(49, i);
                Console.Write("|");

            }
            for (int i = 0; i < w; i++)
            {
                Console.SetCursorPosition(i, 42);
                Console.Write("-");
                Console.SetCursorPosition(i, 50);
                Console.Write("-");
                Console.SetCursorPosition(i, 57);
                Console.Write("-");
            }
            Console.SetCursorPosition(5, 55);
            Console.Write("F1-Help");
            Console.SetCursorPosition(13, 55);
            Console.Write("F2-Mk-Fold");
            Console.SetCursorPosition(24, 55);
            Console.Write("F3-Mk-Fil");
            Console.SetCursorPosition(34, 55);
            Console.Write("Del-Remove");
        }
    }
}
