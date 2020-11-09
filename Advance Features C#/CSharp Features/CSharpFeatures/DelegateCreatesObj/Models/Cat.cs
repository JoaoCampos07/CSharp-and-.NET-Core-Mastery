using System;
using System.Collections.Generic;
using System.Text;

namespace DelegateCreatesObj.Models
{
    public class Cat : Animal
    {
        public Cat(string name)
        {
            this.Name = name;
        }

        public void Meow() => Console.WriteLine("Miau miau");
    }
}
