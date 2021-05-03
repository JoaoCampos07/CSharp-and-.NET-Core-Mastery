namespace GarbageCollector
{
    using System;
    using System.IO;

    class MyResources : IDisposable
    {
        //The managed resource handle
        TextReader tr = null;

        public MyResources(string path)
        {
            //Lets emulate the managed resource aquisition
            Console.WriteLine("Aquiring Managed Resources");
            tr = new StreamReader(path);

            //Lets emulate the unmabaged resource aquisition
            Console.WriteLine("Aquiring Unmanaged Resources");
        }

        void ReleaseManagedResources()
        {
            Console.WriteLine("Releasing Managed Resources");
            if (tr != null)
            {
                tr.Dispose();
            }
        }

        void ReleaseUnmangedResources()
        {
            Console.WriteLine("Releasing Unmanaged Resources");
        }

        public void ShowData()
        {
            //Emulate class usage
            if (tr != null)
            {
                Console.WriteLine(tr.ReadToEnd() + " / Some unmanaged data. ");
            }
        }

        public void Dispose()
        {
            Console.WriteLine("Dispose called from outside");
            // If this function is being called the user wants to release the
            // resources. lets call the Dispose which will do this for us.
            Dispose(true);

            // Now since we have done the cleanup already there is nothing left
            // for the Finalizer to do. So lets tell the GC not to call it later.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Console.WriteLine("Actual Dispose called with a " + disposing.ToString());
            if (disposing == true)
            {
                //someone want the deterministic release of all resources
                //Let us release all the managed resources
                ReleaseManagedResources();
            }
            else
            {
                // Do nothing, no one asked a dispose, the object went out of
                // scope and finalized is called so lets next round of GC 
                // release these resources
            }

            // Release the unmanaged resource in any case as they will not be 
            // released by GC
            ReleaseUnmangedResources();

        }

        ~MyResources()
        {
            Console.WriteLine("Finalizer called");
            // The object went out of scope and finalized is called
            // Lets call dispose in to release unmanaged resources 
            // the managed resources will anyways be released when GC 
            // runs the next time.
            Dispose(false);
        }
    }

    class Program
    {
        public static ConsoleKey UserDecision { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Let's play with IDisposable and check Garbage Collection and Memory Leaks \n");

        MaisUmaVoltinha:

            Console.WriteLine("Press 0 : Do someting \n");

            var input = Console.ReadKey();

            switch (input.Key)
            {
                case ConsoleKey.NumPad0:
                case ConsoleKey.D0:
                    SomeMethod();
                    break;
            }

            CheckIfUserWantsToGoBack();

            if (Program.UserDecision == ConsoleKey.NumPad1 || Program.UserDecision == ConsoleKey.D1)
            {
                goto MaisUmaVoltinha;
            }
        }

        private static void SomeMethod()
        {
            MyResources r3 = new MyResources(@"Files\test.txt");
            r3.ShowData(); // Aqui o finalize() e invocado. Este por sua vez invoca o Dispose()
        }

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
