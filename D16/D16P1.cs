
public class D16P1 : AocMachine 
{
   string binary = "";
   Dictionary<char, string> hexTobin = new Dictionary<char, string>();
   
   public D16P1(string filename) : base(filename)
   {
      initHexToBinMap();
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
      decode(binary);
   
      displayResults();
   }


   private string decode(string bits)
   {
      int version = Convert.ToInt32(  left(bits, 3), 2  );
      result += version;
      bits = shift(bits, 3);
      int type = Convert.ToInt32(  left(bits, 3), 2  );
      bits = shift(bits, 3);

      if(type == 4)
      {
         bits = parseLiteral(bits);
      }
      else
      {

         int lengthTypeID = Convert.ToInt32(   left(bits, 1), 2   );
         bits = shift(bits, 1);
         if(lengthTypeID == 0)
            bits = parseOperandLenInBits(bits);
         else
            bits = parseOperandLenInPackets(bits);
      }

      return bits;
   }

   private string parseOperandLenInPackets(string bits)
   {
      int lenInPackets = Convert.ToInt32(   left(bits, 11), 2   );
      bits = shift(bits, 11);
      for(int i = 0; i < lenInPackets; i++)
      {
         bits = decode(bits);
      }
      return bits;
   }

   private string parseOperandLenInBits(string bits)
   {
      int lenInBits = Convert.ToInt32(   left(bits, 15), 2   );
      bits = shift(bits, 15);
      int packetLength = bits.Length;
      while(packetLength - bits.Length != lenInBits) //Wrong?
      {
         bits = decode(bits);
      }


      return bits;

   }

   private string parseLiteral(string bits)
   {
      // Console.WriteLine("LITERAL--");
      bool isDone = false;
      string literalValue = "";
      int bitCount = 0;
      while(!isDone)
      {
         string temp = left(bits, 5);
         bitCount += 5;
         // Console.WriteLine(temp);
         bits = shift(bits, 5);
         if(temp[0] == '0')
         {
            isDone = true;
         }
         temp = shift(temp, 1);
         literalValue += new String(temp);
      }

      //Calcualate remaining bits

      // int multiple = 0;
      // while((multiple+1)*4 < bitCount)
      //    multiple++;

      // int padBitsRemain = bitCount - (multiple*4);
      // bits = shift(bits, padBitsRemain); 
      // Console.WriteLine("AfterLit: " + bits);
      // Console.WriteLine("CHOP: " + padBitsRemain);

      //LiteralValue missing left pad but thats OK

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
