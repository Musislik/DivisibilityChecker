using System.Numerics;
using Primes.Divisibility;
using Primes.Communication;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions((options) => { options.JsonSerializerOptions.PropertyNamingPolicy = null; });

var app = builder.Build();
bool isBusy = false;
DcConfiguration config = new DcConfiguration(Convert.ToUInt32(Environment.GetEnvironmentVariable("DCID")), ipFromString(Environment.GetEnvironmentVariable("IP")));



//divisor = input[0]
app.MapPost("/divisibility", (List<byte[]> input) =>
{
    var sw = new Stopwatch();
    isBusy = true;
    //Console.WriteLine("Divisibility");
    
    var divisor = new BigInteger(input[0]);
    var dividend = new BigInteger(input[1]);
    sw.Start();
    if (BasicDivisibility.DivisibleByBasic(dividend)) return false;
    global::System.Console.WriteLine("BD: " + sw.ElapsedMilliseconds);
    var Ad = new AdvancedDivisibility(divisor);
    bool output = Ad.IsDivisible(dividend);
    sw.Stop();
    global::System.Console.WriteLine(sw.ElapsedMilliseconds);
    isBusy = false;
    return output;
});


app.MapGet("/state", () =>
{
    //global::System.Console.WriteLine("sending state");
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

