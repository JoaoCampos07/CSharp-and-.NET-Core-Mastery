
/*
 * Implementing the IDisposable Pattern 
 *  Esta Pattern e normalmente usado para expecificas situacoes em que 
 *  we have C# classes that maintain internal unmanaged resources.
 *  Esta Pattern aplica a fusao de duas tecnicas para lidar com unmanaged Resources : 
 *  1) Implement a Finalizer() : Para que te sintas descansado e tenhas a certeza 
 *      que o obj se vai "limpar" a si proprio when Garbage-Collected (seja isso quando for)
 *  2) Implement IDIsposable interface and method Disposable() : 
 *      Assim, o user do object pode "limpar" o obj depois do usar. No entanto, se ele 
 *      se esquecer poderemos ficar com unmanaged resources "presos" na memoria indefinitdamente.
 *  
 * RESOURCES : 
 * https://www.codeproject.com/Articles/413887/Understanding-and-Implementing-IDisposable-Interfa
 * 
 * QUESTIONS : 
 * - Porque ter dois methods Dispose() aqui ? 
 * R: Nos temos um metodo extra, que e um helper. Foi criado para que nao tenhemos Duplicate Code 
 *    porque tanto o Dispose() como o Finalizer() tem que dispor Unmanaged Resources. 
 *    Assim nos criamos um Helper method que e invocado pelo ~Finalizer() (primeira tecnica) e tambem por o Dispose() da implementacao da interface IDIsposable( segunda tecnica). 
 *    Mas que "sabe" quem o invocou e faz o dipose de Managed e Unmanaged, OU so de Unmanaged (no caso da invocao ser do FInalizer() ). 
 *    Porque nesse caso nao temos a certeza se nessa altura o obj, ou objs ainda existem sequer ! 
 *    
 * - Porque e que no finalizer libertamos apenas os Unmanaged Resources ? 
 *  R: Porque o finalizer e invocado pelo Runtime e eu nao deveo referenciar outros objetos.
 *     Devo referenciar apenas Unmanaged Resources, porque nessa altura eu nao faco ideia se esses objs 
 *     ainda permanecem em memoria ou se ja foram swiped for uma garbale-collection.
 */

namespace IDisposableTraining.Helpers
{
    using System;
    using System.IO;

    public class MyFileReaderWithIDisposable : IDisposable
    {
        public TextReader tr = null; // This .NET Framework implements IDisposable itself

        public MyFileReaderWithIDisposable(string filePath)
        {
            Console.WriteLine("Adquiring Managed Resources");
            tr = new StreamReader(filePath);

            Console.WriteLine("Adquiring Unmanaged Resources");
        }

        #region IDisposable Pattern

        //// Este metodo e a implementacao da interface e nao deve ser Virtual 
        ///  pois Derivate Class;s nao devem poder herdar este metodo.
        public void Dispose()
        {
            Console.WriteLine("Dispose called from outside");
            // Se este method esta a ser invocado por outro obj e porque o utilizador 
            // que libertar os Resources : 
            Dispose(true);

            // Ja fizemos a limpeza, mandamos os objectos que esta class utilizava 
            // para o lixo. Vamos dizer ja ao GarbageCollector para chamar o Finalizer 
            // nao ha necessidade de este ser automaticamente invocado mais tarde. 
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly (because of a an exception) by a user's code. Managed and unmanaged resources
        // can be disposed.
        //If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        protected virtual void Dispose(bool toDispose)
        {
            Console.WriteLine("Actual Dispose called with a " + toDispose.ToString());

            if (toDispose == true)
            {
                ReleaseManagedResources();
            }
            else
            {
                // Nao fazer nada, porque ninguem pediu para fazer Dispose...a invocacao foi feito pelo RunTime
                // E eu nao devo referenciar outros objectos.
                // O obj simplesmente saiu do Scope e o finalizer() foi invocado (o nosso finalizer chama o Dispose()) 
            }

            // Release the Unamanged Resources qqr que seja o caso : 
            //  - Alguem pediu a libertacao dos ManagedResources;
            //  - Este obj saiu fora do Scope
            ReleaseUnmanagedResources();
        }

        //// Called by RUntime before remove the obj from memory
        /// Is not possible to directly call it.
        ~MyFileReaderWithIDisposable()
        {
            Console.WriteLine("Finalizer was called...");

            // O obj saiu fora do Scope 
            // Vamos invocar o Dispose() para libertar os Unmanaged Resources apenas. 
            // Os ManagedResources vao ser liberados na proximada "volta" do GC.
            Dispose(false);
        }

        #endregion

        public void ShowFileData()
        {
            if (tr != null)
            {
                Console.WriteLine("Data in the file : " + tr.ReadToEnd() + " / some unmanaged data");
            }
        }

        void ReleaseManagedResources()
        {
            Console.WriteLine("Releasing Managed Resources");
            if (tr != null)
            {
                tr.Dispose();
            }
        }

        void ReleaseUnmanagedResources()
        {
            Console.WriteLine("Releasing Unmanaged Resources");
        }
    }
}
