using System;
using System.Collections.Generic; // Konieczne dla Stack<T>
using System.Collections.Immutable;

namespace typy_generyczne
{
    class Person : IComparable
    {
        private String name, lastname;
        private long PESEL;

        public Person(String _name, String _lastname, long _PESEL)
        {
            name = _name;
            lastname = _lastname;
            PESEL = _PESEL;
        }

        public override string ToString()
        {
            return "Imię: " + name + "\r\n" + "Nazwisko: " + lastname + "\r\n"
            + "PESEL: " + PESEL.ToString() + "\r\n";
        }

        public int CompareTo(object obj)
        {
            Person p = obj as Person;
            if (p != null)
            {
                if(p.lastname.CompareTo(lastname) == 0)
                {
                    return p.lastname.CompareTo(name);
                }
                return p.lastname.CompareTo(lastname);
            }
            return 0;
        }
    }
    public class Stack<T> where T : IComparable
    {
        private T[] stackT = null;
        private int i = 0;

        public Stack(int _size)
        {
            stackT = new T[_size];
        }

        public void Push(T element)
        {
            if (i < stackT.Length)
            {
                stackT[i] = element;
                i++;
            }
        }

        public T Pop()
        {
            if (i == 0)
            {
                return default(T);
            }
            i--;
            return stackT[i];
        }

        public T Peek()
        {
            if (i == 0)
            {
                return default(T);
            }
            return stackT[i - 1];
        }

        public bool isEmpty()
        {
            if (i == 0)
            {  
                return true; 
            }
            return false;
        }

        public void Sort()
        {
            Array.Sort(stackT, 0, i);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Stack<Person> persons = new Stack<Person>(10);
            persons.Push(new Person("Jan", "Kowalski", 45081188771));
            persons.Push(new Person("Maciej", "Ziółowski", 11112225789));
            persons.Push(new Person("Karolina", "Nowak", 12345678910));

            Console.WriteLine(persons.Pop());
            Console.WriteLine(persons.Pop());
            Console.WriteLine(persons.Pop());

            persons.Push(new Person("Jan", "Kowalski", 45081188771));
            persons.Push(new Person("Maciej", "Ziółowski", 11112225789));
            persons.Push(new Person("Karolina", "Nowak", 12345678910));

            persons.Sort();
            Console.WriteLine(persons.Pop());
            Console.WriteLine(persons.Pop());
            Console.WriteLine(persons.Pop());

            Console.ReadKey();
        }
    }
}