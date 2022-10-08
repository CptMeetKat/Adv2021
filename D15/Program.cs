





List<AocMachine> machines = new List<AocMachine>();

// machines.Add(new DJTest("data"));
// machines.Add(new DJTest("data2"));
machines.Add(new DJTest("data3"));
machines.Add(new DJTest("data2"));
machines.Add(new DJTest("data"));


machines.Add(new D15P1("data3"));
machines.Add(new D15P1("data2"));
machines.Add(new D15P1("data"));
// machines.Add(new D15P1("data4"));


// machines.Add(new D15P2("data2"));
// machines.Add(new D15P2("data"));




foreach(var m in machines)
   m.run();


   //699 is too high
   //698
   //600 too low


   //https://en.wikipedia.org/wiki/Dijkstra's_algorithm
   //http://www.gitta.info/Accessibiliti/en/html/Dijkstra_learningObject1.html
   //https://favtutor.com/blogs/dijkstras-algorithm-cpp