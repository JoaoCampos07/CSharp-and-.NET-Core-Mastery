namespace Delegates.OtherModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Cat : Animal
    {
        public Cat(string name)
        {
            this.Name = name;
        }

        public void Meow() => Console.WriteLine("Miau miau");
    }
}
