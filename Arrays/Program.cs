internal class Program
{
    //private static void Main(string[] args)

    //Задание 1
    //{
    //    int[] fibonacciNumbers = GetFibonacciNumbers();
    //    foreach (int number in fibonacciNumbers)
    //    {
    //        Console.WriteLine(number);
    //    }
    //}

    //static int[] GetFibonacciNumbers()
    //{
    //    int[] fibonacciArray = new int[8];
    //    fibonacciArray[0] = 0;
    //    fibonacciArray[1] = 1;

    //    for (int i = 2; i < fibonacciArray.Length; i++)
    //    {
    //        fibonacciArray[i] = fibonacciArray[i - 1] + fibonacciArray[i - 2];
    //    }

    //    return fibonacciArray;
    //}


    //Задание 2
    //static void Main()
    //{
    //    string[] months = GetMonthNames();
    //    foreach (string month in months)
    //    {
    //        Console.WriteLine(month);
    //    }
    //}

    //static string[] GetMonthNames()
    //{
    //    string[] monthNames = new string[12] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
    //    return monthNames;
    //}

    //Задание 3
    //static void Main()
    //{
    //    int[,] matrix = GetMatrix();
    //    for (int i = 0; i < 3; i++)
    //    {
    //        for (int j = 0; j < 3; j++)
    //        {
    //            Console.Write(matrix[i, j] + " ");
    //        }
    //        Console.WriteLine();
    //    }
    //}

    //static int[,] GetMatrix()
    //{
    //    int[,] matrix = new int[3, 3];

    //    for (int i = 0; i < 3; i++)
    //    {
    //        for (int j = 0; j < 3; j++)
    //        {
    //            matrix[i, j] = (int)Math.Pow(j + 2, i + 1);
    //        }
    //    }

    //    return matrix;
    //}


    //Задание 4
    //static double[][] GetMatrix()
    //{
    //    double[][] matrix = new double[3][];
    //    matrix[0] = new double[] { 1, 2, 3, 4, 5 };
    //    matrix[1] = new double[] { Math.E, Math.PI };
    //    matrix[2] = new double[] { Math.Log10(1), Math.Log10(10), Math.Log10(100), Math.Log10(1000) };

    //    return matrix;
    //}

    //static void Main()
    //{
    //    double[][] matrix = GetMatrix();

    //    //Выводим матрицу
    //    for (int i = 0; i < matrix.Length; i++)
    //    {
    //        for (int j = 0; j < matrix[i].Length; j++)
    //        {
    //            Console.Write(matrix[i][j] + "\t");
    //        }
    //        Console.WriteLine();
    //    }
    //}


    //Задание 5
    //static void CopyElements(int[] source, ref int[] destination, int N)
    //{
    //    if (source.Length < N || destination.Length < N)
    //    {
    //        return;
    //    }

    //    Array.Copy(source, destination, N);
    //}

    //static void Main()
    //{
    //    int[] source = new int[] { 2, 4, 6, 8, 10 };
    //    int[] destination = new int[5];
    //    int N = 4;

    //    CopyElements(source, ref destination, N);

    //    //Выводим второй массив после копирования, добавил пропуск нулевых значений ещё
    //    for (int i = 0; i < destination.Length; i++)
    //    {
    //        if (destination[i] != 0)
    //        {
    //            Console.Write(destination[i] + " ");
    //        }
    //    }
    //}



    //Задание 6
    //static void Main(string[] args)
    //{
    //    string[] daysOfWeek = new string [] { "Monday" };
    //    int newSize = 3;

    //    ResizeArray(ref daysOfWeek, newSize);

    //    foreach (string day in daysOfWeek)
    //    {
    //        Console.WriteLine(day);
    //    }
    //}

    //static void ResizeArray(ref string[] array, int newSize)
    //{
    //    Array.Resize(ref array, newSize);

    //    for (int i = array.Length - newSize; i < array.Length; i++)
    //    {
    //        switch (i)
    //        {
    //            case 0:
    //                array[i] = "Monday";
    //                break;
    //            case 1:
    //                array[i] = "Tuesday";
    //                break;
    //            case 2:
    //                array[i] = "Wednesday";
    //                break;
    //            case 3:
    //                array[i] = "Thursday";
    //                break;
    //            case 4:
    //                array[i] = "Friday";
    //                break;
    //            case 5:
    //                array[i] = "Saturday";
    //                break;
    //            case 6:
    //                array[i] = "Sunday";
    //                break;
    //        }
    //    }
    //}
}