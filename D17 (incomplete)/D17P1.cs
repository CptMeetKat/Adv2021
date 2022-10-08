namespace Attempt1;


public class D17P1 : AocMachine 
{
   int minX;
   int maxX;
   int minY;
   int maxY;
   Projectile ball = new Projectile();
   
   public D17P1(string filename) : base(filename){  }   

   
   public override void run()
   {
      readData(filename);
      initBounds(data[0]);
      simulate();
      displayResults();
   }
   
   private void initBounds(string line)
   {
      List<int> values = new List<int>();

      string temp = "";
      for (int i = 0; i < line.Length; i++)
      {
         bool foundNumber = false;
         while(i < line.Length && (Char.IsNumber(line[i]) || line[i] == '-'))
         {
            foundNumber = true;
            temp += line[i].ToString();
            i++;
         }
         if(foundNumber)
         {
            values.Add(Int32.Parse(temp));
            temp = "";
         }
      }

      minX = Math.Min(values[0], values[1]);
      maxX = Math.Max(values[0], values[1]);

      minY = Math.Min(values[2], values[3]);
      maxY = Math.Max(values[2], values[3]);
   }

   private void printAttributes()
   {
      Console.WriteLine("M: {0} {1} {2} {3}", minX, maxX, minY, maxY);
   }

   private bool inBounds(Projectile p)
   {
      return p.x >= minX && p.x <= maxX && p.y >= minY && p.y <= maxY;
   }

   private bool pastBounds(Projectile p, int maxX, int minY) //verify
   {
      return p.x > maxX || p.y < minY;
   }

   private int shoot(Projectile ball)
   {
      int high = Int32.MinValue;
      int localMaxY = Int32.MinValue;
      bool hasEnteredBounds = false;
      int xBound = maxX + 1;
      int yBound = minY -1;

      while(!pastBounds(ball, xBound, yBound)) 
      {
         ball.step();

         localMaxY = Math.Max(localMaxY, ball.y);
         
         
         if(inBounds(ball) || hasEnteredBounds)
         {
            high = Math.Max(high, localMaxY);
            if(localMaxY < high){break;}

            hasEnteredBounds = true;
            xBound = Int32.MaxValue;
         }
      }
      return high;
   }



   private void simulate()
   {
      printAttributes();
      int high = Int32.MinValue;
      int bestXVelocity = 0;
      int bestYVelocity = 0;

      int xEnd = maxX+1;
      int yEnd = Int32.MaxValue;
      // int yEnd = 10000000;

      // for(int x = 0; x < xEnd; x++)
      for(int x = xEnd; x >= 0; x--)
      {
         for (int y = yEnd; y >= 0; y--)
         // for (int y = 0; y < yEnd; y++)
         {

            ball.setVelocity(x, y);
            ball.resetPosition();

            int prevHigh = high;
            high = Math.Max(    shoot(ball), high    ) ;
            if(prevHigh != high)
            {
               bestXVelocity = x;
               bestYVelocity = y;
               break;
            }

         }
         
      }
      Console.WriteLine("BestV: {0} {1}", bestXVelocity, bestYVelocity);
      result = high;

   }

   public override void displayResults()
   {
      Console.WriteLine("results: {0}", result);
   }
}


public class Projectile
{
   public int x = 0;
   public int y = 0;

   public int xVelocity;
   public int yVelocity;

   public void step()
   {
      x += xVelocity;
      y += yVelocity;

      if(xVelocity > 0)
         xVelocity -= 1;
      else
         xVelocity +=1;


      yVelocity -= 1;
   }

   public void resetPosition()
   {
      x = 0;
      y = 0;
   }

   public void setVelocity(int _xVelocity, int _yVelocity)
   {
      xVelocity = _xVelocity;
      yVelocity = _yVelocity;
   }

   public void printAttributes()
   {
      Console.WriteLine("{0} {1}   :    {2} {3}", x, y, xVelocity, yVelocity);
   }
}
