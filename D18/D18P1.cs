
public class D18P1 : AocMachine 
{
 
   List<char> snail = new List<char>();
   public D18P1(string filename) : base(filename){  }   

   
   public override void run()
   {
      readData(filename);
      addSnailNumbers();
      calculateResult();
      displayResults();
   }

   private void calculateResult()
   {
      Stack<char> stack = new Stack<char>();
      foreach(char c in snail)
      {
         if(c != ']')
            stack.Push(c);
         else
            consolidate(stack);            
      }

   }
   private void consolidate(Stack<char> stack)
   {
      char c = stack.Pop();
      string temp = "";
      while(c != '[')
      {
         temp = c + temp;
         c = stack.Pop();
      }
      string[] split = temp.Split(",");

      int num1 = Int32.Parse(split[0]);
      int num2 = Int32.Parse(split[1]);

      string result = ((3*num1)+(2*num2)).ToString();
      foreach(char r in result)
      {
         stack.Push(r);
      }
      

   this.result = Int32.Parse(result);




   }


   private void addSnailNumbers()
   {
      foreach(var d in data)
      {
         add(d);
         reduce();
      }
      printSnail();
   }

   private void reduce()
   {
      bool hasChanged = true;
      while(hasChanged)
      {
         hasChanged = explode();

         if (hasChanged) continue;
         hasChanged = split();
      }
   }


   private int scrollToLeftSquare(int start = 0, int elasped = 5)
   {  
      int totalBrackets = 0;
      int leftSquarePos = -1;

      for (int i = 0; i < snail.Count && totalBrackets < elasped; i++)
      {
         if(snail[i] == '[') 
            totalBrackets++;
         else if(snail[i] == ']')
            totalBrackets--;
   
         if(totalBrackets == elasped)
         {
            leftSquarePos = i;
            break;
         }
      }

      return leftSquarePos;
   }


   private int scrollToRightSquare(int start)
   {
      int end = snail.Count;
      for(int i = start; i < snail.Count; i++ )
      {
         if(snail[i] == ']')
         {
            end = i;
            break;
         }
      }

      return end;

   }

   private bool explode()
   {
      bool hasChanged = false;   

      int start = scrollToLeftSquare();
      if(start == -1) return false;
      int end  = scrollToRightSquare(start);
      hasChanged = true;

      string[] innerPair = extract(start, end).Split(",");

      innerPair[0] = innerPair[0].Substring(1);
      innerPair[1] = innerPair[1].Substring(0, innerPair[1].Length-1);

      removeSection(start, end-start+1);
      snail.Insert(start,'0');
      addRight(start+1, Int32.Parse(innerPair[1]));
      addLeft(start-1, Int32.Parse(innerPair[0]));

      //Find left bracket that is depth 4
      //Find right bracket
      // getSize
      // copy
      // split
      // addLeft
      // addRight
      // Replace size with 0

      return hasChanged;
 
   }

   private void removeSection(int start, int length)
   {
      for(int i = 0; i < length; i++)
      {
         snail.RemoveAt(start);
      }
   }

   private void printSnail()
   {
      snail.ForEach(x => Console.Write(x));
      Console.WriteLine();
   }

   private void addLeft(int pos, int value)
   {
      //Find first whole number too left
      //remove it
      //add new number

      bool numberFound = false;
      string tempNumber = "";
      int i;
      for(i = pos; i >= 0; i--)
      {
         if(   Char.IsDigit(snail[i])   )
         {
            numberFound = true;
            tempNumber = snail[i].ToString() + tempNumber;
         }
         if(numberFound && !Char.IsDigit(snail[i]))
            break;
      }


      
      for (int j = 0; j < tempNumber.Length; j++)
         snail.RemoveAt(i+1);

      if(tempNumber != "")
      {
         string newNumber = (Int32.Parse(tempNumber) + value).ToString();
         insert(i+1, newNumber);
      }
   }

   private void addRight(int pos, int value)
   {
      bool numberFound = false;
      string tempNumber = "";
      int start = -1;
      for(int i = pos; i < snail.Count; i++)
      {
         if(Char.IsDigit(snail[i]))
         {
            numberFound = true;
            tempNumber += snail[i].ToString();
            
            snail.RemoveAt(i);
            if (start == -1)
            {
               start = i;
            }
            i--;
            continue;
            
         }

         if(numberFound && !Char.IsDigit(snail[i]) )
            break;
      }


      if(tempNumber != "")
      {
         string newNumber = (Int32.Parse(tempNumber) + value).ToString();
         insert(start, newNumber);
      }
   }

   private void insert(int pos, string value)
   {
      foreach(char c in Enumerable.Reverse(value) )
      {
         snail.Insert(pos, c);
      }
   }


   private string extract(int start, int end)
   {
      string result = "";
      for (int i = start; i <= end ; i++)
      {
         result += snail[i];
      }
      return result;
   }

   private bool split()
   {
      bool hasChanged = false;
      string value = "";
      bool foundNumber = false;
      int start = -1;
      int length = -1;

      for(int i = 0; i < snail.Count; i++)
      {
         if(Char.IsDigit(snail[i]))
         {
            value += snail[i];
            foundNumber = true;
            if(start == -1)
               start = i;
         }
         
         
         if(foundNumber && !Char.IsDigit(snail[i]))
         {
            length = i - start;

            int parsedValue = Int32.Parse(value);
            if(parsedValue > 9)
            {
               splitValue(start, length, parsedValue);
               hasChanged = true;
               break;
            }
            value = "";
            foundNumber = false;
            start = -1;
         }
      }

      return hasChanged;
   }

   private void splitValue(int start, int length, int value)
   {
      removeSection(start, length);
      int left = value/2;

      int plus = 0;
      if(value % 2 == 1)
         plus = 1;
      int right = (value/2) + plus;

      insert(start, "["+left+","+right+"]");
   }

   private void add(string s)
   {
      bool firstItem = false;
      if(snail.Count == 0)
         firstItem = true;
      
      foreach (char c in s)
         snail.Add(c);

      if(!firstItem)
      {
         snail.Insert(snail.Count - s.Length, ',');
         snail.Insert(0, '[');
         snail.Add(']');
      }  
   }
   

   public override void displayResults()
   {
      Console.WriteLine("results: {0}", result);
   }
}

