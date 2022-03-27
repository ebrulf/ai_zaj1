using System;
using wspolne;
using static wspolne.Zbior;

namespace DirectSearch
{
    public class Dirsearch
    {
        public static double[] Pattern(double x, Matma fun)
        {
            double dx = alfa_min;
            double x1 = x + dx;
            double x2 = x1;
            if (fun(x1) <= fun(x))
            {
                dx *= 2;
                x2 += dx;
                while (fun(x2) < fun(x1))
                {
                    x = x1;
                    x1 = x2;
                    dx *= 2;
                    x2 = x1 + dx;
                }
                return new double[] { x, x2 };

            }
            else
            {
                dx *= 2;
                x2 -= dx;
                while (fun(x) > fun(x2))
                {
                    x1 = x;
                    x = x2;
                    dx *= 2;
                    x2 -= dx;
                }
                return new double[] { x2, x1 };
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
