using System;
using System.Numerics;
using System.Collections.Generic;
//przenosimy
using FibSer;
using static FibSer.FibSe;
using GoldSer;
using static GoldSer.Gold;
using Bisection;
using static Bisection.Bin;
using Lagrange;
using static Lagrange.par;
//zwracaja przedzial
using WolfePrzedzial;
using static WolfePrzedzial.Awoo;
using DirectSearch;
using static DirectSearch.Dirsearch;
//wspolne
using wspolne;
using static wspolne.Zbior;


namespace AI_zaj1
{
    class Program
    {
        
        
        //static List<ulong> Fibon = new List<ulong>(); darujemy sobie generowanie listy fib


        public static double Fun1(double x) => Math.Pow(x, 4) + 2 * x + 1;
        public static double PochodFun1(double x) => 4 * Math.Pow(x, 3) + 2;
        public static double Fun2(double x) => Math.Exp(-x) + Math.Pow(x, 2);
        public static double PochodFun2(double x) => -Math.Exp(-x) + 2 * x;
        public static double[] Fun3(double x, double y) => new double[] { Math.Sin(x + Math.PI), 0 };
        //double x=0, x=1.5*Math.PI
        public static double[] GradFun3(double x, double y)//vectory kiedyśtam
        {
            //double[] p = ;
            return new double[] { -Math.Cos(x), 0 };
        }
        public static double Fun31(double x) =>  Math.Sin(x + Math.PI);
        static double x1 = 0, x2 = 1.5 * Math.PI;
        public static double GradFun31(double x)//vectory kiedyśtam
        {
            //double[] p = ;
            return -Math.Cos(x);
        }
        public static double Fun4(double[] x) 
        {
            if (x.Length <= 0)
                return double.NaN;
            if (x.Length == 1)
                return 100 * Math.Pow(x[0], 4) + Math.Pow(1 - x[0], 2);
            return 100 * Math.Pow(x[1] - x[0] * x[0], 2) + Math.Pow(1 - x[0], 2); 
        }
        public static double[] GradFun4(double[] x)
        {
            /*if (KrytTab(x, stary))
                throw new Exception("obliczasz z pustych tablic");*/
            double[] p = new double[2]; //oryginalnie - stary.Length
            if(x.Length==1)
            {
                p[0] = 2 * (-1 + x[0] + 200 * Math.Pow(x[0], 3));
                p[1] = 200 * (-Math.Pow(x[0], 2));
                return p;
            }
            p[0] = 2 * (-1 + x[0] + 200 * Math.Pow(x[0], 3) - 200 * x[0] * x[1]);
            p[1] = 200 * (-Math.Pow(x[0], 2) + x[1]);
            /*for (int i = 2; i < stary.Length; i++)
            {
                p[i] = stary[i];
            }*/
            return (p);
        }
        //to jest właśnie rosenbrock, przykład
        static void Main(string[] args)
        {
            double[] T1 = { -1.0, 1.0 };
            double[] t2 = { 1.0, 1.0 };
            
            Matma fun = Fun1;
            Matma g = PochodFun1;
            Kryt kf = Kryt1;
            
            //fun += Fun2;
            double eps = Math.Pow(10, -5);
            double[] binser = Pattern(-1, fun);
            Console.WriteLine(binser[0] + " " + binser[1]);
            Console.WriteLine(FibSearch(-1, 0, fun, eps)+" "+FibSearch(binser[0], binser[1], fun, eps));
            Console.WriteLine(GoldSearch(-1, 0, fun, eps) + " " + GoldSearch(binser[0], binser[1], fun, eps));
            Console.WriteLine(BinSearch(-1, 0, fun, g, eps) + " " + BinSearch(binser[0], binser[1], fun, g, eps));
            Console.WriteLine(Parabolic(-0.75, -1, 0, fun, eps, eps) + " " + Parabolic(-0.75, binser[0], binser[1], fun, eps, eps));//necessary
            fun = Fun2;
            g = PochodFun2;
            binser = Pattern(0, fun);
            Console.WriteLine(binser[0] + " " + binser[1]);
            Console.WriteLine(FibSearch(0, 1, fun, eps)+ " "+ FibSearch(binser[0], binser[1], fun, eps));
            Console.WriteLine(GoldSearch(0, 1, fun, eps)+" " + GoldSearch(binser[0], binser[1], fun, eps)); //fun2
            Console.WriteLine(BinSearch(0, 1, fun, g, eps)+" " + BinSearch(binser[0], binser[1], fun,g, eps));//necessary, ups nie działa
            Console.WriteLine(Parabolic(0.5, 0, 1, fun, eps, eps)+" " + Parabolic(0.3, binser[0], binser[1], fun, eps, eps));//konsultacja
            //Console.WriteLine((0 + 1) / eps);
            
            Console.WriteLine("optimal step size to put back in the fibsearch algorithm or sth");
            fun = Fun31;
            g = GradFun31;
            Dir k = Desc1;
            //Console.WriteLine(Wolfe1(x1, fun, g, eps, k));//2
            //Console.WriteLine(Wolfe1(x2, fun, g, eps, k));//1024
            Console.WriteLine(LinSearch1(x1, fun, g, eps, k));
            Console.WriteLine(LinSearch1(x2, fun, g, eps, k));
            //double f = Wolfe(T1, Fun4, GradFun4, eps, t2);

            Console.WriteLine("optimal step size to put back in the fibsearch algorithm or sth");
            Matma2 funi = Fun4;
            Matma1 guni = GradFun4;
            Dir2 k2 = Desc11;
           /* double[] x1 = new double[] { 0 , 0};
            double[] x2 = new double[] { 1.5 * Math.PI };*/
            Console.WriteLine(Wolfe(T1, funi, guni, eps, t2));//2
            double[] sol = LinSearch(T1, funi, guni, eps, t2);
            foreach (double gi in sol)
                Console.WriteLine(gi); //*/
            //Console.WriteLine();
            //Console.WriteLine(Wolfe(x2, funi, guni, eps, k2));//1024
            /*
            double f = Wolfe(T1, Fun4, GradFun4, eps, t2);
            
            Console.WriteLine(f);
            foreach(double g in f)
                           Console.WriteLine(g);*/
        }
    }
}
