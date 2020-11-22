using System;
using System.Collections.Generic;
using DelegateCreatesObj.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace IDisposableTraining
{
    class Program
    {
        private static ConsoleKey UserDecision { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Let's play with X \n");

        MaisUmaVoltinha:

            Console.WriteLine("Press 0 : Do someting \n");
            Console.WriteLine("Press 1 : Using a Delegate to create an instance of an obj \n ");
            Console.WriteLine("Press 2 : Using a Delegate to create an instance of an Animal (already with a Name) based on his type  with predefined props \n ");
            Console.WriteLine("Press 3 : Using a Delegate to create an instance of an Animal (already with a Name) - Layer of Abstraction \n ");
            Console.WriteLine("Press 4 : Using a Delegate to create an instance of an Dog (non-generic method) - separate method \n ");

            var input = Console.ReadKey();

            switch (input.Key) //Switch on Key enum
            {
                case ConsoleKey.NumPad0:
                case ConsoleKey.D0:
                    DelegatePointsToConstructor();
                    break;
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    DelegatePointsToConstructor2();
                    break;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    DelegateResolveInstanceAndInvokeMethod();
                    break;
                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    DelegateResolveGenericInstanceAndInvokeMethod();
                    break;
                case ConsoleKey.NumPad4:
                case ConsoleKey.D4:
                    DelegateResolveGenericInstanceAndInvokeMethod_withCallback();
                    break;
                case ConsoleKey.NumPad5:
                case ConsoleKey.D5:
                    DelegateResolveGenericInstanceAndInvokeMethod_withCallbackAndReturnType();
                    break;
            }

            CheckIfUserWantsToGoBack();

            if (Program.UserDecision == ConsoleKey.NumPad1 || Program.UserDecision == ConsoleKey.D1)
            {
                goto MaisUmaVoltinha;
            }
        }


        #region  1 : Delegate that points to a constructor

        public static Func<object> func = () => new Dog("Snoopy");

        private static void DelegatePointsToConstructor()
        {
            Func<object> delegatePoinstToConstructor = func;

            // Invoke Method - that in this case is a constructor and grab the result of that method.
            var delegateResult = delegatePoinstToConstructor.Invoke(); 
            var dog = delegateResult as Dog;

            Console.WriteLine("I created an instance of a dog. I called him " + dog.Name);
        }

        #endregion

        #region Exercicio 2 : Delegate that points to constructor according to Type

        private static void DelegatePointsToConstructor2()
        {
            // If we try to build an instance of a dog based on a name
            // is going to give us an error, because we cannot have generic constructos.
            // However we can a generic class T that have a construtor that returns T !
            var instance = CreateInstance<Dog>();

            //Mas podemos fazer o seguinte:
            Func<object> del1;
            Func<object> del2;
            Program.TypesDict.TryGetValue(typeof(Dog), out del1);
            Program.TypesDict.TryGetValue(typeof(Cat), out del2);

            var dogConstructor = del1();
            var dog = dogConstructor as Dog;

            Console.WriteLine("The name of the dog is {0}", dog.Name);

            var catConstructor = del2();
            var cat = catConstructor as Cat;

            Console.WriteLine("The name of the cat is {0}", cat.Name);
        }

        private static T CreateInstance<T>()
            where T : new()
        {
            return new T();
        }

        /// <summary>
        /// Method that receives a type Dog or Cat and returns a instancia based on the Name !
        /// </summary>
        public static Dictionary<Type, Func<object>> TypesDict { get; set; } =
        new Dictionary<Type, Func<object>>
        {
            {
                typeof(Dog), () => new Dog("Snoopy")
            },
            {
                typeof(Cat), () => new Cat("Tica")
            }
        };

        #endregion

        #region Exercicio 3 : Delegate that points to constructor - Add Layer of Abstraction

        public static void DelegateResolveInstanceAndInvokeMethod()
        {
            // quero ter aqui instance of dog "Snoopy" para meter abanar a cauda 
            var dog = ResolveInstance();
            dog.Bark();

        }

        private static Dog ResolveInstance()
        {
            Func<object> del;
            var x = Program.TypesDict.TryGetValue(typeof(Dog), out del);

            var createInstance = del();

            return createInstance as Dog;
        }

        #endregion

        #region Exercicio 4 : Delegate that points to constructor - Add Generic Method

        private static void DelegateResolveGenericInstanceAndInvokeMethod()
        {
            var dog = ResolveGeneric<Dog>();
            dog.Bark();

            var cat = ResolveGeneric<Cat>();
            cat.Meow();
        }

        private static T ResolveGeneric<T>()
            where T : Animal
        {
            Func<object> del;
            Program.TypesDict.TryGetValue(typeof(T), out del);

            var callConstructor = del.Invoke();
            // dog or cat 
            var animal = callConstructor as T;

            return animal;
        }

        #endregion

        #region Exercicio 5 : Delegate that points to constructor - Add lambda expression 

        private static void DelegateResolveGenericInstanceAndInvokeMethod_withCallback()
        {
            ResolveGeneric2<Dog>(dog => dog.Bark()); // metodo anonimo que recebe uma instancia de cao e uso o seu metodo "natural"

        }

        /// <summary>
        /// Generic Method that according to the type define 
        /// creates an animal with particular caracteristics and them
        /// uses a callback to make that animal act.
        /// </summary>
        private static void ResolveGeneric2<T>(Action<T> method)
            where T : Animal
        {
            Func<object> del;
            Program.TypesDict.TryGetValue(typeof(T), out del);

            var callConstructor = del.Invoke();
            var animal = callConstructor as T;

            // callback
            method(animal);
        }

        #endregion

        #region Exercicio 11 : Delegate that points to conscrutor - Expressing the return type

        public static void DelegateResolveGenericInstanceAndInvokeMethod_withCallbackAndReturnType()
        {
            var response = Program.ResolveGeneric3<Dog, string>(dog => dog.BarkLouder());

            Console.WriteLine(response);
        }

        /// <summary>
        /// Generic method that resolves an instance, creating it 
        /// and setting some of its props.
        /// Them we uses a callback that have a return type
        /// define by the user of this method
        /// </summary>
        /// <typeparam name="Tin"></typeparam>
        /// <typeparam name="Tout"> response type of one of the methods that belong to Tin</typeparam>
        /// <param name="method"></param>
        /// <returns></returns>
        public static Tout ResolveGeneric3<Tin, Tout>(Func<Tin, Tout> method)
            where Tin : Animal
        {
            Func<object> del;
            Program.TypesDict.TryGetValue(typeof(Tin), out del);

            var callCtor = del.Invoke();
            var animal = callCtor as Tin;

            var response = method(animal);

            return response;
        }

        #endregion

        #region Program Utils
        public static void CheckIfUserWantsToGoBack()
        {
            Console.WriteLine("Do you want to go back to menu ? \n");
            Console.WriteLine("Press 1 : to go back to menu");
            Console.WriteLine("Press 0 : press 0 to exit \n");

            var input = Console.ReadKey();

            Program.UserDecision = input.Key;
        }
        #endregion
    }
}
