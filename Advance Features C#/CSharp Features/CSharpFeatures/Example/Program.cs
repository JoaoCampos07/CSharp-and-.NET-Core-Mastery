using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Main.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace IDisposableTraining
{
    class Program
    {
        public delegate void Delegate1(string message);

        public delegate void Delegate2();

        public static ConsoleKey UserDecision { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Let's play with Delegates\n");

        MaisUmaVoltinha:

            Console.WriteLine("Press 1 : A delegate that points to a method \n");
            Console.WriteLine("Press 2 : A delegate that points to a method an Anonymous Method\n ");
            Console.WriteLine("Press 3 : Using a Delegate for a Callback \n ");
            Console.WriteLine("Press 4 : Using a Delegate with MultiCasting \n ");
            Console.WriteLine("Press 5 : Using a Multicasting Delegate for a callback \n ");
            /**Advance Uses **/
            Console.WriteLine("Press 6 : Using a Delegate to create an instance of an obj \n ");
            Console.WriteLine("Press 7 : Using a Delegate to create an instance of an Animal (already with a Name) based on his type  with predefined props \n ");
            Console.WriteLine("Press 8 : Using a Delegate to create an instance of an Animal (already with a Name) - Layer of Abstraction \n ");
            Console.WriteLine("Press 9 : Using a Delegate to create an instance of an Dog (non-generic method) - separate method \n ");
            Console.WriteLine("Press 0 : Using a Delegate to with Lambda Expression \n ");
            Console.WriteLine("Press X : Using a Delegate to with Lambda Expression And Return Type \n ");

            var input = Console.ReadKey();

            switch (input.Key) //Switch on Key enum
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    DelegatePointsTo_Method();
                    break;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    DelegatePointsTo_AnonymousMethod();
                    break;
                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    MethodWithCallback(1, 2, new Delegate1(MethodForDelegateToPointAt));
                    break;
                case ConsoleKey.NumPad4:
                case ConsoleKey.D4:
                    MethodWithMulticastingDelegates();
                    break;
                case ConsoleKey.NumPad5:
                case ConsoleKey.D5:
                    MultiCastingAndCallBack();
                    break;
                case ConsoleKey.NumPad6:
                case ConsoleKey.D6:
                    DelegatePointsToConstructor();
                    break;
                case ConsoleKey.NumPad7:
                case ConsoleKey.D7:
                    DelegatePointsToConstructor2();
                    break;
                case ConsoleKey.NumPad8:
                case ConsoleKey.D8:
                    DelegateResolveInstanceAndInvokeMethod();
                    break;
                case ConsoleKey.NumPad9:
                case ConsoleKey.D9:
                    DelegateResolveGenericInstanceAndInvokeMethod();
                    break;
                case ConsoleKey.NumPad0:
                case ConsoleKey.D0:
                    DelegateResolveGenericInstanceAndInvokeMethod_withCallback();
                    break;
                case ConsoleKey.Multiply:
                    DelegateResolveGenericInstanceAndInvokeMethod_withCallbackAndReturnType();
                    break;

            }

            CheckIfUserWantsToGoBack();

            if (Program.UserDecision == ConsoleKey.NumPad1 || Program.UserDecision == ConsoleKey.D1)
            {
                goto MaisUmaVoltinha;
            }
        }

        #region Training Exercises 

        #region Exercicio 1 

        private static void MethodForDelegate(string msg)
        {
            Console.WriteLine(System.Environment.NewLine + msg);
        }

        private static void DelegatePointsTo_Method()
        {
            // Vou instanciar o Delegate. Quando faço isso devo atribuir-lhe pelo menos um metodo 
            // para ele encapsular. 
            Program.Delegate1 handler = new Delegate1(MethodForDelegate);

            // Repare-se que o delegate não tem ideia nenhuma da implementação
            // do metodo para o qual ele aponta : "MethodForDelegate". Esta encapsulação é parecida com a das interfaces 
            handler("Olá mundo");
        }

        #endregion

        #region Exercicio 2

        private static void DelegatePointsTo_AnonymousMethod()
        {
            // Se eu usar um Anonymous method consigo o mesmo que em cima numa linha
            Program.Delegate1 delegate1 = new Delegate1(str => Console.WriteLine(Environment.NewLine + str));

            // ou assim...
            delegate1 = new Delegate1(str =>
            {
                Console.WriteLine(Environment.NewLine + str);
            });

            delegate1.Invoke("Olá mundo");
        }

        #endregion

        #region Exercicio 3 : Asynchronous Callback

        private static void MethodForDelegateToPointAt(string msg)
        {
            Console.WriteLine(System.Environment.NewLine + msg);
        }

        /// <summary>
        /// O method vai notificar o seu caller após a conclusão de uma longa operação 
        /// </summary>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <param name="callback">Neste caso o caller é um metodo que vai escrever na consola</param>
        public static void MethodWithCallback(int param1, int param2, Delegate1 callback)
        {
            var soma = param1 + param2;
            // este metodo nao tem conhecimento algum do que faz o metodo "callback"
            // apenas que recebe uma string
            callback("O resultado da operacao é : " + soma.ToString());
        }

        #endregion

        #region Exercicio 4 : MultiCasting
        public static void Method1_RU() { Console.WriteLine("Привет мир !"); }
        public static void Method2_DE() { Console.WriteLine("Hallo Welt !"); }
        public static void Method3_PL() { Console.WriteLine("Witaj świecie !"); }
        public static void Method4_UA() { Console.WriteLine("Привіт, світ !"); }
        public static void Method5_PT() { Console.WriteLine("Olá Mundo !"); }

        public static void MethodWithMulticastingDelegates()
        {
            Console.OutputEncoding = Encoding.UTF8;
            // Ao ser construido ja aponta para o method que escreve em Portugues na csl
            Program.Delegate2 delegate2 = new Delegate2(Method5_PT);
            Delegate2 d1 = Program.Method1_RU;
            Delegate2 d2 = Program.Method2_DE;
            Delegate2 d3 = Program.Method3_PL;

            delegate2 = d1 + d2;
            delegate2 += d3;
            delegate2 += Program.Method4_UA;

            // Time consuming operation
            Thread.Sleep(1000);

            delegate2.Invoke();
        }

        #endregion

        #region Exercicio 5 : MultiCasting in a CallBack

        public static void MethodWithCallback(int timeToWait, Delegate2 callback)
        {
            Console.WriteLine("Doing some time consuming task...");
            Thread.Sleep(timeToWait);
            callback();
        }

        public static void MultiCastingAndCallBack()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Delegate2 delegate2 = new Delegate2(Method1_RU);
            delegate2 += Method2_DE;
            delegate2 += Method3_PL;

            MethodWithCallback(3000, delegate2);
        }

        #endregion

        #region Exercicio 6 : Delegate that points to a constructor

        public static Func<object> func = () => new Dog("Snoopy");

        private static void DelegatePointsToConstructor()
        {
            Func<object> delegatePoinstToConstructor = func;

            // Invoke Method - that in this case is a constructor
            var delegateResult = delegatePoinstToConstructor.Invoke(); // vou buscar o resultado do metodo para o qual o meu delegate aponta
            var dog = delegateResult as Dog;

            Console.WriteLine("I created an instance of a dog. I called him " + dog.Name);
        }

        #endregion

        #region Exercicio 7 : Delegate that points to constructor according to Type

        private static void DelegatePointsToConstructor2()
        {
            // Se tentarmos construir uma instance de dog 
            // com um nome vai dar erro. 
            // não podemos ter Generic Constructors with parameters
            var instance = CreateInstance<Dog>();

            //Mas podemos fazer o seguinte:
            Func<object> del1;
            Func<object> del2;
            Program.typesDict.TryGetValue(typeof(Dog), out del1);
            Program.typesDict.TryGetValue(typeof(Cat), out del2);

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
        /// Metodo que recebe um tipo Dog ou Cat e retorna uma instancia com um nome 
        /// </summary>
        public static Dictionary<Type, Func<object>> typesDict { get; set; } =
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

        #region Exercicio 8 : Delegate that points to constructor - Add Layer of Abstraction

        public static void DelegateResolveInstanceAndInvokeMethod()
        {
            // quero ter aqui instance of dog "Snoopy" para meter abanar a cauda 
            var dog = ResolveInstance();
            dog.Bark();

        }

        private static Dog ResolveInstance()
        {
            Func<object> del;
            var x = Program.typesDict.TryGetValue(typeof(Dog), out del);

            var createInstance = del();

            return createInstance as Dog;
        }

        #endregion

        #region Exercicio 9 : Delegate that points to constructor - Add Generic Method

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
            Program.typesDict.TryGetValue(typeof(T), out del);

            var callConstructor = del.Invoke();
            // dog or cat 
            var animal = callConstructor as T;

            return animal;
        }

        #endregion

        #region Exercicio 10 : Delegate that points to constructor - Add lambda expression 

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
            Program.typesDict.TryGetValue(typeof(T), out del);

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
            Program.typesDict.TryGetValue(typeof(Tin), out del);

            var callCtor = del.Invoke();
            var animal = callCtor as Tin;

            var response = method(animal);

            return response;
        }

        #endregion

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
