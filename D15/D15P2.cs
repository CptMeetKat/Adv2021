
public class D15P2 : AocMachine 
{
   int height;
   int width;
   
   public D15P2(string filename) : base(filename)
   {
   }   
   
   private void expandGraphHorizontally()
   {
      for(int i = 0; i < data.Count; i++)
      {
         string newString = new String(data[i]);
         for(int j = 0; j < 4; j++)
         {
            newString = incString(newString);
            data[i] += newString;
         }
      }
   }


   private void expandGraphVertically()
   {
      int initialHeight = data.Count;

      List<string> additions = new List<string>();
      
      for (int j = 0; j < 4; j++)
      {
         for(int i = data.Count - initialHeight; i < data.Count; i++)
         {
            additions.Add(incString(data[i]));
         }
         additions.ForEach(x => data.Add(x));
         additions.Clear();

      }

   }

   private string incString(string curr)
   {
      string newString = "";
      for(int i = 0; i < curr.Length; i++)
      {
         char c = curr[i];
         int temp = (Int32.Parse(c.ToString())+1) % 10;
         if(temp ==0)temp++;
         newString += temp;
      }

      return newString;
   }

   private List<List<Node>> initPrevious()
   {
      List<List<Node>> prev = new List<List<Node>>();
      for(int i = 0; i < height; i++)
      {
         prev.Add(new List<Node>());
         for (int j = 0; j < width; j++)
            prev[i].Add(new Node(-1,-1));
      }
      return prev;
   }

   private List<List<int>> initDistance()
   {
      List<List<int>> dist = new List<List<int>>();
      for(int i = 0; i < height; i++)
      {
         dist.Add(new List<int>());
         for (int j = 0; j < width; j++)
            dist[i].Add(Int32.MaxValue);
      }
      
      return dist;
   }


   
   private void init()
   {
      expandGraphHorizontally();
      expandGraphVertically();
      height = data.Count;
      width = data[0].Length;



      // printData();



      // height = data[0].Length;
      // width = data.Count;
   }

   private void printData()
   {
      foreach(var s in data)
      {
         Console.WriteLine(s);
      }

   }

   public override void run()
   {
      readData(filename);
      init();
      findShortestPath(0,0);
   
      displayResults();
   }

   private List<Node> initQueue()
   {
      List<Node> queue = new List<Node>();
      for(int y = 0; y < height; y++)
         for(int x = 0; x < width; x++)
         {
            queue.Add(new Node(x,y,Int32.MaxValue));
         }
      return queue;
   }

   private Node popSmallest(List<Node> list)
   {
      int pos = 0;
      int small = list[0].distance;
      for(int i = 0; i < list.Count; i++)
      {
         if(list[i].distance < small)
         {
            pos = i;
            small = list[i].distance;
         }
      }

      Node result = list[pos];
      list.RemoveAt(pos);

      
      return result;
   }

   private void findShortestPath(int startX = 0, int startY = 0)
   {
      List<List<int>> dist = initDistance();
      List<List<Node>> prev = initPrevious();

      // List<Node> queue = initQueue();
      List<Node> queue = new List<Node>();
      queue.Add(new Node(0,0,0));
      dist[startY][startX] = 0;

      while(queue.Count > 0)
      {
         Node u = popSmallest(queue);

         List<Node> neighbours = getNeighbours(u);


         foreach(Node v in neighbours)
         {

            int alt = dist[u.y][u.x] + v.distance;
         
            if(alt < dist[v.y][v.x])
            {
               dist[v.y][v.x] = alt;
               prev[v.y][v.x] = new Node(u.x, u.y, -1); // This path shit could be wrong
               
               neighbours.ForEach(x => queue.Add(new Node(v.x, v.y, alt))); //originally missing this line
            }
         }
      }

      result = dist[height-1][width-1];
      // printPath(prev);      
      // printDistance(dist);      
   }





   private void printDistance(List<List<int>> list)
   {
      Console.WriteLine("w: {0}, h: {1}", width, height);
      for(int x = 0; x < list.Count; x++) 
      {
         for (int y = 0; y < list[0].Count; y++)
         {
            Console.Write("{0} ", list[x][y]);
         }
         Console.WriteLine();
      }
   }

   private void printPath(List<List<Node>> list)
   {
      for(int x = 0; x < list.Count; x++) 
      {
         for (int y = 0; y < list[0].Count; y++)
         {
            var j = list[x][y];
            Console.Write("({0} {1}) ", j.x, j.y, x, y);
         }
         Console.WriteLine();
      }
   }

   private List<Node> getNeighbours(Node u)
   {
      List<Node> adjacent = new List<Node>();
      if(    inRange(u.x-1, u.y)       )
      {
         adjacent.Add(new Node(u.x-1, u.y, Int32.Parse(    data[u.y][u.x-1].ToString()    )));
      }
      if(    inRange(u.x, u.y+1)       )
      {
         adjacent.Add(new Node(u.x, u.y+1, Int32.Parse(    data[u.y+1][u.x].ToString()    )));
      }
      if(    inRange(u.x, u.y-1)       )
      {
         adjacent.Add(new Node(u.x, u.y-1, Int32.Parse(    data[u.y-1][u.x].ToString()    )));
      }
      if(    inRange(u.x+1, u.y)       )
      {
         adjacent.Add(new Node(u.x+1, u.y, Int32.Parse(    data[u.y][u.x+1].ToString()    )));
      }


      return adjacent;
   }

   private bool inRange(int x, int y)
   {
      return x >= 0 && x < width && y < height && y >=0;
   }

   public override void displayResults()
   {
      Console.WriteLine("Results: " + result);
   }
}
