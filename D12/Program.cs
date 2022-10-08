


List<AocMachine> programs = new List<AocMachine>();

programs.Add(new D12P1("data"));
// programs.Add(new D12P1("data2"));
// programs.Add(new D12P1("data3"));
// programs.Add(new D12P1("data4"));


programs.Add(new D12P2("data"));
// programs.Add(new D12P2("data2"));
// programs.Add(new D12P2("data3"));
// programs.Add(new D12P2("data4"));

foreach(var d in programs)
{
   d.run();
}
