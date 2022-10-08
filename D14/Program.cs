
List<AocMachine> machines = new List<AocMachine>();


// machines.Add(new D14P1("data2", 40));
machines.Add(new D14P1("data2", 10));
// machines.Add(new D14P1("data2", 10));

// machines.Add(new D14P2("data2", 4));
// machines.Add(new D14P2("data", 40));


foreach(var m in machines)
{
   m.run();
}
