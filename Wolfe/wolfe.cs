using System;
using DirectSearch;
using static DirectSearch.Dirsearch;
using wspolne;
using static wspolne.Zbior;

namespace WolfePrzedzial
{
    public class Awoo
    {
        //wolfe concośtam
        static double c_1 = Math.Pow(10, -4);
        static double c_2 = 0.1;
        static int k = 10;
        public static double Wolfe(double[] x_k, Matma2 fun, Matma1 g, double eps, Dir2 p)//jeszcze nie umiem wyliczać grad
        {
            return Wolfe(0, double.PositiveInfinity, x_k, fun, g, eps, p);//a,b do szukania alfa
        }
        public static double Wolfe(double[] x_k, Matma2 fun, Matma1 g, double eps, double[] p)//jeszcze nie umiem wyliczać grad
        {
            return Wolfe(0, double.PositiveInfinity, x_k, fun, g, eps, p);//a,b do szukania alfa
        }
        public static double Wolfe1(double x_k, Matma fun, Matma g, double eps, Dir pi)//jeszcze nie umiem wyliczać grad
        {
            return Wolfe1(0, double.PositiveInfinity, x_k, fun, g, eps, pi);
        }
        //1 wolfe gędzie 1D
        //2 będziemy martwić się potem
        public static double Wolfe1(double a, double b, double x_k, Matma fun, Matma g, double eps, Dir pi)
        {
            double alfa = alfa_min, f_next, g_next, x_next, p;
            //p_next, a_next=1001;
            
            for (int j = 0; j < k; j++)//będą kryteria wolfe'a
            {
                p = pi(x_k, g);//steepest descent
                x_next = x_k + alfa * p;
                f_next = fun(x_next);
                g_next = g(x_next);
                if (f_next <= fun(x_k) + c_1 * alfa * (g(x_k)* p))//pierwsze kryterium
                {
                    if (((g_next*p)) >= c_2 * ((g(x_k)* p)))//drufie kryterium
                    {
                        return alfa;
                    }
                    else
                    {
                        a = alfa;
                        alfa = Math.Min(2 * alfa, (alfa + b) / 2.0);
                    }
                }
                else
                {
                    b = alfa;
                    alfa = (alfa + a) / 2.0;
                }
                x_k = x_next;

            }
            return alfa;
        }
        /*public static double LinSearch1(double x_k, Matma fun, Matma g, double eps, Dir pi)
        {
            //double[] pom = Pattern(x_k, fun);
            //Console.WriteLine(pom[0] + " " + pom[1]);
            //double alfa = Wolfe1(pom[0], pom[1], x_k, fun, g, eps, pi);
            return LinSearch1(0, Double.PositiveInfinity, x_k, fun, g, eps, pi);
        }*/
        public static double LinSearch1(double x_k, Matma fun, Matma g, double eps, Dir pi)
        {
            double f_next, g_next, x_next, p, alfa;
            
            for (int j = 0; j < k; j++)
            {
                p = pi(x_k, g);//5 slajd
                alfa = Wolfe1(x_k, fun, g, eps, pi);
                x_next = x_k + alfa * p;
                f_next = fun(x_next);
                g_next = g(x_next);
                if (Kryt2(x_k, x_next, eps, fun))
                    return x_next;
                x_k = x_next;
                
            }
            return 0;//it works!
        }
        public static double[] LinSearch(double[] x_k, Matma2 fun, Matma1 g, double eps, Dir2 pi)
        {
            double[] g_next, p, x_next = new double[x_k.Length];
            double alfa, f_next;

            for (int j = 0; j < k; j++)
            {
                p = pi(x_k, g);//5 slajd
                alfa = Wolfe(x_k, fun, g, eps, p);
                x_next = Dodaj(x_k, p, alfa);
                f_next = fun(x_next);
                g_next = g(x_next);
                if (Kryt2(x_k, x_next, eps, fun))
                    return x_next;
                x_k = x_next;

            }
            return x_next;//it works!
        }
        public static double[] LinSearch(double[] x_k, Matma2 fun, Matma1 g, double eps, double[] pi)
        {
            double[] g_next, x_next = new double[x_k.Length];
            double alfa, f_next;

            for (int j = 0; j < k; j++)
            {
                //p = pi(x_k, g);//5 slajd
                alfa = Wolfe(x_k, fun, g, eps, pi);
                x_next = Dodaj(x_k, pi, alfa);
                f_next = fun(x_next);
                g_next = g(x_next);
                if (Kryt2(x_k, x_next, eps, fun))
                    return x_next;
                x_k = x_next;

            }
            return x_next;//it works!
        }
        public static double Wolfe(double a, double b, double[] x_k, Matma2 fun, Matma1 g, double eps, Dir2 pi)
        {
            double alfa = alfa_min, f_next;
            double[] g_next, p,x_next = new double[x_k.Length];

            for (int j = 0; j < k; j++)//będą kryteria wolfe'a
            {
                p = pi(x_k, g);
                x_next = Dodaj(x_k, p, alfa);
                f_next = fun(x_next);
                g_next = g(x_next);
                if (f_next <= fun(x_k) + c_1 * alfa * Dot(g(x_k), p))//pierwsze kryterium
                {
                    if ((Dot(g_next, p)) >= c_2 * (Dot(g(x_k), p)))//drufie kryterium
                    {
                        return alfa;
                    }
                    else
                    {
                        a = alfa;
                        alfa = Math.Min(2 * alfa, (alfa + b) / 2.0);
                    }
                }
                else
                {
                    b = alfa;
                    alfa = (alfa + a) / 2.0;
                }
                x_k = x_next;

            }
            return alfa;
        }
        public static double Wolfe(double a, double b, double[] x_k, Matma2 fun, Matma1 g, double eps, double[] p)
        {
            double alfa = alfa_min, f_next;
            double[] g_next, x_next = new double[x_k.Length];
            
            for (int j = 0; j < k; j++)//będą kryteria wolfe'a
            {
                //p = pi(x_k, g);
                x_next = Dodaj(x_k, p, alfa);
                f_next = fun(x_next);
                g_next = g(x_next);
                if (f_next <= fun(x_k) + c_1 * alfa * Dot(g(x_k), p))//pierwsze kryterium
                {
                    if ((Dot(g_next, p)) >= c_2 * (Dot(g(x_k), p)))//drufie kryterium
                    {
                        return alfa;
                    }
                    else
                    {
                        a = alfa;
                        alfa = Math.Min(2 * alfa, (alfa + b) / 2.0);
                    }
                }
                else
                {
                    b = alfa;
                    alfa = (alfa + a) / 2.0;
                }
                x_k = x_next;

            }
            return alfa;
        }

    }
}
