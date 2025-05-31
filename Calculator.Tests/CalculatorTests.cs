using System;
using Xunit;

public class CalculatorTests
{
    private readonly Calculator _calculator = new();

    // ========================================
    // BLACK-BOX TESTING METHODOLOGIES
    // ========================================

    // ------------------------
    // 1. EQUIVALENCE CLASS PARTITIONING
    // ------------------------

    [Theory]
    [InlineData(10, 2, 5)]   // Valid positive numbers
    [InlineData(-10, 2, -5)] // Negative dividend
    [InlineData(10, -2, -5)] // Negative divisor
    public void Divide_EquivalenceClasses_ValidInputs(int a, int b, int expected)
    {
        Assert.Equal(expected, _calculator.Divide(a, b));
    }

    [Fact]
    public void Divide_EquivalenceClasses_InvalidInput_DivisionByZero()
    {
        Assert.Throws<ArgumentException>(() => _calculator.Divide(10, 0));
    }

    [Theory]
    [InlineData(1, false)]  // Non-prime > 1
    [InlineData(2, true)]   // Smallest prime
    [InlineData(17, true)]  // Prime number
    [InlineData(4, false)]  // Composite number
    public void IsPrime_EquivalenceClasses(int number, bool expected)
    {
        Assert.Equal(expected, _calculator.IsPrime(number));
    }

    // ------------------------
    // 2. BOUNDARY VALUE ANALYSIS
    // ------------------------

    [Theory]
    [InlineData(int.MaxValue, 1, int.MaxValue)]     // Maximum positive value
    [InlineData(int.MinValue + 1, 1, int.MinValue + 1)] // Minimum value (avoiding overflow)
    [InlineData(1, 1, 1)]                           // Minimum positive values
    public void Divide_BoundaryValues(int a, int b, int expected)
    {
        Assert.Equal(expected, _calculator.Divide(a, b));
    }

    [Theory]
    [InlineData(0, false)]   // Boundary: zero
    [InlineData(1, false)]   // Boundary: one (not prime)
    [InlineData(2, true)]    // Boundary: smallest prime
    public void IsPrime_BoundaryValues(int number, bool expected)
    {
        Assert.Equal(expected, _calculator.IsPrime(number));
    }

    [Theory]
    [InlineData(2, 0, 1)]    // Exponent = 0 (boundary)
    [InlineData(1, 5, 1)]    // Base = 1 (boundary)
    public void Power_BoundaryValues(int baseNum, int exponent, int expected)
    {
        Assert.Equal(expected, _calculator.Power(baseNum, exponent));
    }

    // ========================================
    // WHITE-BOX TESTING METHODOLOGIES
    // ========================================

    // ------------------------
    // 3. STATEMENT COVERAGE
    // ------------------------

    [Fact]
    public void Add_StatementCoverage()
    {
        int result = _calculator.Add(2, 3);
        Assert.Equal(5, result);
    }

    [Fact]
    public void Subtract_StatementCoverage()
    {
        Assert.Equal(4, _calculator.Subtract(7, 3));
    }

    [Fact]
    public void Multiply_StatementCoverage()
    {
        Assert.Equal(12, _calculator.Multiply(4, 3));
    }

    // ------------------------
    // 4. BRANCH/DECISION COVERAGE
    // ------------------------

    [Theory]
    [InlineData(2, true)]   // Even number (true branch)
    [InlineData(3, false)]  // Odd number (false branch)
    public void IsEven_BranchCoverage(int number, bool expected)
    {
        Assert.Equal(expected, _calculator.IsEven(number));
    }

    [Theory]
    [InlineData(10, 3, 1)]  // Normal modulo operation
    public void Modulo_BranchCoverage_ValidInput(int a, int b, int expected)
    {
        Assert.Equal(expected, _calculator.Modulo(a, b));
    }

    [Fact]
    public void Modulo_BranchCoverage_Exception()
    {
        Assert.Throws<ArgumentException>(() => _calculator.Modulo(5, 0));
    }

    [Theory]
    [InlineData(1, false)]   // <= 1 branch (false)
    [InlineData(9, false)]   // Loop finds divisor (false)
    [InlineData(7, true)]    // Loop completes without divisor (true)
    public void IsPrime_BranchCoverage(int number, bool expected)
    {
        Assert.Equal(expected, _calculator.IsPrime(number));
    }

    // ------------------------
    // 5. CONDITION COVERAGE
    // ------------------------

    [Theory]
    [InlineData(6, 2, 3)]    // Both a and b positive
    [InlineData(-6, 2, -3)]  // a negative, b positive
    [InlineData(6, -2, -3)]  // a positive, b negative
    [InlineData(0, 2, 0)]    // a zero, b positive
    public void Divide_ConditionCoverage(int a, int b, int expected)
    {
        Assert.Equal(expected, _calculator.Divide(a, b));
    }

    // ------------------------
    // 6. PATH COVERAGE / BASIS PATH TESTING
    // ------------------------

    [Theory]
    [InlineData(2, 3, 8)]    // Path: normal execution through loop
    [InlineData(5, 0, 1)]    // Path: skip loop (exponent = 0)
    [InlineData(3, 1, 3)]    // Path: single iteration
    public void Power_PathCoverage_ValidInputs(int baseNum, int exponent, int expected)
    {
        Assert.Equal(expected, _calculator.Power(baseNum, exponent));
    }

    [Fact]
    public void Power_PathCoverage_ExceptionPath()
    {
        Assert.Throws<ArgumentException>(() => _calculator.Power(2, -1));
    }

    // ========================================
    // MUTATION TESTING
    // ========================================

    // ------------------------
    // 7. MUTANT KILLING TESTS
    // ------------------------

    [Fact]
    public void IsEven_KillsModuloMutant()
    {
        Assert.True(_calculator.IsEven(0));   // Kills % 2 != 0 mutant
        Assert.False(_calculator.IsEven(1));  // Kills % 2 == 1 mutant
    }

    [Fact]
    public void Divide_KillsOperatorMutants()
    {
        Assert.Equal(3, _calculator.Divide(9, 3));  // Kills / -> * mutant
        Assert.Equal(-3, _calculator.Divide(9, -3)); // Kills sign mutants
    }

    [Fact]
    public void Add_KillsOperatorMutant()
    {
        Assert.Equal(5, _calculator.Add(2, 3));  // Kills + -> - mutant
    }

    [Fact]
    public void Power_KillsLoopMutants()
    {
        Assert.Equal(16, _calculator.Power(2, 4));  // Kills loop boundary mutants
        Assert.Equal(1, _calculator.Power(5, 0));   // Kills initialization mutants
    }

    // ========================================
    // ERROR HANDLING & ROBUSTNESS TESTING
    // ========================================

    [Fact]
    public void Divide_ByZero_ThrowsCorrectException()
    {
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Divide(10, 0));
        Assert.Contains("Divider cannot be zero", exception.Message);
    }

    [Fact]
    public void Modulo_ByZero_ThrowsCorrectException()
    {
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Modulo(5, 0));
        Assert.Contains("Modulo by zero not allowed", exception.Message);
    }

    [Fact]
    public void Power_NegativeExponent_ThrowsCorrectException()
    {
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Power(2, -1));
        Assert.Contains("Negative exponent not supported", exception.Message);
    }
}