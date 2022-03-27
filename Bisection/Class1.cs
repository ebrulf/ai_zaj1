using System;
using wspolne;
using static wspolne.Zbior;

namespace Bisection
{
    public class Bin
    {
        public static double BinSearch(double a, double b, Matma fun, Matma g, double eps)
        {
            if (a >= b || g(a) >= 0 || g(b) <= 0)
            {
                throw new Exception("nieodpowiedni przedział");
            }
            double c = 0;
            while (!Kryt1(a, b, eps, fun))
            {
                c = (a + b) / 2.0;
                //Console.WriteLine("c = " + c+" g(c)="+g(c)+" f(c)="+fun(c));

                if (g(c) < 0)//źle nawiasy
                {
                    a = c;
                }
                else if (g(c) > 0)
                {
                    b = c;
                }
                else
                    break;
                //Console.WriteLine(a + " " + b + " " + c);
            }
            return c;
        }
    }
}
