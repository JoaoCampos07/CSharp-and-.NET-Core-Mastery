using System;
using System.Collections;

namespace SimpleExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Person[] persons = new Person[3]
            {
                new Person(1,"Joao"),
                new Person(2,"Paula"),
                new Person(3,"Lucena")
            };

            var peopleList = new People(persons);
            foreach (var p in peopleList)
                Console.WriteLine(p.Id + " " + p.Name);
        }
    }

    public class Person
    {
        public int Id { get; }
        public string Name;

        public Person(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }

    public class People : IEnumerable
    {
        private Person[] persons;

        public People(Person[] persons)
        {
            this.persons = new Person[persons.Length];

            for (int i = 0; i < persons.Length; i++)
            {
                this.persons[i] = persons[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public PeopleEnum GetEnumerator()
        {
            return new PeopleEnum(persons);
        }
    }

    public class PeopleEnum : IEnumerator
    {
        public Person[] persons;

        int position = -1;

        public PeopleEnum(Person[] list)
        {
            this.persons = list;
        }

        public bool MoveNext()
        {
            position++;
            return position < persons.Length;
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public Person Current
        {
            get
            {
                try
                {
                    return persons[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }

    // Questions 
    // Why on the foreach the we go to the GetEnumerator() method ?
    // R: Because is part of the People implementation, as soon we do foreach 
    // we are grabing the next Person...and also Move next
}
