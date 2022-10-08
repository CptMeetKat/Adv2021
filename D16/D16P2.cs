
public class D16P2 : AocMachine 
{
   string binary = "";
   Dictionary<char, string> hexTobin = new Dictionary<char, string>();
   List<string> instructions = new List<string>();
   Dictionary<int, string> operationsMap;
   Node root = new Node();
   new UInt64 result;
   
   public D16P2(string filename) : base(filename)
   {
      initHexToBinMap();
      operationsMap = new Dictionary<int, string>() {
         {0, "+"},  {1, "*"}, {2, "MIN"},
         {3, "MAX"},{5, ">"}, {6, "<"},
         {7, "="}
      };
   }   

   private void convertToBin()
   {
      foreach(string s in data)
         foreach(char c in s)
         {
            binary += hexTobin[c];
         }
   }

   private void initHexToBinMap()
   {
      hexTobin.Add('0', "0000");
      hexTobin.Add('1', "0001");
      hexTobin.Add('2', "0010");
      hexTobin.Add('3', "0011");
      hexTobin.Add('4', "0100");
      hexTobin.Add('5', "0101");
      hexTobin.Add('6', "0110");
      hexTobin.Add('7', "0111");
      hexTobin.Add('8', "1000");
      hexTobin.Add('9', "1001");
      hexTobin.Add('A', "1010");
      hexTobin.Add('B', "1011");
      hexTobin.Add('C', "1100");
      hexTobin.Add('D', "1101");
      hexTobin.Add('E', "1110");
      hexTobin.Add('F', "1111");
   }

   public override void run()
   {
      readData(filename);
      convertToBin();
      decode(binary, root);
      runCalculator();
      displayResults();
   }

   private void runCalculator()
   {
      // printExplore(root);
      result = calcExplore(root);
      // Console.WriteLine("ANSWER: " + calcExplore(root));
   }

   private UInt64 calcExplore(Node curr)
   {
      UInt64 currVal;
      if(UInt64.TryParse(curr.value, out currVal)) // Number
      {
         return currVal;
      }
      else //Operand
      {
         List<UInt64> values = new List<UInt64>();
         foreach(var i in curr.children)
         {
            values.Add(calcExplore(i));
         }
         return calculate(curr.value, values);
      }
   }

   private void printExplore(Node curr)
   {
      Console.WriteLine(curr.value);
      foreach(var x in curr.children)
      {
         printExplore(x);
      }
   }

   private UInt64 calculate(string operation, List<UInt64> values)
   {
      UInt64 tot = 0;
      if(operation == "+")
         tot = Add(values);
      else if(operation == "*")
         tot = Multiply(values);
      else if(operation == "MIN")
         tot = Min(values);
      else if(operation == "MAX")
         tot = Max(values);
      else if(operation == ">")
         tot = greaterThan(values);
      else if(operation == "<")
         tot = lesserThan(values);
      else if(operation == "=")
         tot = equality(values);

      return tot;
   }

   private UInt64 Add(List<UInt64> values)
   {
      UInt64 total = 0;
      foreach (var v in values)
      {
         total += (UInt64)v;
      }
      return total;
   }

   private UInt64 Multiply(List<UInt64> values)
   {
      UInt64 total = 1;
      foreach(var v in values)
      {
         total *= (UInt64)v;
      }
      return total;
   }

   private UInt64 Min(List<UInt64> values)
   {
      UInt64 small = UInt64.MaxValue;
      foreach(var v in values)
      {
         small = Math.Min(small, v);
      }
      return small;
   }

   private UInt64 Max(List<UInt64> values)
   {
      UInt64 big = UInt64.MinValue;
      foreach(var v in values)
      {
         big = Math.Max(big, v);
      }
      return big;
   }

   private UInt64 greaterThan(List<UInt64> values)
   {
      if(values[0] > values[1])
         return 1;
      return 0;

   }

   private UInt64 lesserThan(List<UInt64> values)
   {
      if(values[0] < values[1])
         return 1;
      return 0;
   }

   private UInt64 equality(List<UInt64> values)
   {
      if(values[0] == values[1])
         return 1;
      return 0;
   }



   private void storeType(int type, Node curr)
   {
      if(type == 4){return;}
      instructions.Add(operationsMap[type]);
      curr.value = operationsMap[type];
   }

   private string decode(string bits, Node curr)
   {
      int version = Convert.ToInt32(  left(bits, 3), 2  );
      bits = shift(bits, 3);
      int type = Convert.ToInt32(  left(bits, 3), 2  );
      bits = shift(bits, 3);


      storeType(type, curr);
      if(type == 4)
      {
         bits = parseLiteral(bits, curr);
      }
      else
      {
         int lengthTypeID = Convert.ToInt32(   left(bits, 1), 2   );
         bits = shift(bits, 1);
         if(lengthTypeID == 0)
            bits = parseOperandLenInBits(bits, curr);
         else
            bits = parseOperandLenInPackets(bits, curr);
      }

      return bits;
   }

   private string parseOperandLenInPackets(string bits, Node curr)
   {
      int lenInPackets = Convert.ToInt32(   left(bits, 11), 2   );
      bits = shift(bits, 11);
      for(int i = 0; i < lenInPackets; i++)
      {
         curr.children.Add(new Node());
         bits = decode(bits, curr.children[i]);
      }
      return bits;
   }

   private string parseOperandLenInBits(string bits, Node curr)
   {
      int lenInBits = Convert.ToInt32(   left(bits, 15), 2   );
      bits = shift(bits, 15);
      int packetLength = bits.Length;
      while(packetLength - bits.Length != lenInBits) //Wrong?
      {
         Node c = new Node();
         curr.children.Add(c);
         bits = decode(bits, c);
      }


      return bits;

   }

   private string parseLiteral(string bits, Node curr)
   {
      bool isDone = false;
      string literalValue = "";
      int bitCount = 0;
      while(!isDone)
      {
         string temp = left(bits, 5);
         bitCount += 5;
         bits = shift(bits, 5);
         if(temp[0] == '0')
         {
            isDone = true;
         }
         temp = shift(temp, 1);
         literalValue += new String(temp);
      }
      instructions.Add(Convert.ToUInt64(  literalValue, 2   ).ToString());
      curr.value = Convert.ToUInt64(  literalValue, 2   ).ToString();

      return bits;
   }

   private string left(string s, int len)
   {
      return s.Substring(0, len);
   }

   private string shift(string s, int start)
   {

      return s.Substring(start);
   }

   public override void displayResults()
   {
      Console.WriteLine("({0}) results: {1}", filename, result);
   }
}



public class Node
{
   public List<Node> children = new List<Node>();
   public string value = "";

   public Node(string _value = "")
   {
      value = _value;
   }
}