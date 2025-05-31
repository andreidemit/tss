using System;
using Xunit;

public class CalculatorTests
{
    private readonly Calculator _calculator = new();

    // ========================================
    // STRATEGII DE TESTARE BLACK-BOX
    // ========================================

    // ------------------------
    // 1. CLASE DE ECHIVALENȚĂ
    // ------------------------

    [Theory]
    [InlineData(10, 2, 5)]   // Numere pozitive valide
    [InlineData(-10, 2, -5)] // Deîmpărțit negativ
    [InlineData(10, -2, -5)] // Împărțitor negativ
    public void Divide_ClaseDeEchivalenta_InputuriValide(int a, int b, int expected)
    {
        Assert.Equal(expected, _calculator.Divide(a, b));
    }

    [Fact]
    public void Divide_ClaseDeEchivalenta_InputInvalid_ImpartireLaZero()
    {
        Assert.Throws<ArgumentException>(() => _calculator.Divide(10, 0));
    }

    [Theory]
    [InlineData(1, false)]  // Non-prim > 1
    [InlineData(2, true)]   // Cel mai mic număr prim
    [InlineData(17, true)]  // Număr prim
    [InlineData(4, false)]  // Număr compus
    public void IsPrime_ClaseDeEchivalenta(int number, bool expected)
    {
        Assert.Equal(expected, _calculator.IsPrime(number));
    }

    // ------------------------
    // 2. VALORI DE FRONTIERĂ
    // ------------------------

    [Theory]
    [InlineData(int.MaxValue, 1, int.MaxValue)]     // Valoare pozitivă maximă
    [InlineData(int.MinValue + 1, 1, int.MinValue + 1)] // Valoare minimă (evitând overflow)
    [InlineData(1, 1, 1)]                           // Valori pozitive minime
    public void Divide_ValoriDeFrontiera(int a, int b, int expected)
    {
        Assert.Equal(expected, _calculator.Divide(a, b));
    }

    [Theory]
    [InlineData(0, false)]   // Frontieră: zero
    [InlineData(1, false)]   // Frontieră: unu (nu este prim)
    [InlineData(2, true)]    // Frontieră: cel mai mic prim
    public void IsPrime_ValoriDeFrontiera(int number, bool expected)
    {
        Assert.Equal(expected, _calculator.IsPrime(number));
    }

    [Theory]
    [InlineData(2, 0, 1)]    // Exponent = 0 (frontieră)
    [InlineData(1, 5, 1)]    // Bază = 1 (frontieră)
    public void Power_ValoriDeFrontiera(int baseNum, int exponent, int expected)
    {
        Assert.Equal(expected, _calculator.Power(baseNum, exponent));
    }

    // ========================================
    // STRATEGII DE TESTARE WHITE-BOX
    // ========================================

    // ------------------------
    // 3. ACOPERIRE INSTRUCȚIUNE
    // ------------------------

    [Fact]
    public void Add_AcoperireInstructiune()
    {
        int result = _calculator.Add(2, 3);
        Assert.Equal(5, result);
    }

    [Fact]
    public void Subtract_AcoperireInstructiune()
    {
        Assert.Equal(4, _calculator.Subtract(7, 3));
    }

    [Fact]
    public void Multiply_AcoperireInstructiune()
    {
        Assert.Equal(12, _calculator.Multiply(4, 3));
    }

    // ------------------------
    // 4. ACOPERIRE DECIZIE & CONDIȚIE
    // ------------------------

    [Theory]
    [InlineData(2, true)]   // Număr par (ramura adevărată)
    [InlineData(3, false)]  // Număr impar (ramura falsă)
    public void IsEven_AcoperireDecizie(int number, bool expected)
    {
        Assert.Equal(expected, _calculator.IsEven(number));
    }

    [Theory]
    [InlineData(10, 3, 1)]  // Operație modulo normală
    public void Modulo_AcoperireDecizie_InputValid(int a, int b, int expected)
    {
        Assert.Equal(expected, _calculator.Modulo(a, b));
    }

    [Fact]
    public void Modulo_AcoperireDecizie_Exceptie()
    {
        Assert.Throws<ArgumentException>(() => _calculator.Modulo(5, 0));
    }

