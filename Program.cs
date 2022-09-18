using System.Numerics;
using Primes.Divisibility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();
bool isBusy = false;


app.MapPost("/divisibility", (DUnit input) =>
{
    isBusy = true;
    var Ad = new AdvancedDivisibility(input.Divisor);
    isBusy = false;
    return Ad.IsDivisible(input.Dividend);
});


app.MapGet("/state", () =>
{
    return isBusy;
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