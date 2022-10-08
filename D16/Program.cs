



List<AocMachine> machines = new List<AocMachine>();
machines.Add(new D16P1("data"));
// machines.Add(new D16P1("data2"));


machines.Add(new D16P2("data2"));
machines.Add(new D16P2("data"));

//2221103336 is too low
//6516070632 is too low

foreach(var m in machines)
   m.run();