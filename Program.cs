using System.Numerics;
using Primes.Divisibility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();


app.MapGet("/divisibility", (DUnit input) =>
{
    var Ad = new AdvancedDivisibility(input.Divisor);
    return Ad.IsDivisible(input.Dividend);
});



app.Run();


class DUnit
{
    public BigInteger Divisor, Dividend;
    public bool IsBig;
    DUnit(BigInteger divisor, BigInteger dividend, bool isBig)
    {
        this.IsBig = isBig;
        this.Dividend = dividend;
        this.Divisor = divisor;
    }
}