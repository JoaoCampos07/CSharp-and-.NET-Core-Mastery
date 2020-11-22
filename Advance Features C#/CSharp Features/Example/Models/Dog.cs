using System;
using System.Collections.Generic;
using System.Text;

namespace Main.Models
{
    public class Dog : Animal
    {
        public Dog()
        {
        }

        public Dog(string name)
        {
            this.Name = name;
        }

        public void Bark() => Console.WriteLine("Ao ao ao !");

        public string BarkLouder() => "AO AO AO !";

    }
}
