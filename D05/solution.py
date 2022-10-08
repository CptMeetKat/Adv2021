#!/usr/bin/python3

filename = "input"
# filename = "input2"



class Coord:
   def __init__(self, c):
      coords = c.split(",")
      self.x = int(coords[0])
      self.y = int(coords[1])


def readInput(filename):
   global lines
   f = open(filename,"r")
   lines = f.readlines()

def processP1():
   global grid
   grid = [[0] * 1000 for i in range(1000)]

   # printGrid()

   for i in lines:
      s = i.split(" -> ")
      a = Coord(s[0])
      b = Coord(s[1])

      if a.x == b.x:

         if a.y > b.y:
            a,b = b,a

         for j in range(a.y, b.y+1):
            grid[j][a.x] += 1         
      elif a.y == b.y:
         if a.x > b.x:
            a,b = b,a
         
         for j in range(a.x, b.x+1):
            grid[a.y][j] += 1

   return countOverlap()

   # printGrid()

def countOverlap():
   tot = 0
   for i in range(0, len(grid)):
      for j in range(0, len(grid[0])):
         if grid[i][j] > 1:
            tot += 1
   return tot


#19630 too low
def processP2():
   global grid
   grid = [[0] * 1000 for i in range(1000)]

   # printGrid()

   for i in lines:
      s = i.split(" -> ")
      a = Coord(s[0])
      b = Coord(s[1])

      if a.x == b.x:
         if a.y > b.y:
            a,b = b,a

         for j in range(a.y, b.y+1):
            grid[j][a.x] += 1         
      elif a.y == b.y:
         if a.x > b.x:
            a,b = b,a
         
         for j in range(a.x, b.x+1):
            grid[a.y][j] += 1
      else:
         x = a.x
         y = a.y
         for i in range(0, abs(a.x - b.x)+1):
            grid[y][x] += 1
            if x < b.x:
               x += 1
            else:
               x -=1

            if y < b.y:
               y += 1
            else:
               y -= 1

   # printGrid()
   return countOverlap()



def printGrid():
   print()
   for i in range(0, len(grid)):
      print(grid[i])


def main():
   readInput(filename)
   result1 = processP1()
   result2 = processP2()

   print("Result 1: ", result1)
   print("Result 2: ", result2)


if __name__ == "__main__":
   main()



  