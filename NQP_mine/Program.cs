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

            //NQP_solver nqps = new NQP_solver();

            //nqps.Solve();

            NQPsolver nqps = new NQPsolver(8);
            nqps.NextRaw(new List<int>());
            
            foreach(var result in nqps.results)
            {
                Console.WriteLine(result);
            }

        }
    }
}


class NQPsolver
{
    public int gridSize;

    public List<string> results;

    public NQPsolver(int size)
    {
        gridSize = size;
        results = new List<string>();
    }

    //
    public void NextRaw(List<int> currentRaw)
    {
        if (currentRaw.Count == gridSize)
        {
            string newResult = "";
            foreach (var qp in currentRaw)
            {
                newResult += (qp+1).ToString() + ",";
            }
            results.Add(newResult);
        }
        else
        {
            List<int> NextSolutions = RawHasNextSolutions(currentRaw);
            if (NextSolutions != null && NextSolutions.Count > 0)
            {
                foreach (var nextSolution in NextSolutions)
                {
                    List<int> newCurrentRaw = new List<int>(currentRaw);
                    newCurrentRaw.Add(nextSolution);
                    NextRaw(newCurrentRaw);
                }
            }
        }
    }

    // проверка на наличие следующих решений
    public List<int> RawHasNextSolutions(List<int> currentRaw)
    {
        // получить список возможных решений
        List<int> possibleSolutions = new List<int>(gridSize - currentRaw.Count);
        List<int> resultSolutions = new List<int>(gridSize - currentRaw.Count);
        for (int i = 0; i < gridSize; i++)
        {
            if (currentRaw.Contains(i) != true)
            {
                possibleSolutions.Add(i);
            }
        }

        // провереям возможные решения
        int nextX = currentRaw.Count;
        foreach (var possibleSolution in possibleSolutions)
        {
            // проверить возможность решений - блокировки с предыдущих уровней
            // заполняем поле
            NQP_field nf = new NQP_field(gridSize);
            for (int i = 0; i < currentRaw.Count; i++)
            {
                nf.SetQueenInPosition(i, currentRaw[i]);
            }

            if (nf.SetQueenInPosition(nextX, possibleSolution) == true)
            {
                resultSolutions.Add(possibleSolution);
            }
        }
        return resultSolutions;
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

    public NQP_field(int size)
    {
        fieldSize = size;
        field = new int[size, size];
        queens = new int[size];
    }

    public NQP_field(int size, int[,] data, int[] qData)
    {
        fieldSize = size;
        field = data;
        queens = qData;
    }

    // установить ферзя в позицию Х, У
    // возвращает true если ферзь успешно установлен
    // возвращает false если в данную позицию нельзя установить ферзя
    public bool SetQueenInPosition(int x, int y)
    {
        if (checkQueenPosition(x, y) == true)
        {
            field[x, y] = 1;
            queens[x] = y + 1;
            SetBlocks();
            return true;
        }
        else
        {
            return false;
        }
    }

    // заполнение блокировок на установку ферзей
    private void SetBlocks()
    {
        for (int i = 0; i < fieldSize; i++)
        {
            if (queens[i] != 0)
            {
                int currentQueenRaw = i;
                int currentQueenCol = queens[i] - 1;

                // заблокировать горизонталь
                for (int j = 0; j < fieldSize; j++)
                {
                    if (j != currentQueenCol)
                    {
                        field[currentQueenRaw, j] = 2;
                    }
                }

                // заблокировать вертикаль
                for (int j = 0; j < fieldSize; j++)
                {
                    if (j != currentQueenRaw)
                    {
                        field[j, currentQueenCol] = 2;
                    }
                }

                // заблокировать диагональ - слева-вверх
                for (int j = -1; (currentQueenCol + j >= 0) && (currentQueenRaw + j >= 0); j--)
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
        if ((x >= 0 && x < fieldSize) &&
             (y >= 0 && y < fieldSize))
        {
            if (field[x, y] != 2)
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
