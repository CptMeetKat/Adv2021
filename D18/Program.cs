var watch = System.Diagnostics.Stopwatch.StartNew();


List<AocMachine> machines = new List<AocMachine>();
// machines.Add(new D18P1("data2"));
machines.Add(new D18P1("data"));
machines.Add(new D18P2("data"));
// machines.Add(new D18P2("data2"));

foreach(var m in machines)
{
   Console.WriteLine("({0})", m.identify());
   m.run();
}


