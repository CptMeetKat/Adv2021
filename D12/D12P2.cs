


public class D12P2 : AocMachine 
{
   Dictionary<string, List<string> > adjList;
   List<List<string>> completePaths;


   public D12P2(string filename) : base(filename)
   {
      adjList = new Dictionary<string, List<string>>();
      completePaths = new List<List<string>>();
   }

   public override void run()
   {
      readData(filename);
      mapPaths();

      calculatePaths();
      // printCompletePaths();
      // printPaths();
      displayResults();
   }

   private void mapPaths()
   {
      foreach(string line in data)
      {
         string[] elts = line.Split("-");
         string key = elts[0];
         string val = elts[1];
         addToPaths(key, val);
         addToPaths(val, key);
      }
   }

   private void printCompletePaths()
   {
      Console.WriteLine("---Printing Complete Paths---");
      foreach (var i in completePaths)
      {
         i.ForEach(x=>Console.Write(x + " "));   
         Console.WriteLine("");
      }

      // completePaths.ForEach(x=>Console.Write(x + " "));
      // Console.WriteLine("");
   }

   private void calculatePaths()
   {
      // var completePaths = new List< List<string> >();
      var candidates = new Stack< List<string> >();
      candidates.Push(   new List<string>()   );
      candidates.Peek().Add("start");

      while(candidates.Count > 0)
      {
         List<string> c = candidates.Pop();
         string endNode = c.Last();

         if(endNode == "end") //Finish path if last node is end
         {
            completePaths.Add(c);
            continue;
         }

         // c.ForEach(x=>Console.Write(x + " "));
         // Console.WriteLine("");

         List<string> adjacentNodes = adjList[endNode];
         foreach(var node in adjacentNodes)
         {
            if(node == "start") continue; //Dont add start node
            //if node is lower case,then check if visted
            if(isVisited(c, node)) continue;

            candidates.Push(   new List<string>(c)  );
            candidates.Peek().Add(node);
         }


         //get last string
         // find all connected nodes
         //for each connected node, create new list with each appended
         //each list psuh to candidates
      }
      result = completePaths.Count;
   }

   private bool isVisited(List<string> path, string node)
   {
      bool visited = false;

      if(isLowerCase(node))
      {

         if ( path.Exists(x => x == node) )
         {
            List<string> allLowerCaseNodes = path.FindAll(x => isLowerCase(x));
            if ( containsDuplicates(allLowerCaseNodes) )
            {
               // allLowerCaseNodes.ForEach(x => Console.Write(x + " " ));
               // Console.WriteLine("");
               visited = true;
            }
         }

      }
      
      return visited;
   }

   private bool isLowerCase(string s)
   {
      bool result = true;
      foreach(char c in s)
      {
         if(! Char.IsLower(c) )
         {
            result = false;
            break;
         }
      }

      return result;
   }


   private bool containsDuplicates(List<string> items)
   {
      HashSet<string> set = new HashSet<string>();
      bool hasDupes = false;
      foreach(var s in items)
      {
         hasDupes = !set.Add(s);
         if(hasDupes){break;}
      }

      return hasDupes;
   }


   private void addToPaths(string key, string val)
   {
      if(! adjList.ContainsKey(key))
      {
         adjList[key] = new List<string>();
      }            
      adjList[key].Add(val);
   }

   private void printPaths()
   {
      Console.WriteLine("---Printing Paths---");
      foreach(string key in adjList.Keys)
      {
         Console.WriteLine("{0}: {1}", key, String.Join(" ", adjList[key].ToArray()));
      }
   }
   
   public override void displayResults()
   {
      Console.WriteLine("Results: " + result);
   }
}