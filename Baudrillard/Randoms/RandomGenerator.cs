using System;

namespace Baudrillard.Randoms
{
    public static class RandomGenerator
    {
        private static Random _random = new Random();

        public static double Nativo()
        {
            return _random.NextDouble();
        }

        public static int Uniform(int desde, int hasta)
        {
            return (int)(_random.NextDouble() * (hasta - desde) + desde);
        }

        public static double Uniform(double desde, double hasta)
        {
            return _random.NextDouble() * (hasta - desde) + desde;
        }

        public static double CongruencialMixto(int m, int a, int b, double x0)
        {
            return ((a * x0) - 1 + b) % m;
        }

        public static int Poisson(double lambda)
        {
            double a = Math.Exp(-lambda);
            double s = 1.0;
            int x = 0;

            do
            {
                x++;
                s *= _random.NextDouble();
            }
            while (s > a);

            return x - 1;
        }

        public static double Exponential(double lambda)
        {
            return -lambda * Math.Log(_random.NextDouble());
        }

        public static double Normal(double media = 0, double desviacionEstandar = 1)
        {
            var u1 = _random.NextDouble();
            var u2 = _random.NextDouble();

            var rand_std_normal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            var rand_normal = media + desviacionEstandar * rand_std_normal;

            return rand_normal;
        }
    }
}
