
public class D14P2 : AocMachine 
{
   string polymer = "";
   Dictionary<string, string> rules;
   int runs;
   Dictionary<char, int> occurances;
   // Dictionary<string, int> polymerMap;

   public D14P2(string filename, int runs = 10) : base(filename)
   {
      rules = new Dictionary<string, string>();
      occurances = new Dictionary<char, int>();
      this.runs = runs;
      // polymerMap = new Dictionary<string, int>();
   }

   public override void run()
   {
      readData(filename);
      setPolymerTemplate();
      setInsertionRules();

      findOptimalPolymerFormula1();
      // findOptimalPolymerFormula2();
      // aggregateOccurances();
      // printOccurances();
      // calculateResult();

      displayResults();
   }

   private void calculateResult()
   {
      int big = Int32.MinValue;
      int small = Int32.MaxValue;

      foreach(char key in occurances.Keys)
      {
         big = Math.Max(big, occurances[key]);
         small = Math.Min(small, occurances[key]);
      }

      result = big - small;
   }

   private void printOccurances()
   {
      foreach(char key in occurances.Keys)
      {
         Console.WriteLine("{0} {1}", key, occurances[key]);
      }
      Console.WriteLine();
   }

   private void aggregateOccurances()
   {
      foreach(char s in polymer)
      {
         if ( occurances.ContainsKey(s))
         {
            occurances[s]++;
         }
         else
         {
            occurances[s] = 1;
         }
      }
   }


   // private void findOptimalPolymerFormula2()
   // {
   //    // Console.WriteLine(polymer);
   //    polymerMap = new Dictionary<string, int>();
   //    for(int s = 0; s < polymer.Length-1; s++)
   //    {
   //       string pair = polymer.Substring(s, 2);
   //       if(polymerMap.ContainsKey(pair))
   //       {
   //          polymerMap[pair]++;
   //       }
   //       else
   //       {
   //          polymerMap[pair] = 1;
   //       }
   //    }


   //    for (int i = 0; i < runs; i++)
   //    {

   //       Dictionary<string, int> evolvedMap = new Dictionary<string, int>();
   //       foreach(var key in polymerMap.Keys)
   //       {
   //          if(rules.ContainsKey(key))
   //          {
   //             string a = key[0] + rules[key]; 
   //             string b = rules[key] + key[1];

   //             addPolymerToMap(evolvedMap, a, polymerMap[key]);
   //             addPolymerToMap(evolvedMap, b, polymerMap[key]);
   //          }
   //          else
   //          {
   //             addPolymerToMap(evolvedMap, key, polymerMap[key]);
   //          }
   //       }
   //       polymerMap = evolvedMap;
   //    }

   // }

   private void addPolymerToMap(Dictionary<string, int> evolvedMap, string key, int total)
   {
      if(evolvedMap.ContainsKey(key))
      {
         evolvedMap[key] += total;
      }
      else
      {
         evolvedMap[key] = total;
      }
   }

   private void findOptimalPolymerFormula1()
   {
      // Console.WriteLine(polymer);


      int polyStart = polymer.Length-1;
      for (int i = 0; i < runs; i++)
      {
         polyStart = polyStart * 2;
      }
      polyStart = (polyStart + polyStart) + 1;
      result = polyStart;
   }

   private void setInsertionRules()
   {
      for (int i = 2; i < data.Count; i++)
      {
         string[] line = data[i].Split(" -> ");
         rules.Add(line[0], line[1]);
      }
   }

   private void setPolymerTemplate()
   {
      polymer = data[0];
   }


   public override void displayResults()
   {
      Console.WriteLine("Results: " + result);
   }

}

