
public class D13P1 : AocMachine 
{
   HashSet<Coordinate> datapoints;
   List<string> instructions;
   int totFolds;

   public D13P1(string filename, int totFolds) : base(filename)
   {
      datapoints = new HashSet<Coordinate>();
      
      instructions = new List<string>();
      this.totFolds = totFolds;
   }

   public override void run()
   {
      readData(filename);
      populateDataPoints();
      populateFolds();
      fold(totFolds);

      displayResults();
      printResult();
   }

   private void fold(int maxFolds)
   {
      for(int i = 0; i < maxFolds && i < instructions.Count; i++)
      {
         string[] line = instructions[i].Split("=");
         string type = line[0];
         int position = Int32.Parse(line[1]);

         if(type == "x")
            xFold(position);
         else
            yFold(position);
      }

      result = datapoints.Count;
   }

   private void xFold(int position)
   {
      //find all coords in fold range
      //remove each
      //apply fold function
      //add to set
      int x1 = position;
      int y1 = 0;
      int x2 = Int32.MaxValue;
      int y2 = Int32.MaxValue;

      List<Coordinate> newDataPoints = new List<Coordinate>();

      // printSet(datapoints);
      foreach(var c in datapoints)
      {
         Coordinate temp = c;
         if( c.inRange(x1,y1,x2,y2)  )
         {
            datapoints.Remove(c);

            temp.x = (position)  + position - temp.x;
            newDataPoints.Add(temp);
         }
      }

      newDataPoints.ForEach(x => datapoints.Add(x));
      // printSet(datapoints);
   }

   private void printSet(HashSet<Coordinate> set)
   {
      foreach(var c in set)
      {
         Console.WriteLine(c.x + " " + c.y);
      }
   }

   private void yFold(int position)
   {
      int x1 = 0;
      int y1 = position;
      int x2 = Int32.MaxValue;
      int y2 = Int32.MaxValue;

      List<Coordinate> newDataPoints = new List<Coordinate>();

      foreach(var c in datapoints)
      {
         Coordinate temp = c;
         if( c.inRange(x1,y1,x2,y2)  )
         {
            datapoints.Remove(c);

            temp.y = (position) + position - temp.y;
            newDataPoints.Add(temp);
         }
      }

      newDataPoints.ForEach(x => datapoints.Add(x));

      // printSet(datapoints);

   }

   private void populateFolds()
   {
      int i = 0;
      while(i < data.Count && data[i] != "")
         i++;
      i++; //scroll to the section of the data


      for(; i < data.Count; i++)
      {
         string[] line = data[i].Split(" ");
         instructions.Add(line[2]);
      }
      // folds.ForEach(x => Console.WriteLine(x));
   }

   private void populateDataPoints()
   {
      for(int i = 0; i < data.Count && data[i] != ""; i++)
      {
         string[] line = data[i].Split(",");

         int x = Int32.Parse(line[0]);
         int y = Int32.Parse(line[1]);

         datapoints.Add(new Coordinate(x, y));
      }
   }
  
   public override void displayResults()
   {
      Console.WriteLine("Results: " + result);
   }

   private void printResult()
   {

      int xMax = 0;
      int yMax = 0;
      foreach(Coordinate c in datapoints)
      {
         xMax = Math.Max(xMax, c.x);
         yMax = Math.Max(yMax, c.y);
      }
      xMax++;
      yMax++;

      Coordinate curr = new Coordinate(0,0);
      for(int y = 0; y < yMax; y++)
      {
         curr.y = y;
         for(int x = 0; x < xMax; x++)
         {
            curr.x = x;
            if(datapoints.Contains(curr))
            {
               Console.Write("#");
            }
            else
            {
               Console.Write("-");
            }
         }
         Console.WriteLine();
      }
   }
}


public class Coordinate
{
   public int x { get; set;}
   public int y {get; set;}

   public Coordinate(int x, int y)
   {
      this.x = x;
      this.y = y;
   }
   public Coordinate()
   {
      x = 0;
      y = 0;
   }

   public bool inRange(int x1, int y1, int x2, int y2)
   {
      if(x1 > x2)
         StandardFunctions.swap(ref x1, ref x2);
      if(y1 > y2)
         StandardFunctions.swap(ref y1, ref y2);


      bool result = false;
      if(this.x >= x1 && this.x <= x2)
         if(this.y >= y1 && this.y <= y2)
            result = true;

      return result;
   }

    public override bool Equals(Object? a)
    {
      if(a == null) return false;

      return (this.x == ((Coordinate)a).x && this.y == ((Coordinate)a).y);
    }

    public override int GetHashCode()
    {
        int hCode = this.x * this.y;
        return hCode.GetHashCode();
    }
}
