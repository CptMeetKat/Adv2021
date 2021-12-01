#!/usr/bin/python3
# import timeit

filename = "input"

def readInput(filename):
   global lines
   f = open(filename,"r")
   lines = f.readlines()

def processP1():
   result = 0;
   for i in range(1, len(lines)):
      if int(lines[i]) > int(lines[i-1]):
         result +=1

   print("RESULT 1: ", result)


def processP2():
   result = 0;
   for i in range(1, len(lines)-2):
      currWin = int(lines[i-1]) + int(lines[i]) + int(lines[i+1])
      forWin = int(lines[i]) + int(lines[i+1]) + int(lines[i+2])

      if currWin < forWin:
         result +=1

   print("RESULT 2: ", result)

      

def main():
   readInput(filename)
   # start = timeit.default_timer()
   processP1()
   # stop = timeit.default_timer()
   # print('Time: ', stop - start)
   
   processP2()




if __name__ == "__main__":
   main()



  