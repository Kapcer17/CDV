using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
//for Matrix VVV
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

/*namespace Complex
{
    class Complex
    {
        public Complex(float real, float imag) 
        {
            _real = real;
            _imag = imag;
        }

        public override string ToString()
        {
            if (_imag < 0)
            {
                return _real + "" + _imag + "i";
            }
            else 
            {
                return _real + "+" + _imag + "i";
            }
        }
//        public void print()
//       {
//            if (_imag < 0)
//            { 
//                Console.WriteLine(_real + "" + _imag + "i");
//           }
//            else
//            {
//               Console.WriteLine(_real + "+" + _imag + "i");
//            }
//        }

        private float _real, _imag;

        public static Complex operator +(Complex a, Complex b) 
        {
            return new Complex(a._real + b._real, a._imag + b._imag); ;
        }

        public static Complex operator *(Complex a, Complex b)
        {
            return new Complex(a._real * b._real+(-1 * a._imag * b._imag), a._real * b._imag + a._imag * b._real);
        }

        public double mod()
        {
            return Math.Sqrt(Math.Pow(_real, 2) + Math.Pow(_imag, 2));
        }

        public void draw()
        {
            for(int i = -10; i < 10; i++)
            {
                if (i == 0)
                {
                    for(int z = -50; z < 50; z++)
                    {
                        Console.Write("-");
                    }
                    Console.Write("Real");
                    Console.Write("\r\n");
                }
                for(int j = -50;  j < 50; j++)
                {
                    Console.Write(" ");
                    if (j == 0)
                    {
                        if(i == -10)
                        {
                            Console.Write("Imag");
                        }
                        else
                        {
                            Console.Write("|");
                        }
                    }
                    if(i == -_imag &&  j == _real)
                    {
                        Console.Write("X");
                    }
                }
                Console.Write("\r\n");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            Complex n1 = new Complex(8, 9);
            Complex n2 = new Complex(2, -4);

            Console.WriteLine("Wynik mnożenia dwóch liczb zespolonych to: " + n1 * n2);

            //            Console.WriteLine(n1.mod());

           

            n1.draw();
           
            Console.ReadKey();
        }
    }
}
*/
namespace Matrix
{
    /*class Matrix1
    {
        class Program
        {
            static void Main()
            {
                string filePath = "C:\\Users\\kstach16\\source\\repos\\Zaj2-Zadanie1\\matrix.txt";
                try
                {
                    string[] lines = File.ReadAllLines(filePath);

                    int[,] matrix = new int[lines.Length, lines[0].Split(' ').Length];

                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] numbers = lines[i].Split(' ');
                        for (int j = 0; j < numbers.Length; j++)
                        {
                            matrix[i, j] = int.Parse(numbers[j]);
                        }
                    }

                    Console.WriteLine("Macierz:");
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            Console.Write(matrix[i, j] + " ");
                        }
                        Console.WriteLine();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Błąd: " + ex.Message);
                }
            }
        }
    }
    */
    public class Matrix
        {
            private int[,] _matrix;
            public Matrix(string filePath)
            {
                string[] lines = File.ReadAllLines(filePath);

                _matrix = new int[lines.Length, lines[0].Split(' ').Length];

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] numbers = lines[i].Split(' ');
                    for (int j = 0; j < numbers.Length; j++)
                    {
                        _matrix[i, j] = int.Parse(numbers[j]);
                    }
                }
            }

            // Konstruktor pozwalający na utworzenie macierzy o dowolnym rozmiarze
            public Matrix(int rows, int cols)
            {
                _matrix = new int[rows, cols];
            }

            // Przeciążenie operatora ToString, aby ładnie wypisać macierz
            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < _matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < _matrix.GetLength(1); j++)
                    {
                        sb.Append(_matrix[i, j] + " ");
                    }
                    sb.AppendLine();
                }
                return sb.ToString();
            }

            // Przeciążenie operatora dodawania dwóch macierzy
            public static Matrix operator +(Matrix m1, Matrix m2)
            {
                if (m1._matrix.GetLength(0) != m2._matrix.GetLength(0) || m1._matrix.GetLength(1) != m2._matrix.GetLength(1))
                    throw new InvalidOperationException("Macierze muszą mieć ten sam rozmiar!");

                Matrix result = new Matrix(m1._matrix.GetLength(0), m1._matrix.GetLength(1));

                for (int i = 0; i < m1._matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < m1._matrix.GetLength(1); j++)
                    {
                        result._matrix[i, j] = m1._matrix[i, j] + m2._matrix[i, j];
                    }
                }

                return result;
            }

            // Przeciążenie operatora mnożenia dwóch macierzy
            public static Matrix operator *(Matrix m1, Matrix m2)
            {
                if (m1._matrix.GetLength(1) != m2._matrix.GetLength(0))
                    throw new InvalidOperationException("Liczba kolumn pierwszej macierzy musi być równa liczbie wierszy drugiej macierzy!");

                Matrix result = new Matrix(m1._matrix.GetLength(0), m2._matrix.GetLength(1));

                for (int i = 0; i < m1._matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < m2._matrix.GetLength(1); j++)
                    {
                        for (int k = 0; k < m1._matrix.GetLength(1); k++)
                        {
                            result._matrix[i, j] += m1._matrix[i, k] * m2._matrix[k, j];
                        }
                    }
                }

                return result;
            }

            // Transpozycja macierzy
            public Matrix Transpose()
            {
                Matrix result = new Matrix(_matrix.GetLength(1), _matrix.GetLength(0));

                for (int i = 0; i < _matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < _matrix.GetLength(1); j++)
                    {
                        result._matrix[j, i] = _matrix[i, j];
                    }
                }

                return result;
            }
        }

        class Program
        {
            static void Main()
            {
                try
                {
                    // Tworzymy macierz z pliku
                    Matrix matrix1 = new Matrix("D:\\Studia\\2rok\\Programowanie obiektowe\\Zaj2-Zadanie1&2\\matrix.txt");
                    Matrix matrix2 = new Matrix("D:\\Studia\\2rok\\Programowanie obiektowe\\Zaj2-Zadanie1&2\\matrix2.txt");

                    // Wypisujemy macierz
                    Console.WriteLine("Macierz 1:");
                    Console.WriteLine(matrix1);

                    // Transpozycja macierzy
                    Matrix transposedMatrix = matrix1.Transpose();
                    Console.WriteLine("Transponowana Macierz 1:");
                    Console.WriteLine(transposedMatrix);

                    // Dodawanie macierzy
                    Matrix sumMatrix = matrix1 + matrix2;
                    Console.WriteLine("Suma macierzy:");
                    Console.WriteLine(sumMatrix);

                    // Mnożenie macierzy
                    Matrix productMatrix = matrix1 * matrix2;
                    Console.WriteLine("Iloczyn macierzy:");
                    Console.WriteLine(productMatrix);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Błąd: " + ex.Message);
                }
            }
        }

    }