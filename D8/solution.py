#!/usr/bin/python3


filename = "input"
# filename = "input2"


def readInput(filename):
   global data
   with open(filename) as f:
      data = f.read().splitlines() 


def inRange(x, y, xMax, yMax):
   if x >= 0 and x < xMax and y >= 0 and y < yMax:
      return True
   return False


def processP1():
   yMax = len(data)
   xMax = len(data[0])

   valleys = []

   for y in range(0, yMax):
      for x in range(0, xMax):

         isValley = True         
         if inRange(x+1, y, xMax, yMax):
            if data[y][x] >= data[y][x+1]:
               isValley = False


         if inRange(x-1, y, xMax, yMax):
            if data[y][x] >= data[y][x-1]:
               isValley = False


         if inRange(x, y+1, xMax, yMax):
            if data[y][x] >= data[y+1][x]:
               isValley = False


         if inRange(x, y-1, xMax, yMax):
            if data[y][x] >= data[y-1][x]:
               isValley = False

         if isValley:
            valleys.append(int(data[y][x]) + 1)

   return sum(valleys)


def exploreValley(valley, xMax, yMax):
   global data

   candidates = [valley]

   totArea = 0
   while len(candidates) > 0:
      c = candidates.pop()
      y = c[0]
      x = c[1]
      
      if inRange(x, y, xMax, yMax    ) and data[y][x] != "x" and data[y][x] != "9":
         # data[y][x] = "x"
         s = data[y]
         s = s[:x] + "x" + s[x+1:]
         data[y] = s
         
         totArea += 1
         candidates.append( [y+1,x] )
         candidates.append( [y-1,x] )
         candidates.append( [y,x+1] )
         candidates.append( [y,x-1] )

   return totArea





def processP2():

   yMax = len(data)
   xMax = len(data[0])

   valleys = []

   for y in range(0, yMax):
      for x in range(0, xMax):

         isValley = True         
         if inRange(x+1, y, xMax, yMax):
            if data[y][x] >= data[y][x+1]:
               isValley = False


         if inRange(x-1, y, xMax, yMax):
            if data[y][x] >= data[y][x-1]:
               isValley = False


         if inRange(x, y+1, xMax, yMax):
            if data[y][x] >= data[y+1][x]:
               isValley = False


         if inRange(x, y-1, xMax, yMax):
            if data[y][x] >= data[y-1][x]:
               isValley = False

         if isValley:
            valleys.append([y, x])

   valleyArea = []
   for v in valleys:
      valleyArea.append( exploreValley(v, xMax, yMax) )

   valleyArea.sort()
   valleyArea.reverse()

   return valleyArea[0] * valleyArea[1] * valleyArea[2]



def main():
   readInput(filename)
   result1 = processP1()
   result2 = processP2()

   print("Result 1: ", result1)
   print("Result 2: ", result2)


if __name__ == "__main__":
   main()



  