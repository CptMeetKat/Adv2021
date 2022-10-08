using Attempt1;


var watch = System.Diagnostics.Stopwatch.StartNew();
// the code that you want to measure comes here




List<AocMachine> machines = new List<AocMachine>();
machines.Add(new D17P1("data2"));
machines.Add(new D17P1("data"));

foreach(var m in machines)
{
   Console.WriteLine("({0})", m.identify());
   m.run();
}


//9316 is too low

watch.Stop();
var elapsedMs = watch.ElapsedMilliseconds;
Console.WriteLine("\nExecution Time: " + elapsedMs);