#!/usr/bin/python3


filename = "input"
# filename = "input2"
# filename = "input3"


def readInput(filename):
   global data
   with open(filename) as f:
      data = f.readlines()

   for d in range(0, len(data)):
      data[d] = [int(i) for i in data[d].rstrip("\n") ]


def inRange(x, y, xMax, yMax):
   if x >= 0 and x < xMax and y >= 0 and y < yMax:
      return True
   return False

def printGrid():
   for i in data:
      print(i)


def increase(xMax, yMax):
   #Increase
   for y in range(0, yMax):
      for x in range(0, xMax):
         data[y][x] += 1


def flash(xMax, yMax):
   #Flash
   hasFlash = True
   flashes = 0

   while hasFlash:
      hasFlash = False
      for y in range(0, yMax):
         for x in range(0, xMax):
            
            if data[y][x] >= 10 and data[y][x] != -1:
               hasFlash = True
               data[y][x] = -1
               flashes += 1
               adjacents = [[x-1, y-1], [x-1, y],[x-1, y+1],[x, y-1],[x, y+1],[x+1,y-1],[x+1, y],[x+1, y+1]]

               for i in adjacents:
                  if inRange(i[0],i[1],xMax,yMax) and data[i[1]][i[0]] != -1:
                     data[i[1]][i[0]] += 1
   return flashes

def reset(xMax, yMax):
   #Reset
   for y in range(0, yMax):
      for x in range(0, xMax):
         if data[y][x] >= 10 or data[y][x] == -1:
            data[y][x] = 0;


def processP1():
   yMax = len(data)
   xMax = len(data[0])


   flashes = 0
   for count in range(0, 100):



      increase(xMax, yMax)
      flashes += flash(xMax, yMax)
      reset(xMax, yMax)

   # printGrid()
   return flashes



def allFlash(xMax, yMax):

   for y in range(0, yMax):
      for x in range(0, xMax):
         if data[y][x] != -1:
            return False

   return True




def processP2():
   yMax = len(data)
   xMax = len(data[0])

   step = -1
   for count in range(1, 100000):

      increase(xMax, yMax)
      flash(xMax, yMax)
      
      if allFlash(xMax, yMax):
         step = count
         break

      reset(xMax, yMax)

   printGrid()
   return step





def main():
   readInput(filename)
   result1 = processP1()
   readInput(filename)
   result2 = processP2()

   print("Result 1: ", result1)
   print("Result 2: ", result2)


if __name__ == "__main__":
   main()







            # x-1 y-1
            # x-1 y
            # x-1 y+1
            # ----
            # x  y-1
            # x  y+1
            # ----
            # x+1, y-1
            # x+1, y
            # x+1, y+1
