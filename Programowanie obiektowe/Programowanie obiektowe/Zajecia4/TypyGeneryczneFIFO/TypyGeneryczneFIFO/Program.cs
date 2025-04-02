using System;
using System.Collections.Generic;

namespace typy_generyczne
{
    class Person : IComparable
    {
        private string name, lastname;
        private long PESEL;

        public Person(string _name, string _lastname, long _PESEL)
        {
            name = _name;
            lastname = _lastname;
            PESEL = _PESEL;
        }

        public override string ToString()
        {
            return $"Imię: {name}\r\nNazwisko: {lastname}\r\nPESEL: {PESEL}";
        }

        public int CompareTo(object obj)
        {
            Person p = obj as Person;
            if (p != null)
            {
                if (lastname.CompareTo(p.lastname) == 0)
                {
                    return name.CompareTo(p.name);
                }
                return lastname.CompareTo(p.lastname);
            }
            return 0;
        }

        public override bool Equals(object obj)
        {
            if (obj is Person p)
            {
                return this.name == p.name && this.lastname == p.lastname && this.PESEL == p.PESEL;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (name + lastname + PESEL).GetHashCode();
        }
    }


    public class Queue<T> where T : IComparable
    {
        private LinkedList<T> queue = new LinkedList<T>();

        public void Enqueue(T element)
        {
            queue.AddLast(element);
        }

        public T Dequeue()
        {
            if (queue.Count == 0)
            {
                return default(T);
            }
            T value = queue.First.Value;
            queue.RemoveFirst();
            return value;
        }

        public T Peek()
        {
            if (queue.Count == 0)
            {
                return default(T);
            }
            return queue.First.Value;
        }

        public bool IsEmpty()
        {
            return queue.Count == 0;
        }

        public bool FindElement(T element)
        {
            return queue.Contains(element);
        }

        public void ForEachElement(Action<T> action)
        {
            foreach (T item in queue)
            {
                action(item);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Queue<Person> personsQueue = new Queue<Person>();

            personsQueue.Enqueue(new Person("Jan", "Kowalski", 45081188771));
            personsQueue.Enqueue(new Person("Maciej", "Ziółowski", 11112225789));
            personsQueue.Enqueue(new Person("Karolina", "Nowak", 12345678910));

            Console.WriteLine("Dequeue elements:");
            Console.WriteLine(personsQueue.Dequeue());
            Console.WriteLine(personsQueue.Dequeue());
            Console.WriteLine(personsQueue.Dequeue());

            personsQueue.Enqueue(new Person("Jan", "Kowalski", 45081188771));
            personsQueue.Enqueue(new Person("Maciej", "Ziółowski", 11112225789));
            personsQueue.Enqueue(new Person("Karolina", "Nowak", 12345678910));

            bool exists = personsQueue.FindElement(new Person("Jan", "Kowalski", 45081188771));
            Console.WriteLine($"\nWyszukanie czy Jan Kowalski jest w kolejce - {exists}");

            Console.WriteLine("\nIterowanie:");
            personsQueue.ForEachElement(person => Console.WriteLine(person));

            Console.ReadKey();
        }
    }
}
