using System.Numerics;
using Primes.Divisibility;
using Primes.Communication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();
bool isBusy = false;
DcConfiguration config = new DcConfiguration(Convert.ToUInt32(Environment.GetEnvironmentVariable("DCID")), ipFromString(Environment.GetEnvironmentVariable("IP")));

app.MapPost("/divisibility", (DUnit input) =>
{
    global::System.Console.WriteLine("Divisibility");
    isBusy = true;
    var Ad = new AdvancedDivisibility(input.Divisor);
    isBusy = false;
    return Ad.IsDivisible(input.Dividend);
});

app.MapGet("/state", () =>
{
    global::System.Console.WriteLine("sending state");
    return isBusy;
});

app.MapPost("/setup", (DcConfiguration newConfig) =>
{
    global::System.Console.WriteLine("setup");
    try
    {
        config = newConfig;
        return true;
    }
    catch
    {
        return false;
    }
});

app.Run();

byte[] ipFromString(string input)
{
    int k = 0;
    byte[] output = new byte[4];
    for (int i = 0; i < input.Length; i++)
    {        
        if (input[i] == '.') k++;
        else output[k] = (byte)(output[k] * 10 + input[i] - 48);
    }
    return output;
}