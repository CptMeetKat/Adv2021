using System;


internal class Day8
{
   List<string> data;
   string filename;
   int result1 = -1;
   int result2 = 0;
   


   static void Main(string[] args)
   {
      string filename = "data";
      // string filename = "data3";
      Day8 d = new Day8(filename);
      d.run();
   }



   public Day8(string filename)
   {
      data = new List<string>();
      this.filename = filename;

   }

   private void run()
   {
      readData();
      calculateP1();
      calculateP2();
      printResults();

   }

   private void printResults()
   {
      //Console.WriteLine("{0} {1} was born on: {2}", first, last, birthday);
      Console.WriteLine("Result 1: " + result1);
      Console.WriteLine("Result 2: " + result2);
   }


   private void calculateP1()
   {
      int total = 0;
      HashSet<int> uniqueNums = new HashSet<int>() {2, 4, 3, 7};

      foreach(string d in data)
      {
         string[] oSignals = extractOutputSignals(d);

         foreach(string s in oSignals)
         {
            if(uniqueNums.Contains<int>(s.Length))
            {
               total = total + 1;
            }
         }
      }

      result1 = total;
   }

   private string[] extractOutputSignals(string line)
   {
      string oSignals = line.Split("|")[1].Trim();
      string[] result = oSignals.Split(" ");
      return result;
   }

   private string[] extractInputSignals(string line)
   {
      string oSignals = line.Split("|")[0].Trim();
      string[] result = oSignals.Split(" ");
      return result;
   }
   

   private void readData()
   {
      string[] fileData = File.ReadAllLines(filename);
      foreach (string s in fileData)
      {
         data.Add(s);
      }
   }

   private void printData()
   {
      foreach(string s in data)
      {
         Console.WriteLine(s);
      }
   }


   private void addToSet(HashSet<char> map, string s)
   {
      foreach(char c in s)
      {
         map.Add(c);
      }

   }

   private void clearSet(HashSet<char>[] maps)
   {
      foreach(var e in maps)
      {
         e.Clear();
      }
   }

   private HashSet<char>[] generateMap(string line)
   {
      HashSet<char>[] maps = new HashSet<char>[10];
      for(int i = 0; i < 10; i++)
      {
         maps[i] = new HashSet<char>();
      }


      string[] iSignals = extractInputSignals(line);
      Find1478(maps, iSignals);
      Find6(maps, iSignals);
      Find0(maps, iSignals);
      Find9(maps, iSignals);
      Find3(maps, iSignals);
      Find2(maps, iSignals);
      Find5(maps, iSignals);
      

      string[] oSignals = extractOutputSignals(line);
      mapOutputs(maps, oSignals);


      // printMapping(maps);
      clearSet(maps);

      return maps;
   }

   private void mapOutputs(HashSet<char>[] maps, string[] oSignals)
   {

      string result = "";
      HashSet<char> outValue = new HashSet<char>();

      foreach(string s in oSignals)
      {
         addToSet(outValue, s);
         int count = 0;
         foreach(var m in maps)
         {
            if (   outValue.SetEquals(m) )
            {
               result += count;
               break;
            }
            count++;
         }
         outValue.Clear();
      }
      // Console.WriteLine(result);
      result2 += int.Parse(result);
      
      // Console.WriteLine("-----------------");

   }

   private void Find1478(HashSet<char>[] maps, string[] iSignals)
   {
      foreach(string s in iSignals) //Determine 1 4 7 8
      {
         if(s.Length == 2)
         {
            addToSet(maps[1], s);
         }
         else if(s.Length == 4)
         {
            addToSet(maps[4], s);
         }
         else if(s.Length == 3)
         {
            addToSet(maps[7], s);
         }
         else if(s.Length == 7)
         {
            addToSet(maps[8], s);
         }
      }



   }


   private void Find6(HashSet<char>[] maps, string[] iSignals)
   {
      HashSet<char> temp = new HashSet<char>();
      HashSet<char> target = new HashSet<char>();


      foreach(string s in iSignals)
      {
         addToSet(target, s);

         temp.UnionWith(maps[1]);
      
         if(target.Count() == 6)
         {
            target.UnionWith(temp);
            if(target.Count() == 7)
            {
               addToSet(maps[6], s);
               break;
            }
         }
         target.Clear();
      }
 
   }

   private void Find0(HashSet<char>[] maps, string[] iSignals)
   {
      HashSet<char> temp = new HashSet<char>();
      HashSet<char> target = new HashSet<char>();


      foreach(string s in iSignals)
      {

         addToSet(target, s);
         temp.UnionWith(maps[4]);
      
         if(target.Count() == 6)
         {
            if( !  maps[4].IsSubsetOf(target))
            {
               
               if( ! maps[6].IsSubsetOf(target)   )
               {
                  addToSet(maps[0], s);
                  break;

               }

               
            }



            // target.UnionWith(temp);
            // if(target.Count() == 7)
            // {
            //    target.Clear();
            //    addToSet(target, s);
            //    target.IntersectWith(maps[6]);
            //    if(target.Count == 6){continue;}


            //    addToSet(maps[0], s);
            //    break;
            // }
         }
         target.Clear();
      }
 
   }


   private void Find9(HashSet<char>[] maps, string[] iSignals)
   {
      HashSet<char> target = new HashSet<char>();

      foreach(string s in iSignals)
      {

         addToSet(target, s);
      
         if(target.Count() == 6)
         {
            if(   !target.IsSubsetOf(maps[0]) && !target.IsSubsetOf(maps[6])    )
            {
               addToSet(maps[9], s);
               break;
            }
         }
         target.Clear();
      }
 
   }


   private void Find3(HashSet<char>[] maps, string[] iSignals)
   {
      HashSet<char> target = new HashSet<char>();

      foreach(string s in iSignals)
      {

         addToSet(target, s);
      
         if(target.Count() == 5)
         {
            if(   maps[1].IsSubsetOf(target)   )
            {
               addToSet(maps[3], s);
               break;
            }
         }
         target.Clear();
      }
 
   }

   private void Find2(HashSet<char>[] maps, string[] iSignals)
   {
      HashSet<char> target = new HashSet<char>();

      foreach(string s in iSignals)
      {

         addToSet(target, s);
      
         if(target.Count() == 5)
         {
            if(   !maps[1].IsSubsetOf(target)   ) //not 3
            {
               target.UnionWith(maps[6]);
               if(target.Count() == 7)
               {
                  addToSet(maps[2], s);
                  break;
               }
            }
         }
         target.Clear();
      }
   }

   private void Find5(HashSet<char>[] maps, string[] iSignals)
   {
      HashSet<char> target = new HashSet<char>();

      foreach(string s in iSignals)
      {

         addToSet(target, s);
      
         if(target.Count() == 5)
         {
            if(   !maps[1].IsSubsetOf(target)   ) //not 3
            {
               target.UnionWith(maps[6]);
               if(target.Count() == 6)
               {
                  addToSet(maps[5], s);
                  break;
               }
            }
         }
         target.Clear();
      }
   }



   private void printMapping(HashSet<char>[] mapping)
   {
      foreach(var e in mapping)
      {
          Console.WriteLine(String.Join(", ", e));
      }
      Console.WriteLine("-------------------");
   }

   private void calculateP2()
   {
      foreach(string d in data)
      {
         HashSet<char>[] mapping = generateMap(d);
      }
   }
  

}