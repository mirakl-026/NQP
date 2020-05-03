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

        NQP_field f = new NQP_field(8);
        f.SetPosition(0, 0);
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

    // статус поля - если обнаружена полностью заблокированная строка - решение неправильное
    public bool fieldStatus = true;

    public NQP_field (int size)
    {
        fieldSize = size;
        field = new int[size,size];
    }

    public bool SetPosition(int x, int y)
    {
        if (checkPosition(x, y) == true)
        {
            field[x, y] = 1;
            SetBlocks();
            return true;
        }
        else
        {
            return false;
        }        
    }

    private void SetBlocks ()
    {

    }

    public bool checkPosition(int x, int y)
    {
        return true;
    }

    public void ShowField()
    {
        for (int i = 0; i < fieldSize; i++)
        {
            for (int j = 0; j < fieldSize; j++)
            {
                Console.Write(field[i, j] + ",");
            }
            Console.WriteLine();
        }
    }
}