    [Theory]
    [InlineData(1, false)]   // Ramura <= 1 (fals)
    [InlineData(9, false)]   // Bucla găsește divizor (fals)
    [InlineData(7, true)]    // Bucla se completează fără divizor (adevărat)
    public void IsPrime_AcoperireDecizie(int number, bool expected)
    {
        Assert.Equal(expected, _calculator.IsPrime(number));
    }

    // Acoperire condiție - testarea tuturor combinațiilor de condiții
    [Theory]
    [InlineData(6, 2, 3)]    // Ambele a și b pozitive
    [InlineData(-6, 2, -3)]  // a negativ, b pozitiv
    [InlineData(6, -2, -3)]  // a pozitiv, b negativ
    [InlineData(0, 2, 0)]    // a zero, b pozitiv
    public void Divide_AcoperireConditie(int a, int b, int expected)
    {
        Assert.Equal(expected, _calculator.Divide(a, b));
    }

    // ------------------------
    // 5. CIRCUITE INDEPENDENTE
    // ------------------------

    [Theory]
    [InlineData(2, 3, 8)]    // Circuit: execuție normală prin buclă
    [InlineData(5, 0, 1)]    // Circuit: sări peste buclă (exponent = 0)
    [InlineData(3, 1, 3)]    // Circuit: o singură iterație
    public void Power_CircuiteIndependente_InputuriValide(int baseNum, int exponent, int expected)
    {
        Assert.Equal(expected, _calculator.Power(baseNum, exponent));
    }

    [Fact]
    public void Power_CircuiteIndependente_CircuitExceptie()
    {
        Assert.Throws<ArgumentException>(() => _calculator.Power(2, -1));
    }

    // ========================================
    // GENERATOR DE MUTANȚI & OMOARĂ MUTANȚI
    // ========================================

    // ------------------------
    // 6. OMOARĂ MUTANȚI NEECHIVALENȚI
    // ------------------------

    [Fact]
    public void IsEven_OmoaraMutantModulo()
    {
        Assert.True(_calculator.IsEven(0));   // Omoară mutantul % 2 != 0
        Assert.False(_calculator.IsEven(1));  // Omoară mutantul % 2 == 1
    }

    [Fact]
    public void Divide_OmoaraMutantiOperatori()
    {
        Assert.Equal(3, _calculator.Divide(9, 3));  // Omoară mutantul / -> *
        Assert.Equal(-3, _calculator.Divide(9, -3)); // Omoară mutanții de semn
    }

    [Fact]
    public void Add_OmoaraMutantOperator()
    {
        Assert.Equal(5, _calculator.Add(2, 3));  // Omoară mutantul + -> -
    }

    [Fact]
    public void Power_OmoaraMutantiBucla()
    {
        Assert.Equal(16, _calculator.Power(2, 4));  // Omoară mutanții frontieră buclă
        Assert.Equal(1, _calculator.Power(5, 0));   // Omoară mutanții inițializare
    }

    [Fact]
    public void Subtract_OmoaraMutantOperator()
    {
        Assert.Equal(1, _calculator.Subtract(4, 3));  // Omoară mutantul - -> +
    }

    [Fact]
    public void Multiply_OmoaraMutantOperator()
    {
        Assert.Equal(6, _calculator.Multiply(2, 3));  // Omoară mutanții * -> / sau * -> +
    }

    // ========================================
    // TESTARE ROBUSTEȚE & GESTIONARE ERORI
    // ========================================

    [Fact]
    public void Divide_ImpartireLaZero_AruncaExceptiaCorecta()
    {
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Divide(10, 0));
        Assert.Contains("Divider cannot be zero", exception.Message);
    }

    [Fact]
    public void Modulo_ModuloLaZero_AruncaExceptiaCorecta()
    {
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Modulo(5, 0));
        Assert.Contains("Modulo by zero not allowed", exception.Message);
    }

    [Fact]
    public void Power_ExponentNegativ_AruncaExceptiaCorecta()
    {
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Power(2, -1));
        Assert.Contains("Negative exponent not supported", exception.Message);
    }
}