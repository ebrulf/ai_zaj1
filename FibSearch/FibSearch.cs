using System;
using wspolne;

namespace FibSer
{
    //public delegate double Matma(double x);
    public class FibSe
    {
        public static double[] Fibo = {0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181, 6765, 10946, 17711, 28657, 46368, 75025,
 121393, 196418, 317811, 514229, 832040, 1346269,
 2178309, 3524578, 5702887, 9227465, 14930352, 24157817,
 39088169, 63245986, 102334155 };
        public static double Fib(double d)
        {
            //ulong help1, help2; do tego listę
            for (int i = 0; i < Fibo.Length; i++)
            {
                if (Fibo[i] > d)
                    return Fibo[i];
            }
            //if(Fibo[i]<d)
            return 0;
        }
        public static int Fibnum(double d)
        {
            int licz = 0;
            for (int i = 0; i < Fibo.Length; i++)
            {
                if (Fibo[i] < d)
                    licz++;
            }
            return licz;
        }
        public static double FibSearch(double a, double b, Matma fun, double eps)
        {
            double c = (b - a) / eps;
            double Fi = (Fib((Math.Floor(c))));
            int n = Fibnum((Fi));
            //Console.WriteLine(n);
            int k = 1;
            double x_1 = a + (Fibo[n - 2] / Fibo[n]) * (b - a);
            double x_2 = a + (Fibo[n - 1] / Fibo[n]) * (b - a);
            double f_1 = fun(x_1);
            double f_2 = fun(x_2);
            while (k < n - 2)
            {
                if (f_1 < f_2)
                {
                    b = x_2;
                    x_2 = x_1;
                    f_2 = f_1;
                    x_1 = a + (Fibo[n - k - 2] / Fibo[n - k]) * (b - a);
                    f_1 = fun(x_1);
                }
                else
                {
                    a = x_1;
                    x_1 = x_2;
                    f_1 = f_2;
                    x_2 = a + (Fibo[n - k - 1] / Fibo[n - k]) * (b - a);
                    f_2 = fun(x_2);
                }
                k++;//tego zapomniałem
            }
            if (f_1 < f_2)
            {
                b = x_2;
                x_2 = x_1;
                f_2 = f_1;
            }
            else
            {
                a = x_1;
            }
            x_1 = x_2 - 0.1 * (b - a);
            f_1 = fun(x_1);
            if (f_1 < f_2)
                return (a + x_1) / 2.0;
            else if (f_1 == f_2)
                return (x_1 + x_2) / 2.0;
            else
                return (x_1 + b) / 2.0;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
