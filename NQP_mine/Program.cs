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
            Console.Write("Размерность сетки: ");
            int gridSize = Int32.Parse(Console.ReadLine());

            Console.Write("Кол-во решений: ");
            int solutionsCount = Int32.Parse(Console.ReadLine());


        }
    }
}

class NQP_solver
{
    // размерность сетки
    public int GridSize;

    // необходимо ко-во решений
    public int SolutionsCount;

    // метод решения
    public List<string> Solve()
    {
        List<string> result = new List<string>();

        // допустим первые 10 решений

        


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
