using System;


class Program
{
    static void Main(string[] args)
    {
        MyFrac frac1 = new MyFrac(15, 40);
        MyFrac frac2 = new MyFrac(17, 7);

        Console.WriteLine("frac1: " + frac1);
        Console.WriteLine("frac2: " + frac2);

        Console.WriteLine("ToStringWithIntegerPart(frac2): " + frac2.ToStringWithIntegerPart());

        Console.WriteLine("DoubleValue(frac1): " + frac1.DoubleValue());

        MyFrac sum = MyFrac.Plus(frac1, frac2);
        Console.WriteLine("sum (frac1 + frac2): " + sum);

        MyFrac difference = MyFrac.Minus(frac1, frac2);
        Console.WriteLine("difference (frac1 - frac2): " + difference);

        MyFrac product = MyFrac.Multiply(frac1, frac2);
        Console.WriteLine("product (frac1 * frac2): " + product);

        MyFrac quotient = MyFrac.Divide(frac1, frac2);
        Console.WriteLine("quotient (frac1 / frac2): " + quotient);

        MyFrac calcSum1Result = MyFrac.CalcSum1(5);
        Console.WriteLine("CalcSum1(5): " + calcSum1Result);

        MyFrac calcSum2Result = MyFrac.CalcSum2(5);
        Console.WriteLine("CalcSum2(5): " + calcSum2Result);
    }
}

public class MyFrac
{
    public long Nom { get; private set; }
    public long Denom { get; private set; }

    public MyFrac(long nom, long denom)
    {
        if (denom == 0)
            throw new ArgumentException("Denominator cannot be zero.");

        if (denom < 0)
        {
            nom = -nom;
            denom = -denom;
        }

        long gcd = GCD(Math.Abs(nom), denom);
        Nom = nom / gcd;
        Denom = denom / gcd;
    }

    private static long GCD(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    public override string ToString()
    {
        return $"{Nom}/{Denom}";
    }

    public string ToStringWithIntegerPart()
    {
        bool isNegative = Nom < 0;

        long integerPart = Math.Abs(Nom) / Math.Abs(Denom);
        long fractionalNom = Math.Abs(Nom) % Math.Abs(Denom);

        if (fractionalNom == 0)
        {
            return isNegative ? $"-{integerPart}" : integerPart.ToString();
        }
        else
        {
            return isNegative
                ? $"-({integerPart} + {fractionalNom}/{Denom})"
                : $"{integerPart} + {fractionalNom}/{Denom}";
        }
    }

    public  double DoubleValue()
    {
        return (double)Nom / Denom;
    }

    public static MyFrac Plus(MyFrac f1, MyFrac f2)
    {
        return new MyFrac(f1.Nom * f2.Denom + f2.Nom * f1.Denom, f1.Denom * f2.Denom);
    }

    public static MyFrac Minus(MyFrac f1, MyFrac f2)
    {
        return new MyFrac(f1.Nom * f2.Denom - f2.Nom * f1.Denom, f1.Denom * f2.Denom);
    }

    public static MyFrac Multiply(MyFrac f1, MyFrac f2)
    {
        return new MyFrac(f1.Nom * f2.Nom, f1.Denom * f2.Denom);
    }

    public static MyFrac Divide(MyFrac f1, MyFrac f2)
    {
        return new MyFrac(f1.Nom * f2.Denom, f1.Denom * f2.Nom);
    }

    public static MyFrac CalcSum1(int n)
    {
        MyFrac result = new MyFrac(0, 1);
        for (int i = 1; i <= n; i++)
        {
            MyFrac term = new MyFrac(1, i * (i + 1));
            result = Plus(result, term);
        }
        return result;
    }

    public static MyFrac CalcSum2(int n)
    {
        MyFrac result = new MyFrac(1, 1);
        for (int i = 2; i <= n; i++)
        {
            MyFrac term = new MyFrac(i + 1, 2 * i);
            result = Multiply(result, term);
        }
        return result;
    }
}

