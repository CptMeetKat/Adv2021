


public class Day10P1 : AocMachine 
{
   Dictionary<char, int> scores;

   public Day10P1(string filename) : base(filename)
   {
      scores = new Dictionary<char, int>();
      scores.Add(')', 3);
      scores.Add(']', 57);
      scores.Add('}', 1197);
      scores.Add('>', 25137);
   }

   public override void run()
   {
      readData(filename);
      calculateSolution();
      displayResults();
   }

   private int getErrorValue(char key)
   {
      return scores[key];
   }
   

   private void calculateSolution()
   {
      char[] openKeys = {'(', '[', '{', '<'};
      char[] closeKeys = {')', ']', '}', '>'};

      int errors = 0;
      Stack<char> stack = new Stack<char>();

      Dictionary<char, char> pairs = new Dictionary<char, char>();
      for(int i = 0; i < openKeys.Length; i++) { pairs.Add(closeKeys[i], openKeys[i]);  }

      foreach(string line in data)
      {
         int lineErrors = 0;
         for(int i = 0; i < line.Length; i++)
         {
            char c = line[i];
            if(openKeys.Contains(c))
            {
               stack.Push(c);
            }
            else if(closeKeys.Contains(c))
            {
               char pop = stack.Pop();
               if(pop != pairs[c])
               {
                  lineErrors += getErrorValue(c);
                  // stack.Push(pop);
               }
            }
            else{ Console.WriteLine("Error!"); }

         }
         errors += lineErrors; 
         stack.Clear();

         // // Print contents of stack
         // string contents = "";
         // while(stack.Count > 0 ){contents += stack.Pop();}
         // Console.WriteLine(contents);
      }

      result = errors;
   }
   
   public override void displayResults()
   {
      Console.WriteLine("Results: " + result);
   }
}