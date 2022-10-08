


public class Day10P2 : AocMachine 
{
   Dictionary<char, ulong> scoresMap;
   char[] openKeys = {'(', '[', '{', '<'};
   char[] closeKeys = {')', ']', '}', '>'};
   Dictionary<char, char> pairs;
   List<ulong> scores;
   new ulong result;


   public Day10P2(string filename) : base(filename)
   {
      scoresMap = new Dictionary<char, ulong>();
      scoresMap.Add('(', 1u);
      scoresMap.Add('[', 2u);
      scoresMap.Add('{', 3u);
      scoresMap.Add('<', 4u);

      pairs = new Dictionary<char, char>();
      for(int i = 0; i < openKeys.Length; i++) { pairs.Add(closeKeys[i], openKeys[i]);  }

      scores = new List<ulong>();
   }

   public override void run()
   {
      readData(filename);
      removeCorrupt();
      processIncompleteLines();
      findResult();
      displayResults();
   }


   private void removeCorrupt()
   {
      char[] openKeys = {'(', '[', '{', '<'};
      char[] closeKeys = {')', ']', '}', '>'};

      Stack<char> stack = new Stack<char>();

      Dictionary<char, char> pairs = new Dictionary<char, char>();
      for(int i = 0; i < openKeys.Length; i++) { pairs.Add(closeKeys[i], openKeys[i]);  }

      for(int j = 0; j < data.Count; j++)
      {
         string line = data[j];

         for(int i = 0; i < line.Length; i++)
         {
            char c = line[i];
            if(openKeys.Contains(c))
               stack.Push(c);
            else
            {
               char pop = stack.Pop();
               if(pop != pairs[c])
               {
                  data.RemoveAt(j--); //Remove step back
                  break;
               }
            }
         }
         stack.Clear();
      }
   }

   private void processIncompleteLines()
   {
      Stack<char> stack = new Stack<char>();


      foreach(string line in data)
      {
         ulong lineScore = 0;
         foreach(char c in line)
         {
            if(openKeys.Contains(c))
               stack.Push(c);
            else
               stack.Pop();
         }

         while(stack.Count > 0)
         {
            
            lineScore *= 5;
            lineScore += scoresMap[  stack.Pop() ];
         }
         scores.Add(lineScore);
         stack.Clear();
      }
   }

   private void findResult()
   {
      scores.Sort();
      int winnerPos = scores.Count/2;
      result = scores[winnerPos];
      // Console.WriteLine(    String.Join(" ", scores.ToArray()      ) );

   }


   
   public override void displayResults()
   {
      Console.WriteLine("Results: " + result);
   }
}