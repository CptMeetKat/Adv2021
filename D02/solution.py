#!/usr/bin/python3

filename = "input"

def readInput(filename):
   global lines
   f = open(filename,"r")
   lines = f.readlines()

def processP1():
   x = 0 #horizontal
   y = 0 #depth

   for l in lines:
      frag = l.split(" ")
      if frag[0] == "forward":
         x = x + int(frag[1])
      elif frag[0] == "up":
         y = y - int(frag[1])
      elif frag[0] == "down":
         y = y + int(frag[1])

   # print(x, y)
   return x * y

# down X increases your aim by X units.
# up X decreases your aim by X units.

# forward X does two things:
# It increases your horizontal position by X units.
# It increases your depth by your aim multiplied by X.


def processP2():
   x = 0 #horizontal
   y = 0 #depth
   aim = 0 #aim

   for l in lines:
      frag = l.split(" ")
      if frag[0] == "forward":
         x = x + int(frag[1])
         y = y + (aim * int(frag[1]))
      elif frag[0] == "up":
         aim = aim - int(frag[1])
      elif frag[0] == "down":
         aim = aim + int(frag[1])
         

   # print(x, y)
   return x * y


      

def main():
   readInput(filename)
   result1 = processP1()
   result2 = processP2()

   print("Result 1: ", result1)
   print("Result 2: ", result2)
   
   




if __name__ == "__main__":
   main()



  