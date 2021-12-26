#!/usr/bin/python3

import sys

filename = "input"
# filename = "input2"

fish = []



def readInput(filename):
   global data
   f = open(filename,"r")
   data = f.readlines()[0].split(",")
   f.close()

   data = [int(i) for i in data ]


def processP1():
   global data
   high = max(data)
   fuelSpent = 0

   minFuel = sys.maxsize

   for i in range(0, high):
      fuelSpent = 0
      for j in data:
         fuelSpent += abs(j - i)

      minFuel = min(fuelSpent, minFuel)

   return minFuel

   

def processP2():
   global data
   high = max(data)
   fuelSpent = 0

   minFuel = sys.maxsize

   for i in range(0, high):
      fuelSpent = 0
      for j in data:
         x = abs(j - i)
         fuelSpent += ((x*(x+1))/2)

      minFuel = min(fuelSpent, minFuel)

   return int(minFuel)


def main():
   readInput(filename)
   result1 = processP1()
   result2 = processP2()

   print("Result 1: ", result1)
   print("Result 2: ", result2)


if __name__ == "__main__":
   main()



  