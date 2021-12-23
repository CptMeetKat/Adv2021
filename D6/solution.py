#!/usr/bin/python3

filename = "input"
# filename = "input2"

fish = []

class LanternFish:
   def __init__(self, age, new=False):
      self.age = age 

   def ageFish(self):
      self.age -= 1

      if self.age < 0:
         self.age = 6
         return True

      return False


def readInput(filename):
   global lines
   f = open(filename,"r")
   lines = f.readlines()
   f.close()

def processP1():
   global fish
   fish = [LanternFish(int(i)) for i in lines[0].split(",") ]

   for count in range(0,80):
      

      currFish = len(fish)
      for i in range(0, currFish):
         if fish[i].ageFish():
            fish.append( LanternFish(8, True))

   return len(fish)

def printFish():
   global fish
   s = ""

   for f in fish:
      s += str(f.age) + " "
   print(s)


def processP2():
   global data
   data = [int(i) for i in lines[0].split(",") ]

   fish = [0,0,0,0,0,0,0,0,0]

   for i in data:
      fish[i] = fish[i] + 1

   for count in range(0,256):
      fish[7] = fish[7] + fish[0]
      fish.append(fish[0])
      fish.pop(0)

   return sum(fish)


def main():
   readInput(filename)
   result1 = processP1()
   result2 = processP2()

   print("Result 1: ", result1)
   print("Result 2: ", result2)


if __name__ == "__main__":
   main()



  