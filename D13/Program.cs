




List<AocMachine> machines = new List<AocMachine>();
machines.Add(new D13P1("data", Int32.MaxValue) );
machines.Add(new D13P1("data2", 1));
// machines.Add(new D13P1("data3"));
// machines.Add(new D13P1("data4"));


foreach(var m in machines)
{
   m.run();
}



