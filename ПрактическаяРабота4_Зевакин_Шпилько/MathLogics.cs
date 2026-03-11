using System;

namespace ПрактическаяРабота4_Зевакин_Шпилько;

public class MathLogics
{
    public static double FirstCalculate(double x, double y, double z)
    {
        double part1 = Math.Log(Math.Pow(y, -Math.Sqrt(Math.Abs(x))));
        double part2 = x - y / 2;
        double part3 = Math.Pow(Math.Sin(Math.Atan(z)), 2);
        return part1 * part2 + part3;
    }

    public static double ThirdCalculate(double x, double b)
    {
        return Math.Pow(x, 4) + Math.Cos(2 + Math.Pow(x, 3) - b);
    }
}
    
