
public class DJTest : AocMachine 
{
   int height;
   int width;
   
   public DJTest(string filename) : base(filename)
   {
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
      height = data.Count;
      width = data[0].Length;

      // height = data[0].Length;
      // width = data.Count;
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

   private List<List<bool>> initVisited()
   {
      List<List<bool>> dist = new List<List<bool>>();
      for(int i = 0; i < height; i++)
      {
         dist.Add(new List<bool>());
         for (int j = 0; j < width; j++)
            dist[i].Add(false);
      }
      
      return dist;
   }



   private Node getSmallest(List<List<int>> dist, List<List<bool>> visited, List<Node> cand)
   {
      Node result = new Node(-1,-1);
      int small = Int32.MaxValue;

      for(int y = 0; y < height; y++)
      {
         for(int x = 0; x < width; x++)
         {
            if( small > dist[y][x] && !visited[y][x])
            {
               small = dist[y][x];
               result.x = x;
               result.y = y;
            }
         }
      }

      for(int i = 0; i < cand.Count; i++)
      {
         if(cand[i].x == result.x && cand[i].y == result.y)
         {
            cand.RemoveAt(i);
         }
      }

      return result;
   }


   private void findShortestPath(int startX = 0, int startY = 0)
   {
      List<List<int>> dist = initDistance();
      List<List<Node>> prev = initPrevious();
      List<List<bool>> visited = initVisited();

      List<Node> queue = initQueue();
      dist[startY][startX] = 0;

      while(queue.Count > 0)
      {
         Node u = getSmallest(dist, visited, queue);
         visited[u.y][u.x] = true;
         List<Node> neighbours = getNeighbours(u);

         foreach(Node v in neighbours)
         {
            int alt = dist[u.y][u.x] + v.distance;
            if(alt < dist[v.y][v.x])
            {
               dist[v.y][v.x] = alt;
               prev[v.y][v.x] = new Node(u.x, u.y, -1); // This path shit could be wrong
            }
         }
         // Console.WriteLine(dist[height-1][width-1]);
         
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
