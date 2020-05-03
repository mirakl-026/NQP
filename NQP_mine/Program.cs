using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// задача NxN ферзей
// на вход подаётся размерность сетки больше 1
// необходимо вывести все возможные решения

namespace NQP_mine
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.Write("Размерность сетки: ");
            //int gridSize = Int32.Parse(Console.ReadLine());

            //Console.Write("Кол-во решений: ");
            //int solutionsCount = Int32.Parse(Console.ReadLine());

            NQP_solver nqps = new NQP_solver();

            nqps.Solve();


        }
    }
}

class NQP_solver
{
    // размерность сетки
    public int GridSize { get; set; }

    // необходимо ко-во решений
    public int SolutionsCount { get; set; }

    public NQP_solver()
    {
        GridSize = 8;
        SolutionsCount = 1;
    }

    public NQP_solver(int gs)
    {
        GridSize = gs;
    }

    public NQP_solver(int gs, int sc)
    {
        SolutionsCount = sc;
    }

    // метод решения
    public List<string> Solve()
    {
        List<string> result = new List<string>();

        NQP_field f = new NQP_field(GridSize);

        f.SetQueenInPosition(0, 4);
        f.SetQueenInPosition(1, 6);
        f.SetQueenInPosition(2, 0);
        f.SetQueenInPosition(3, 3);
        f.SetQueenInPosition(4, 1);
        f.SetQueenInPosition(5, 7);
        f.SetQueenInPosition(6, 5);
        f.SetQueenInPosition(7, 2);

        f.ShowField();       


        return new List<string>();
    }

    private string ConvertSolutionToString (int[] solution)
    {
        string result = "";
        foreach(var pos in solution)
        {
            result += pos.ToString() + ",";
        }
        return result;
    }
}

class NQP_field
{
    // размер поля
    public int fieldSize;

    // сам двумерный массив поля
    public int[,] field;

    // позиции ферзей
    public int[] queens;

    // статус поля - если обнаружена полностью заблокированная строка - решение неправильное
    public bool fieldStatus = true;

    public NQP_field (int size)
    {
        fieldSize = size;
        field = new int[size,size];
        queens = new int[size];
    }

    // установить ферзя в позицию Х, У
    // возвращает true если ферзь успешно установлен
    // возвращает false если в данную позицию нельзя установить ферзя
    public bool SetQueenInPosition(int x, int y)
    {
        if (checkQueenPosition(x, y) == true)
        {
            field[x, y] = 1;
            queens[x] = y+1;
            SetBlocks();
            return true;
        }
        else
        {
            return false;
        }        
    }

    // заполнение блокировок на установку ферзей
    private void SetBlocks ()
    {
        for (int i = 0; i < fieldSize; i++)
        {
            if (queens[i] != 0)
            {
                int currentQueenRaw = i;
                int currentQueenCol = queens[i] - 1;

                // заблокировать горизонталь
                for (int j = 0; j<fieldSize; j++)
                {
                    if (j != currentQueenCol)
                    {
                        field[currentQueenRaw, j] = 2;
                    }
                }

                // заблокировать вертикаль
                for (int j = 0; j<fieldSize; j++)
                {
                    if (j != currentQueenRaw)
                    {
                        field[j, currentQueenCol] = 2;
                    }
                }

                // заблокировать диагональ - слева-вверх
                for (int j = -1; (currentQueenCol+j >= 0) && (currentQueenRaw+j >= 0); j--)
                {
                    field[currentQueenRaw + j, currentQueenCol + j] = 2;
                }

                // заблокировать диагональ - справа-вниз
                for (int j = 1; (currentQueenCol + j < fieldSize) && (currentQueenRaw + j < fieldSize); j++)
                {
                    field[currentQueenRaw + j, currentQueenCol + j] = 2;
                }

                // заблокировать диагональ - слева-вниз
                for (int j = -1; (currentQueenCol + j >= 0) && (currentQueenRaw - j < fieldSize); j--)
                {
                    field[currentQueenRaw - j, currentQueenCol + j] = 2;
                }

                // заблокировать диагональ - справа-вверх
                for (int j = -1; (currentQueenCol - j < fieldSize) && (currentQueenRaw + j >= 0); j--)
                {
                    field[currentQueenRaw + j, currentQueenCol - j] = 2;
                }


            }
        }
    }

    public bool checkQueenPosition(int x, int y)
    {
        if ( (x >= 0 && x < fieldSize) &&
             (y >= 0 && y < fieldSize))
        {
            if (field[x,y] != 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public void ShowField()
    {
        // ферзи
        Console.Write("Queens: ");
        foreach (var q in queens)
        {
            Console.Write(q.ToString() + ",");
        }
        Console.WriteLine();
        Console.WriteLine();

        // поле
        for (int y = 0; y < fieldSize; y++)
        {
            for (int x = 0; x < fieldSize; x++)
            {
                Console.Write(field[x, y] + ",");
            }
            Console.WriteLine();
        }
    }
}
