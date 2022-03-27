using System;

namespace wspolne
{
    public delegate double Matma(double x);
    public delegate double[] Matma1(double[] x);
    public delegate double Matma2(double[] x );
   // public delegate double[] Matma3(double x, double y);
    public delegate double Dir(double x, Matma g);
    public delegate double[] Dir2(double[] x, Matma1 m);
    public delegate bool Kryt(double a, double b, double eps, Matma f);
    public static class Zbior
    {
        public static double alfa_min = 1; // Math.Pow(2, -6);
        public static double eps = Math.Pow(10, -5);
        public static bool Kryt0(double a, double b, double eps, Matma g) => Math.Abs(g(b - a)) < eps;
        //ustawić a na 0
        public static bool Kryt1(double a, double b, double eps, Matma f) => Math.Abs(b - a) < eps;
        public static bool Kryt11(double a, double b, double eps, Matma f) => Math.Abs(b - a) < eps*Math.Abs(a);
        public static bool Kryt2(double a, double b, double eps, Matma f) => Math.Abs(f(b) - f(a)) < eps;
        public static bool Kryt2(double[] a, double[] b, double eps, Matma2 f) => Math.Abs(f(b) - f(a)) < eps;
        public static bool Kryt21(double a, double b, double eps, Matma f) => Math.Abs(f(b) - f(a)) < eps*Math.Abs(f(a));
        public static bool Kryt3(double a, double b, double c, double eps, Matma f)
        {
            double pom = (c - a) * f(b);
            pom += (b - c) * f(a);
            pom += (a - b) * f(c);
            return Math.Abs(pom) <= eps;
        }
        public static bool KrytTab(double[] x, double[] y) => x == null || y == null || x.Length <= 0 || y.Length <= 0;
        public static double Desc0(double x_k, Matma grad) => 1;//miał być newton
        public static double Desc1(double x_k, Matma grad) => -grad(x_k);//steepest descent
        public static double[] Desc11(double[] x_k, Matma1 grad)
        {
            double[] pom = new double[x_k.Length];
            //for(int i=0; i<pom.Length; i++)
            return Dodaj(pom, grad(x_k), -1);
        }
        public static double Dot(double[] x, double[] y)
        {
            double p = 0;
            
            if (KrytTab(x,y))
            {
                return double.NaN;
            }
            int mi = Math.Min(x.Length, y.Length);
            for (int i = 0; i < mi; i++)
            {
                p += x[i] * y[i];
            }
            return p;
        }
        public static double[] Dodaj(double[] x, double[] y, double mnoz)
        {
            double[] pom;
            if (KrytTab(x,y) || x.Length != y.Length)
                throw new Exception("Dane nie pasują");
            else
            {
                pom = new double[x.Length];
                for(int i=0; i<pom.Length; i++)
                {
                    pom[i] = x[i] + y[i] * mnoz;
                }
                return pom;
            }
        }
        public static double[] Dodaj(double[]x, double[] y)
        {
            /*if (x == null || y == null || x.Length <= 0 || y.Length <= 0 || x.Length != y.Length)
                throw new Exception("Dane nie pasują");
            double[] pom = new double[x.Length];
            for (int i = 0; i < pom.Length; i++)
            {
                pom[i] = 1;
            }*/
            return Dodaj(x, y, 1);
        }
    }
}
