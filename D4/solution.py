#!/usr/bin/python3

filename = "input"

class Board:
   def __init__(self, maxX=5, maxY=5): 
      self.maxX = maxX
      self.maxY = maxY

      self.board = [[1, 1, 1, 1, 1],
                    [8, 88, 99, 13, 12],
                    [16, 62, 86, 24, 77],
                    [20, 57, 19, 67, 46],
                    [36, 83, 54, 63, 82]]

      self.mark = [[0, 0, 0, 0, 0],
                    [0, 0, 0, 0, 0],
                    [0, 0, 0, 0, 0],
                    [0, 0, 0, 0, 0],
                    [0, 0, 0, 0, 0]]

      self.nextX = 0
      self.nextY = 0

   def setNext(self, val):
      #NO SAFETY on MAX SET


      board = self.board;
      x = self.nextX;
      y = self.nextY;

      board[y][x] = val
      
      x = x + 1

      if x >= self.maxX:
         x = 0
         if y >= self.maxY:
            y = 0
         else:
            y = y + 1

      self.nextX = x;
      self.nextY = y;

      if self.maxY <= y:
         return True

      return False


   def checkRows(self):
      board = self.board
      mark = self.mark
      bingo = True
      for x in range(0, len(board)):
         bingo = True
         for y in range(0, len(board[x])):
            if mark[x][y] == 0:
               bingo = False
               break

         if bingo == True:
            return self.sumUnmarked()
            

      return -1

   def sumRow(self, row):
      board = self.board

      result = 0;
      for y in range(0, len(board[row])):

         result = result + board[row][y]

      return result

   def sumCol(self, col):
      board = self.board

      result = 0;
      for y in range(0, len(board)):

         result = result + board[y][col]

      return result

   def sumUnmarked(self):
      board = self.board
      mark = self.mark

      result = 0
      for x in range(0, len(board)):
         for y in range(0, len(board[x])):
            if mark[y][x] == 0:
               result += board[y][x]

      return result



   def checkCols(self):
      board = self.board
      mark = self.mark
      bingo = True
      for x in range(0, len(board)):
         bingo = True
         for y in range(0, len(board[x])):
            if mark[y][x] == 0:
               bingo = False
               break

         if bingo == True:
            # return self.sumCol(x) * board[y][x]
            return self.sumUnmarked()# * board[y][x]

      return -1
      

   def setDraw(self, draw):
      board = self.board
      mark = self.mark

      for x in range(0, len(board)):
         for y in range(0, len(board[x])):
            if board[y][x] == draw:
               mark[y][x] = 1

   def printBoard(self):
      board = self.board
      for i in range(0, len(board)):
         print( board[i] )

   def printMark(self):
      mark = self.mark
      for i in range(0, len(mark)):
         print( mark[i] )




def readInput(filename):
   global lines
   f = open(filename,"r")
   lines = f.readlines()

def processP1():
   readDraws()
   setBoards()


   for d in draws:
      for b in boards:
         b.setDraw(d)
         if b.checkRows() != -1:
            # b.printBoard()
            # b.printMark()
            return b.checkRows() * d

         if b.checkCols() != -1:
            # b.printBoard()
            # b.printMark()
            return b.checkCols() * d


def readDraws():
   global draws
   draws = lines[0].split(",") 
   draws = [int(i) for i in draws]

def printBoards():
   global boards
   for i in range(0, len(boards)):
      boards[i].printBoard() 


def setBoards():
   global boards
   boards = []

   for i in range(1, len(lines)):
      if lines[i] == "\n":
         boards.append( Board() )
         continue

      values = lines[i].replace("  ", " ").split(" ")

      for j in values:
         if j == "":
            continue
         boards[len(boards)-1].setNext(int(j))

def processP2():
   readDraws()
   setBoards()


   for d in draws:
      b = 0
      while b < len(boards):
         boards[b].setDraw(d)
         if boards[b].checkRows() != -1:

            if len(boards) == 1:
               return boards[b].checkRows() * d
            else:
               boards.remove(boards[b])
               b = b - 1


         if boards[b].checkCols() != -1:
            if len(boards) == 1:
               return boards[b].checkCols() * d
            else:
               boards.remove(boards[b])
               b = b - 1

         b = b + 1

            # b.printBoard()
            # b.printMark()



def main():
   readInput(filename)
   result1 = processP1()
   result2 = processP2()

   print("Result 1: ", result1)
   print("Result 2: ", result2)


if __name__ == "__main__":
   main()



  