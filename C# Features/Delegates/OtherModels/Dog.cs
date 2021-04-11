namespace Delegates.OtherModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
