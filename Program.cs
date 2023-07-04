using System;
namespace App
{
    internal class Project
    {
        public static int a = 9;
        public static int b = 0;
        public static int c = 3;

        public static void Main()
        {
            int[,] sudoku = new int[a, a];
            SudokuSolver S = new SudokuSolver(b, a, sudoku);

            Console.WriteLine("sudoku");
            S.fillRandSudoku(c);
            S.printCurrentSudoku();

            S.solver();

            Console.WriteLine("Sudoku solution");
            S.printCurrentSudoku();
        }
    }
    internal class SudokuSolver
    {
        private int b;
        private int a;
        private int[,] sudoku;

        public int B
        {
            get { return b; }
            set { b = value; }
        }
        public int A
        {
            get { return a; }
            set { a = value; }
        }

        public int[,] Sudoku
        {
            get { return sudoku; }
            set { sudoku = value; }
        }

        public SudokuSolver(int b, int a, int[,] sudoku)
        {
            B = b;
            A = a;
            Sudoku = sudoku;
        }
        public SudokuSolver() { }


        public int[,] sudokuFiller()
        {
            int userInpInt = b;
            string? userInpStr;
            bool isExists = false;
            for (int i = 0; i < a; i++)
            {
                for (int j = 0; j < a; j++)
                {
                    Console.WriteLine("sudoku:");
                    printCurrentSudoku();
                    do
                    {
                        Console.WriteLine(" [" + i + "][" + j + "]: ");
                        userInpStr = Console.ReadLine();
                        if (
                            userInpStr == "" ||
                            userInpStr == " " ||
                            userInpStr == null)
                        {
                            userInpInt = b;
                        }
                        else
                        {
                            userInpInt = Convert.ToInt32(userInpStr);
                        }
                        if (userInpInt > 9 || userInpInt < 0)
                        {
                            Console.WriteLine("adad bein 0-9");
                        }
                        isExists = checkDuplicateNum(userInpInt, i, j);

                        if (isExists && userInpInt != b)
                        {
                            Console.WriteLine("dar satr va soton adad tekrari nade");
                        }
                        else
                        {
                            isExists = false;
                        }
                    } while ((userInpInt > 9 || userInpInt < 0) || isExists);

                    sudoku[i, j] = userInpInt;
                }
            }
            return sudoku;
        }

        public void printCurrentSudoku(int[,] sudokuInp = null)
        {
            sudoku = sudokuInp != null ? sudokuInp : this.sudoku;
            for (int i = 0; i < a; i++)
            {
                for (int j = 0; j < a; j++)
                {
                    Console.Write(sudoku[i, j]);
                    if (j != 0 || j != a)
                    {
                        Console.Write("|");
                    }

                }
                Console.Write("\n------------------\n");
            }
        }

        private bool checkDuplicateNum(int num, int x, int y)
        {
            bool isExists = false;
            for (int i = 0; i < a; i++)
            {
                if (sudoku[i, y] == num)
                {
                    isExists = true;
                }
                if (sudoku[x, i] == num)
                {
                    isExists = true;
                }
            }

            int startRow = x - x % 3;
            int startCol = y - y % 3;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (sudoku[i + startRow, j + startCol] == num)
                    {
                        isExists = true;
                    }
                }
            }

            return isExists;
        }

        public void fillRandSudoku(int c)
        {
            int[] rands = new int[c];
            Random r = new Random();
            Random r2 = new Random();
            for (int i = 0; i < a; i++)
            {
                for (int k = 0; k < c; k++)
                {
                    rands[k] = r.Next(0, a);
                }


                for (int j = 0; j < a; j++)
                {
                    if (rands.Contains(j))
                    {
                        for (int z = 0; z < a; z++)
                        {
                            if (!checkDuplicateNum(z, i, j))
                            {
                                sudoku[i, j] = z;
                            }
                        }
                    }
                }
            }
        }
        public bool solver()
        {
            for (int i = 0; i < a; i++)
            {
                for (int j = 0; j < a; j++)
                {
                    if (sudoku[i, j] == b)
                    {
                        for (int n = 1; n <= a; n++)
                        {
                            if (!checkDuplicateNum(n, i, j))
                            {
                                sudoku[i, j] = n;
                                if (solver())
                                {
                                    return true;
                                }
                                else
                                {
                                    sudoku[i, j] = b;
                                }
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }
    }
}