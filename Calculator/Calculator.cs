public class Calculator
{
    public int Divide(int a, int b)
    {
        if (b == 0)
            throw new ArgumentException("Divider cannot be zero.");
        return a / b;
    }

    public bool IsEven(int number)
    {
        return number % 2 == 0;
    }

    public int Add(int a, int b) => a + b;

    public int Subtract(int a, int b) => a - b;

    public int Multiply(int a, int b) => a * b;

    public int Modulo(int a, int b)
    {
        if (b == 0)
            throw new ArgumentException("Modulo by zero not allowed.");
        return a % b;
    }

    public bool IsPrime(int number)
    {
        if (number <= 1) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;
        
        for (int i = 3; i <= Math.Sqrt(number); i += 2)
        {
            if (number % i == 0) return false;
        }
        return true;
    }

    public int Power(int baseNum, int exponent)
    {
        if (exponent < 0) throw new ArgumentException("Negative exponent not supported.");
        int result = 1;
        for (int i = 0; i < exponent; i++)
        {
            result *= baseNum;
        }
        return result;
    }
}
