using System;
using wspolne;
using static wspolne.Zbior;

namespace GoldSer
{
    public class Gold
    {
        public static double GoldSearch(double a, double b, Matma fun, double eps)//now just like on the slides
        {
            double invphi = (Math.Sqrt(5) - 1) / 2.0;  // 0.618...
            double invphi2 = (3 - Math.Sqrt(5)) / 2.0;  // 0.382
            double c = a + (b - a) * invphi2;//x_1
            double d = a + (b - a) * invphi;//x_2
            double f_1 = fun(c);
            double f_2 = fun(d);
            while (!Kryt1(a, b, eps, fun))
            {
                if (f_1 < f_2)
                {
                    b = d;
                    d = c;
                    f_2 = f_1;
                    c = a + (b - a) * invphi2;
                    f_1 = fun(c);
                }
                else if (f_1 == f_2)
                {
                    a = c;
                    b = d;
                    c = a + (b - a) * invphi2;//x_1
                    d = a + (b - a) * invphi;//x_2
                    f_1 = fun(c);
                    f_2 = fun(d);
                }
                else
                {
                    a = c;
                    c = d;
                    f_1 = f_2;
                    d = a + (b - a) * invphi;
                    f_2 = fun(d);
                }
            }
            return (a + b) / 2;
        }
    }
}
