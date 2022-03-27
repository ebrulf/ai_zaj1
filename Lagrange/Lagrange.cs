using System;
using wspolne;
using static wspolne.Zbior;
namespace Lagrange
{
    public class par
    {
        public static double Parabolic(double x0, double x1, double x2, Matma fun, double eps1, double eps2)
        {
            if (x1 < x0 && x0 < x2 && fun(x1) > fun(x0) && fun(x2) > fun(x0) && eps1>0 &&eps2>0)
            {
                double xs = (x1 + x2) / 2.0;//na pocz
                while (!(Kryt1(xs, x0, eps1, fun) && Kryt3(x0, x1, x2, eps2, fun)))
                {
                    xs = (x2 * x2 - x0 * x0) * fun(x1) + (x1 * x1 - x2 * x2) * fun(x0) + (x0 * x0 - x1 * x1) * fun(x2);
                    xs /= (x2 - x0) * fun(x1) + (x1 - x2) * fun(x0) + (x0 - x1) * fun(x2);
                    xs /= 2.0;
                    if (fun(x0) - fun(xs) < 0)
                    {
                        if (x0 < xs)
                            x2 = xs;
                        else
                            x1 = xs;
                    }
                    else if (fun(x0) - fun(xs) == 0)
                    {
                        if (x0 < xs)
                        {
                            x1 = x0;
                            x2 = xs;
                        }
                        else
                        {
                            x1 = xs;
                            x2 = x0;
                        }
                        x0 = (x1 + x2) / 2.0;
                    }
                    else
                    {
                        if (x0 > xs)
                            x2 = x0;
                        else
                            x1 = x0;
                        x0 = xs;
                    }
                }
                return x0;
            }
            //Console.WriteLine("No jest problem.");
            //Console.WriteLine(x1 + " " + x0 + " " + x2 + " czy w kolejności?\n " + fun(x1) + " " + fun(x0) + " " + fun(x2)+" czy niecka?");
            return double.NaN;
        }
    }
}
